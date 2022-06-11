using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridSO gridSo;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GridSO savedLayouts;
    private bool isRandomMode = false;
    private int level = 0;
    private LevelLayout currentGameLayout;
    private int savedLevels = 0;
    private int currentSavedLevel = 0;

    private void RestartGame()
    {
        gridManager.CleanGrid();
        NewGameWithSO();
    }

    private void NewGameWithSO()
    {
        if (!isRandomMode)
        {
            currentGameLayout = gridSo.levelLayouts[level];
        }
        gridManager.FillGridWithSO(currentGameLayout);
        
    }

    private void NewRandomGame()
    {
        currentGameLayout = gridManager.FillGridRandomly();
    }

    private void WinGame()
    {
        gridManager.CleanGrid();
        if (!isRandomMode)
        {
            level++;
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
