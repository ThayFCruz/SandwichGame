using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Grid")]
public class GridSO : ScriptableObject
{
    public List<LevelLayout> levelLayouts;
}

[System.Serializable]
public class LevelLayout
{
    public List<Item> layouts;
}

[System.Serializable]
public class Item
{
    [SerializeField] public Vector2 position;
    [SerializeField] public Ingredient ingredient;

    public Item(Vector2 pos, Ingredient ingr)
    {
        position = pos;
        ingredient = ingr;
    }

}