using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridManager : MonoBehaviour
{

    [SerializeField] private Tile tile;

    [SerializeField] private IngredientManager ingredient;

    [SerializeField] private DragHandler dragHandler;

    [SerializeField] private UiManager uiManager;

    private Dictionary<Vector2, Tile> tileDictionary;

    private const int GridSize = 4;

    //used to manage how many tiles are occupied
    private List<Tile> occupiedTiles = new List<Tile>();

    void Start()
    {
        dragHandler.onMovedPieces += CheckWinning;
    }

    //The Grid is created just once and reused every game
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
                tile.RemoveIngredients();
            }
        }
    }

    //Fill Grid using a Layout already saved in Scriptable Object
    public void FillGridWithSO(LevelLayout grid, int ingredientsQuantity)
    {
        ingredientsQuantity = grid.layouts.Count;

        foreach (IngredientInSO item in grid.layouts)
        {
            Tile tile = GetTileAtPosition(item.position);
            tile.AddIngredient(item.ingredient);
            occupiedTiles.Add(tile);
        }
    }

    //Fill grid randomly with the selected ingredients quantity and return the Layout to be saved
    public LevelLayout FillGridRandomly(int ingredientsQuantity, int breadsQuantity = 2)
    {
        //available tiles to instantiate a new ingredient
        List<Vector2> availableTiles = new List<Vector2>(tileDictionary.Keys);

        LevelLayout levelLayout = new LevelLayout();
        levelLayout.layouts = new List<IngredientInSO>();

        for (int i = 0; i < ingredientsQuantity; i++)
        {
            //sort the new ingredient position and get corresponding tile
            Vector2 newPosition = availableTiles[Random.Range(0, availableTiles.Count)];
            Tile newTile = GetTileAtPosition(newPosition);

            //sets the new ingredient (add the breads fisrt and after reaching the bread quantity start to sort)
            Ingredient ingr = ingredient.SetIngredient(i < breadsQuantity);
            newTile.AddIngredient(ingr);

            //Creates a new ingredient in SO to save the current layout
            IngredientInSO item = new IngredientInSO(newPosition, ingr);

            //include the new ingredient in the list of occupied tiles
            occupiedTiles.Add(newTile);

            availableTiles = NewRandomTile();

            levelLayout.layouts.Add(item);
        }

        return levelLayout;
    }

    //used to randomly choose one of the ingredients in the grid to sort a neighbour to receive a new ingredient
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

    //used to return the neighbours of a tile
    private List<Vector2> GetNeighbours(Tile tile)
    {
        List<Vector2> neighbours = new List<Vector2>();
        Vector2Int[] neighboursOffsetPos = { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };

        for (int i = 0; i < neighboursOffsetPos.Length; i++)
        {
            Vector3 tilePosition = tile.transform.position;
            Vector2 neighbourPosition = new Vector2(tilePosition.x, tilePosition.z) + neighboursOffsetPos[i];

            if (tileDictionary.ContainsKey(neighbourPosition))
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

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tileDictionary.TryGetValue(pos, out var clickedTile)) return clickedTile;
        return null;
    }

    //every movement checks if grid has only one stack 
    public void CheckWinning(Tile tile)
    {
        FreeTile(tile);
        if (occupiedTiles.Count == 1)
        {
            Ingredient[] ing = occupiedTiles[0].ingredients.ToArray();
            if(ing[0].isBread && ing[ing.Length - 1].isBread)
            {
                occupiedTiles[0].transform.DOShakeScale(1f,1f,5,50).OnComplete(() => uiManager.ShowWinScreen());
            }
        }
    }

    public void FreeTile(Tile tile)
    {
        occupiedTiles.Remove(tile);
    }
    
}
