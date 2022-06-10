using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<Ingredient> ingredients;

    void Start()
    {
       
    }


    public void Setup()
    {
        ingredients = new List<Ingredient>();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }

}
