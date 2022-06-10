using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Grid")]
public class GridSO : ScriptableObject
{
    public List<GridLayout> layout;
}

[System.Serializable]
public class GridLayout
{
    [SerializeField] private Vector2 position;
    [SerializeField] private GridSO tile;
}