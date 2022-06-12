using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridManager : MonoBehaviour
{

   // [SerializeField] [Range(4, GridArea)] private int ingredientsQuantity = 4;

    [SerializeField] private Tile tile;

    [SerializeField] private IngredientManager ingredient;

    [SerializeField] private DragHandler dragHandler;

    [SerializeField] private UiManager uiManager;

    private Dictionary<Vector2, Tile> tileDictionary;

    private const int GridSize = 4;

    public const float GridArea = GridSize * GridSize;

    private List<Tile> occupiedTiles = new List<Tile>();

    void Start()
    {
        dragHandler.onMovedPieces += CheckWinning;
    }

    public void CreateEmptyGrid()
    {
        tileDictionary = new Dictionary<Vector2, Tile>();
        for (int lin = 0; lin < GridSize; lin++)
        {
            for (int col = 0; col < GridSize; col++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(lin,0,col), Quaternion.identity);
                spawnedTile.name = $"Tile {lin} {col}";

                spawnedTile.Setup(new Vector2(lin,col));

                tileDictionary[new Vector2(lin, col)] = spawnedTile;
            }
        }
    }

    public void CleanGrid()
    {
        occupiedTiles.Clear();
        foreach (Tile tile in tileDictionary.Values)
        {
            if (tile.ingredients.Count > 0)
            {
                tile.CleanTile();
            }
        }
    }

    public void FillGridWithSO(LevelLayout grid, int ingredientsQuantity)
    {
        ingredientsQuantity = grid.layouts.Count;

        foreach (Item item in grid.layouts)
        {
            Tile tile = GetTileAtPosition(item.position);
            tile.AddIngredient(item.ingredient);
            occupiedTiles.Add(tile);
        }
    }

    public LevelLayout FillGridRandomly(int ingredientsQuantity, int breadsQuantity = 2)
    {
        List<Vector2> availableTiles = new List<Vector2>(tileDictionary.Keys);

        LevelLayout levelLayout = new LevelLayout();
        levelLayout.layouts = new List<Item>();


        for (int i =0;i < ingredientsQuantity; i++)
        {
            Vector2 newPosition = availableTiles[Random.Range(0, availableTiles.Count)];
            Tile newTile = GetTileAtPosition(newPosition);
            Ingredient ingr = ingredient.SetIngredient(i < breadsQuantity);
            newTile.AddIngredient(ingr);
            Item item = new Item(newPosition, ingr);
            occupiedTiles.Add(newTile);

            availableTiles = NewRandomTile();
            
            levelLayout.layouts.Add(item);
        }
        
        return levelLayout;
    }

    private List<Vector2> NewRandomTile()
    {
        Tile sortedTile = occupiedTiles[Random.Range(0, occupiedTiles.Count)];

        List <Vector2> availableTiles = GetNeighbours(sortedTile);

        if(availableTiles.Count == 0)
        {
           availableTiles = NewRandomTile();
        }

        return availableTiles;
       
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tileDictionary.TryGetValue(pos, out var clickedTile)) return clickedTile;
        return null;
    }

    public void CheckWinning(Tile tile)
    {
        FreeTile(tile);
        if (occupiedTiles.Count == 1)
        {
            Ingredient[] ing = occupiedTiles[0].ingredients.ToArray();
            if(ing[0].isBread && ing[ing.Length - 1].isBread)
            {
                occupiedTiles[0].transform.DOShakeScale(1f,1f,5,50).OnComplete(() => uiManager.WinScreen());
            }
        }
    }

    private List<Vector2> GetNeighbours(Tile tile)
    {
        List<Vector2> neighbours = new List<Vector2>();
        Vector2Int[] neighboursOffsetPos = { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };

        for(int i=0; i < neighboursOffsetPos.Length; i++)
        {
            Vector3 tilePosition = tile.transform.position;
            Vector2 neighbourPosition = new Vector2(tilePosition.x, tilePosition.z) + neighboursOffsetPos[i];

            if(tileDictionary.ContainsKey(neighbourPosition))
            {
                Tile neighbourTile = GetTileAtPosition(neighbourPosition);
                if (neighbourTile.ingredients.Count <= 0)
                {
                    neighbours.Add(neighbourPosition);
                }
            }
        }

        return neighbours;
    }

    public void FreeTile(Tile tile)
    {
        occupiedTiles.Remove(tile);
    }
    
}