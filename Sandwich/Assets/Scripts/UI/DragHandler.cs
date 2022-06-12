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

    public void MovedPieces()
    {
        onMovedPieces?.Invoke(startingTile);
        canFlip = false;
        startingTile = null;
    }

    public void MovedWrong()
    {
        startingTile?.Shake();
    }

    public void SetInteractable(bool status)
    {
        interactable = status;
    }
}
