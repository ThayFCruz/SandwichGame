using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridSO levelSO;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GridSO savedLayouts;
    private bool isRandomMode = false;
    private int level = 0;
    private LevelLayout currentGameLayout;
    private int savedLevels = 0;
    private int currentSavedLevel = 0;

    private void Start()
    {
        gridManager.CreateEmptyGrid();
        NewRandomGame();
    }
    private void RestartGame()
    {
        gridManager.CleanGrid();
        NewGameWithSO();
    }

    private void NewGameWithSO()
    {
        if (!isRandomMode)
        {
            currentGameLayout = levelSO.levelLayouts[level];
        }
        gridManager.FillGridWithSO(currentGameLayout);
        
    }

    private void NewRandomGame()
    {
        isRandomMode = true;
        currentGameLayout = gridManager.FillGridRandomly();
    }

    public void WinGame()
    {
        if (!isRandomMode)
        {
            if(level <= levelSO.levelLayouts.Count -1)
            level++;
            gridManager.CleanGrid();
            NewGameWithSO();
        }
        else
        {
            gridManager.CleanGrid();
            NewRandomGame();
        }
    }

    private void SaveLayout()
    {
        savedLayouts.levelLayouts.Add(currentGameLayout);
        savedLevels = savedLayouts.levelLayouts.Count;
    }

    private void LoadSavedLayout()
    {
        if (savedLevels != 0)
        {
            currentGameLayout = savedLayouts.levelLayouts[currentSavedLevel];
            RestartGame();
        }
    }

    private void NextSavedLevelButton()
    {
        if(currentSavedLevel >= savedLevels-1)
        {

        }
        else {
            currentSavedLevel++;
            LoadSavedLayout();
        }
       
    }
}
