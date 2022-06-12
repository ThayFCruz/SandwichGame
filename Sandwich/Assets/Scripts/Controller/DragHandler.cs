using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
{
    public static DragHandler Instance;
    public bool canFlip = false;
    public Tile startingTile;
    public bool interactable;
    public event Action<Tile> onMovedPieces = null;

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

    //everytime that occurs a flip of ingredients
    public void PiecesMoved()
    {
        onMovedPieces?.Invoke(startingTile);
        canFlip = false;
        startingTile = null;
    }

    //when the player try to move to a wrong tile
    public void CantMove()
    {
        startingTile?.Shake();
    }

    //used to display the list of saved levels
    public void SetInteractable(bool status)
    {
        interactable = status;
    }
}
