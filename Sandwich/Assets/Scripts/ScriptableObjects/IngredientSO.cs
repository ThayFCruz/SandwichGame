using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Ingredient")]
public class IngredientSO : ScriptableObject
{
    public List<Ingredient> ingredients;
}
