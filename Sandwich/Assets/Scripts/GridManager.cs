using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{

    [SerializeField] [Range(4, GridArea)] private int ingredientsQuantity = 4;

    [SerializeField] private Tile tile;

    [SerializeField] private Ingredient ingredient;

    [SerializeField] private DragHandler dragHandler;

    private Dictionary<Vector2, Tile> tileDictionary;

    private const int GridSize = 4;

    public const float GridArea = GridSize * GridSize;

    private List<Tile> occupiedTiles = new List<Tile>();

    void Start()
    {
        dragHandler.onMovedPieces += CheckWinning;
        CreateEmptyGrid();
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

                spawnedTile.Setup();

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

    public void FillGridWithSO(LevelLayout grid)
    {
        ingredientsQuantity = grid.layouts.Count;
        foreach (Item item in grid.layouts)
        {
            Tile tile = GetTileAtPosition(item.Position);
            ingredient.SetupIngredient(item.Ingredient);
            tile.AddIngredient(ingredient);
            occupiedTiles.Add(tile);
        }
    }

    public LevelLayout FillGridRandomly(int breadsQuantity = 2)
    {
        List<Vector2> availableTiles = new List<Vector2>(tileDictionary.Keys);
        List<Tile> occupiedTiles = new List<Tile>();

        LevelLayout levelLayout = new LevelLayout();

        for (int i =0;i < ingredientsQuantity; i++)
        {
            Vector2 newPosition = availableTiles[Random.Range(0, availableTiles.Count)];
            Tile newTile = GetTileAtPosition(newPosition);

            IngredientSO so = ingredient.SetupRandomIngredient(i < breadsQuantity);
            newTile.AddIngredient(ingredient);

            occupiedTiles.Add(newTile);
            newTile = occupiedTiles[Random.Range(0, occupiedTiles.Count)];

            availableTiles = GetNeighbours(newTile);

            Item item = new Item(newPosition, so);
            levelLayout.layouts.Add(item);
        }
        
        return levelLayout;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tileDictionary.TryGetValue(pos, out var clickedTile)) return clickedTile;
        return null;
    }

    public void CheckWinning()
    {
        if (occupiedTiles.Count == 1)
        {
            Ingredient[] ing = occupiedTiles[0].ingredients.ToArray();
            if(ing[0].isBread && ing[ing.Length - 1].isBread)
            {
                Debug.Log("You win");
            }
        }
    }

    private Tile GetLastFilledTile()
    {
        
        foreach (Tile tile in tileDictionary.Values)
        {
            if (tile.ingredients.Count > 0)
            {
                return tile;
            }
        }

        return null;
    }

    private List<Vector2> GetNeighbours(Tile tile)
    {
        List<Vector2> neighbours = new List<Vector2>();
        Vector2[] neighboursOffsetPos = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };

        for(int i=0; i < 2; i++)
        {
            Vector3 tilePos = tile.transform.position;
            Vector2 neighbourPosition = new Vector2(tilePos.x, tilePos.z) + neighboursOffsetPos[i];

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
