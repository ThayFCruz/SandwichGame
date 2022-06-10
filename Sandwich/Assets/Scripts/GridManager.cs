using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] [Range(4, GridArea)] private int ingredientsQuantity = 4;

    [SerializeField] private Tile tile;

    [SerializeField] private List<GridSO> layouts;

    private Dictionary<Vector2, Tile> tileDictionary;

    private const int GridSize = 4;

    private const float GridArea = GridSize * GridSize;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        CreateEmptyGrid();
        FillGridWithSO(layouts[0]);
    }

    private void CreateEmptyGrid()
    {
        tileDictionary = new Dictionary<Vector2, Tile>();
        for (int lin = 0; lin < GridSize; lin++)
        {
            for (int col = 0; col < GridSize; col++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(lin,0,col), Quaternion.identity,this.transform);
                spawnedTile.name = $"Tile {lin} {col}";

                spawnedTile.Setup();

                tileDictionary[new Vector2(lin, col)] = spawnedTile;
            }
        }

    }

    private void FillGridRandomly()
    {


    }

    private void FillGridWithSO(GridSO grid)
    {
        ingredientsQuantity = grid.layout.Count;
        foreach(Item item in grid.layout)
        {
            Tile tile = GetTileAtPosition(item.Position);
            Vector3 tilePosition = tile.transform.position;
            Instantiate(item.Ingredient, new Vector3(tilePosition.x, tilePosition.y + 0.2f, tilePosition.z), Quaternion.identity, tile.transform);

            tile.AddIngredient(item.Ingredient);
        }
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tileDictionary.TryGetValue(pos, out var clickedTile)) return clickedTile;
        return null;
    }
}
