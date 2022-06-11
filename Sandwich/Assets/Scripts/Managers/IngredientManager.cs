using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager: MonoBehaviour
{
    public static IngredientManager Instance;
    [SerializeField] private IngredientSO breadSO;
    [SerializeField] private IngredientSO ingredientSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public Ingredient GetRandomIngredient()
    {
        Ingredient randomIngredient = ingredientSO.ingredients[Random.Range(0, ingredientSO.ingredients.Count)];
        return randomIngredient;
    }

    public Ingredient SetupBread()
    {
        return breadSO.ingredients[Random.Range(0, breadSO.ingredients.Count)];
    }

    public Ingredient SetIngredient(bool isBread)
    {
        Ingredient ingredient;

        if (isBread)
        {
            ingredient = SetupBread();
        }
        else
        {
            ingredient = GetRandomIngredient();
        }

        return ingredient;
    }
}
