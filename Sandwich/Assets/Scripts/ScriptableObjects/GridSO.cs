using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Grid")]
public class GridSO : ScriptableObject
{
    //list of different levels
    public List<LevelLayout> levelLayouts;
}

[System.Serializable]
public class LevelLayout
{
    //layout of a level
    public List<IngredientInSO> layouts;
}

[System.Serializable]
public class IngredientInSO
{
    [SerializeField] public Vector2 position;
    [SerializeField] public Ingredient ingredient;

    public IngredientInSO(Vector2 pos, Ingredient ingr)
    {
        position = pos;
        ingredient = ingr;
    }

}