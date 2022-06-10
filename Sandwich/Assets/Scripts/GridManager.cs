using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] [Range(4, GridArea)] private int ingredientsQuantity = 4;

    [SerializeField] private Tile tile;

    [SerializeField] private List<GridSO> layouts;

    [SerializeField] private DragHandler dragHandler;

    private Dictionary<Vector2, Tile> tileDictionary;

    private const int GridSize = 4;

    private const float GridArea = GridSize * GridSize;

    private int occupiedSlots = 0;

    private int level = 0;

    void Start()
    {
        dragHandler.onMovedPieces += CheckWinning;
        Init();
    }

    private void Init()
    {
        CreateEmptyGrid();
        FillGridWithSO(layouts[level]);
    }

    private void NewGame()
    {
        occupiedSlots = 0;
        CleanGrid();
        FillGridWithSO(layouts[level]);
    }

    private void CreateEmptyGrid()
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

    private void CleanGrid()
    {
        foreach (Tile tile in tileDictionary.Values)
        {
            if (tile.ingredients.Count > 0)
            {
                tile.CleanTile();
            }
        }
    }

    private void FillGridWithSO(GridSO grid)
    {
        ingredientsQuantity = grid.layout.Count;
        foreach (Item item in grid.layout)
        {
            Tile tile = GetTileAtPosition(item.Position);
            tile.AddIngredient(item.Ingredient);
            occupiedSlots++;
        }
    }

    private void FillGridRandomly()
    {


    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tileDictionary.TryGetValue(pos, out var clickedTile)) return clickedTile;
        return null;
    }

    public void CheckWinning()
    {
        occupiedSlots--;
        Tile lastTile = null;
        if (occupiedSlots == 1)
        {
            lastTile = GetLastFilledTile();
        }

        if(lastTile != null)
        {
            Ingredient[] ing = lastTile.ingredients.ToArray();
            if(ing[0].isBread && ing[ing.Length - 1].isBread)
            {
                Debug.Log("You win");
                lastTile.OnMovedPieces();
                level++;
                NewGame();
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
}
