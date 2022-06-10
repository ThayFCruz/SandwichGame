using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform camera;

    [SerializeField] [Range(4, GridArea)] private int ingredientsQuantity = 4;

    [SerializeField] private Tile tile;

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
    }

    private void CreateEmptyGrid()
    {
        tileDictionary = new Dictionary<Vector2, Tile>();
        for (int lin = 0; lin < GridSize; lin++)
        {
            for (int col = 0; col < GridSize; col++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(lin,0.2f,col), Quaternion.identity,this.transform);
                spawnedTile.name = $"Tile {lin} {col}";

                spawnedTile.Setup();

                tileDictionary[new Vector2(lin, col)] = spawnedTile;
            }
        }

        //camera.transform.position = new Vector3((float)GridSize / 2 - 0.5f, (float)GridSize / 2 - 0.5f, -10);
    }

    private void FillGridRandomly()
    {

    }

    private void FillGridWithSO(GridSO grid)
    {


    }

    public Tile GetClickedTile(Vector2 pos)
    {
        if (tileDictionary.TryGetValue(pos, out var clickedTile)) return clickedTile;
        return null;
    }
}
