using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private DragHandler dragHandler;
    public Stack<Ingredient> ingredients;
    public Vector2 tilePosition;
    public List<string> i = new List<string>();

    void Start()
    {
        dragHandler = DragHandler.Instance;
    }


    public void Setup(Vector2 pos)
    {
        ingredients = new Stack<Ingredient>();
        tilePosition = pos;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Push(ingredient);
        Instantiate(ingredient, transform.position + new Vector3(0, 0.2f * ingredients.Count, 0), Quaternion.identity, transform);
    }

    private void OnMouseEnter()
    {
        if (dragHandler.interactable && ingredients.Count > 0)
        {
            if (dragHandler.canFlip && dragHandler.startingTile != this)
            {
                if (IsNeighbour(dragHandler.startingTile))
                {
                    foreach (Ingredient ingredient in dragHandler.startingTile.ingredients)
                    {
                        AddIngredient(ingredient);
                    }

                    dragHandler.MovedPieces();
                }
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
            dragHandler.startingTile = null;
            dragHandler.onMovedPieces -= IngredientsMoved;
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
        if(tile == this)
        {
            CleanTile();
        }
    }

    public void CleanTile()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        ingredients.Clear();
    }
}
