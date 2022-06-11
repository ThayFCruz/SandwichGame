using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridSO normalLevels;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GridSO savedLevelsLayout;

    private Mode mode;
    private int savedLevelsCount = 0;
    private int normalLevelsCount = 0;

    private int currentSavedLevel = 0;
    private int currentNormalLevel = 0;
    private LevelLayout currentGameLayout;

    enum Mode
    {
        NORMAL,
        RANDOM,
        SAVED
    }

    private void Start()
    {
        gridManager.CreateEmptyGrid();
        GetSavedLayoutsCount();
        GetNormalLayoutsCount();
    }

    public void RestartGame()
    {
        gridManager.CleanGrid();
        NewGameWithSO();
    }

    public void InitNormalGame()
    {
        mode = Mode.NORMAL;
        NewGameWithSO();
    }
    public void NewGameWithSO()
    {
        gridManager.CleanGrid();
        switch (mode)
        {
            case Mode.NORMAL:
                currentGameLayout = normalLevels.levelLayouts[currentNormalLevel];
                break;
            case Mode.SAVED:
                currentGameLayout = savedLevelsLayout.levelLayouts[currentSavedLevel];
                break;
        }
        
        gridManager.FillGridWithSO(currentGameLayout);
    }

    public void NewRandomGame()
    {
        gridManager.CleanGrid();
        mode = Mode.RANDOM;
        currentGameLayout = gridManager.FillGridRandomly();
    }

    public void WinGame()
    {
        gridManager.CleanGrid();

        switch (mode)
        {
            case Mode.SAVED:
                if (currentSavedLevel <= savedLevelsLayout.levelLayouts.Count - 1)
                {
                    currentSavedLevel++;
                    NewGameWithSO();
                }
                break;
            case Mode.NORMAL:
                if (currentNormalLevel <= normalLevels.levelLayouts.Count - 1)
                {
                    currentNormalLevel++;
                    NewGameWithSO();
                }  
                break;
            case Mode.RANDOM:
                NewRandomGame();
                break;
        }
    }

    public void SaveLayout()
    {
        savedLevelsLayout.levelLayouts.Add(currentGameLayout);
        GetSavedLayoutsCount();
    }

    public void LoadSavedLayout()
    {
        mode = Mode.SAVED;
        gridManager.CleanGrid();        
        if (savedLevelsCount != 0)
        {
            currentGameLayout = savedLevelsLayout.levelLayouts[0];
            RestartGame();
        }
    }

    public bool NextSavedLevel()
    {
        if (mode != Mode.SAVED) return false;
        if(currentSavedLevel >= savedLevelsCount-1)
        {
            return false;
        }
        else {
            currentSavedLevel++;
            LoadSavedLayout();
            return true;
        }
    }

    public bool PreviousSavedLevel()
    {
        if (mode != Mode.SAVED) return false;
        if (currentSavedLevel <= 0)
        {
            return false;
        }
        else
        {
            currentSavedLevel--;
            LoadSavedLayout();
            return true;
        }
    }

    public int GetSavedLayoutsCount()
    {
        savedLevelsCount = savedLevelsLayout.levelLayouts.Count;
        return savedLevelsCount;
    }

    public int GetNormalLayoutsCount()
    {
        normalLevelsCount = normalLevels.levelLayouts.Count;
        return normalLevelsCount;
    }
}
