using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Ingredient")]
public class IngredientSO : ScriptableObject
{
    //used to define every ingredient of a type
    public List<Ingredient> ingredients;
}
