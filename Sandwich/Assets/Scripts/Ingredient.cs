using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Ingredient : MonoBehaviour
{
    private IngredientSO breadSO;
    private List<IngredientSO> ingredients;
    public bool isBread;
    
    public IngredientSO SetupRandomIngredient(bool isBread)
    {
        if (isBread)
        {
            SetupIngredient(breadSO);
        }
        IngredientSO randomIngredient = ingredients[UnityEngine.Random.Range(0, ingredients.Count)];
        SetupIngredient(randomIngredient);
        return randomIngredient;
    }

    public void SetupIngredient(IngredientSO so)
    {
        this.GetComponent<MeshFilter>().mesh = so.mesh;
        isBread = so.isBread;
    }
}
