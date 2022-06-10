using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Grid")]
public class GridSO : ScriptableObject
{
    public List<Item> layout;
}

[System.Serializable]
public class Item
{
    [SerializeField] private Vector2 position;
    [SerializeField] private Ingredient ingredient;

    public Ingredient Ingredient => ingredient;
    public Vector2 Position => position;

}