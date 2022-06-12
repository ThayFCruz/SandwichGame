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

    public void Setup()
    {
        ingredients = new Stack<Ingredient>();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Push(Instantiate(ingredient, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, ingredientsGroup.transform));
    }

    public void RemoveIngredients()
    {
        foreach (Transform child in ingredientsGroup.transform)
        {
            Destroy(child.gameObject);
        }
        ingredients.Clear();
    }

    #region managing the drags of ingredients 

    //when the player select this tile to move to another
    private void OnMouseDown()
    {
        if (!dragHandler.canFlip && dragHandler.interactable)
        {
            dragHandler.startingTile = this;
            dragHandler.canFlip = true;
            dragHandler.onMovedPieces += IngredientsMoved;
        }
    }

    //when the player cancel the drag
    private void OnMouseUp()
    {
        if (dragHandler.interactable)
        {
            dragHandler.canFlip = false;
        }

    }

    //used to try to move the selected pieces to him whenever the player pass through
    private void OnMouseEnter()
    {
        if (dragHandler.interactable) 
        {
            if (ingredients.Count > 0)
            {
                if (dragHandler.canFlip && dragHandler.startingTile != this)
                {
                    Tile startingTile = dragHandler.startingTile;

                    //if is neighbour and has ingredients, then can move the pieces
                    if (IsNeighbour(startingTile))
                    {
                        //animating the movement of the group of ingredients to the new tile
                        startingTile?.ingredientsGroup.transform.DOJump(ingredientsGroup.transform.position + new Vector3(0, 0.1f * ingredients.Count + 0.1f * startingTile.ingredients.Count), 2, 1, 0.2f)
                        .OnPlay(() =>
                        {
                            startingTile.ingredientsGroup.transform.DORotate(new Vector3(-180, 0, 0), 0.3f)
                            .OnComplete(() =>
                            {
                                //after the animation adding each new ingredient to the tile list
                                foreach (Ingredient ingredient in startingTile.ingredients)
                                {
                                    ingredient.transform.SetParent(ingredientsGroup.transform);
                                    ingredients.Push(ingredient);
                                }

                                //used to shoot the event when the pieces are already moved between tiles
                                dragHandler.PiecesMoved();
                            });
                        });

                    }
                }
            }
            else
            {
                //when this tile has no ingredients
                dragHandler.CantMove();
            }
        }
    }

    //checks if the passed tile is neighbour from this one
    private bool IsNeighbour(Tile tile)
    {
        Vector3 tilePos = tile.transform.position;
        Vector3 myPos = transform.position;
        if (myPos.x == tilePos.x && (tilePos.z == myPos.z + 1 || tilePos.z == myPos.z - 1))
        {
            return true;
        }
        else if (myPos.z == tilePos.z && (tilePos.x == myPos.x + 1 || tilePos.x == myPos.x - 1))
        {
            return true;
        }

        return false;
    }

    //when trying to move to wrong direction
    public void Shake()
    {
        ingredientsGroup?.transform.DOShakeRotation(0.3f, new Vector3(20, 0), 10, 50).OnComplete(() => ingredientsGroup.transform.rotation = Quaternion.identity);

    }

    //When the ingredients of this tile has moved to another one 
    private void IngredientsMoved(Tile tile)
    {
        if (tile == this)
        {
            ingredients.Clear();

            //needs to return to the original position because it was used to move the ingredients to another tile 
            ingredientsGroup.transform.localPosition = new Vector3(0, 0.15f, 0);
            ingredientsGroup.transform.DORotate(new Vector3(0, 0, 0), 0f);

            RemoveIngredients();
            dragHandler.onMovedPieces -= IngredientsMoved;
        }
    }
    #endregion

}
