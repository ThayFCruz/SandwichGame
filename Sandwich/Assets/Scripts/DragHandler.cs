using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
{
    public static DragHandler Instance;
    [SerializeField] GridManager gridManager;
    public bool canFlip = false;
    public Tile startingTile;

    public event Action onMovedPieces = null;

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
        startingTile.CleanTile();
        onMovedPieces?.Invoke();
    }
}
