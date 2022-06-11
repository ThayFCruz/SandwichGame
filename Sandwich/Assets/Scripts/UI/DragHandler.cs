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
    }
}
