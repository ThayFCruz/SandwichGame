using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    private DragHandler dragHandler;
    public Stack<Ingredient> ingredients;
    public GameObject ingredientsGroup;
    Quaternion originalRotation;
    void Start()
    {
        dragHandler = DragHandler.Instance;
        originalRotation = ingredientsGroup.transform.rotation;
    }


    public void Setup(Vector2 pos)
    {
        ingredients = new Stack<Ingredient>();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Push(Instantiate(ingredient, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, ingredientsGroup.transform));
    }

    public void Shake()
    {
        ingredientsGroup?.transform.DOShakeRotation(0.3f, new Vector3 (20,0), 10, 50).OnComplete( () => ingredientsGroup.transform.rotation = Quaternion.identity);
        
    }

    private void OnMouseEnter()
    {
        if (dragHandler.interactable) 
        {
            if (ingredients.Count > 0)
            {
                if (dragHandler.canFlip && dragHandler.startingTile != this)
                {
                    Tile startingTile = dragHandler.startingTile;
                    if (IsNeighbour(startingTile))
                    {

                        startingTile?.ingredientsGroup.transform.DOJump(ingredientsGroup.transform.position + new Vector3(0, 0.1f * ingredients.Count + 0.1f * startingTile.ingredients.Count), 2, 1, 0.2f).OnPlay(() =>
                        {
                            startingTile.ingredientsGroup.transform.DORotate(new Vector3(-180, 0, 0), 0.3f).OnComplete(() =>
                            {
                                foreach (Ingredient ingredient in startingTile.ingredients)
                                {
                                    ingredient.transform.SetParent(ingredientsGroup.transform);
                                    ingredients.Push(ingredient);
                                }
                                dragHandler.MovedPieces();
                            });
                        });

                        //ingredientsGroup.transform.position = this.transform.position + new Vector3(0, ingredients.Count * 0.2f, 0);
                    }
                }
            }
            else
            {
                dragHandler.MovedWrong();
            }
        }
    }

    private void OnMouseDown()
    {
        if (!dragHandler.canFlip && dragHandler.interactable)
        {
            dragHandler.startingTile = this;
            dragHandler.canFlip = true;
            dragHandler.onMovedPieces += IngredientsMoved;
        }
    }

    private void OnMouseUp()
    {
        if (dragHandler.interactable)
        {
            dragHandler.canFlip = false;
        }

    }

    private bool IsNeighbour(Tile tile)
    {
        Vector3 tilePos = tile.transform.position;
        Vector3 myPos = transform.position;
        if(myPos.x == tilePos.x && (tilePos.z == myPos.z + 1 || tilePos.z == myPos.z - 1))
        {
            return true;
        }else if (myPos.z == tilePos.z && (tilePos.x == myPos.x + 1 || tilePos.x == myPos.x - 1))
        {
            return true;
        }

        return false;
    }

    private void IngredientsMoved(Tile tile)
    {
        if (tile == this)
        {
            ingredients.Clear();
            ingredientsGroup.transform.localPosition = new Vector3(0, 0.15f, 0);
            ingredientsGroup.transform.DORotate(new Vector3(0, 0, 0), 0f);
            CleanTile();
            dragHandler.onMovedPieces -= IngredientsMoved;
        }
    }

    public void CleanTile()
    {
        foreach (Transform child in ingredientsGroup.transform)
        {
            Destroy(child.gameObject);
        }
        ingredients.Clear();
    }
}
