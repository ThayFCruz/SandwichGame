using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridSO normalLevels;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GridSO savedLevelsLayout;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private int ingredientsQuantity = 4;

    private Mode mode;
    private int savedLevelsCount = 0;
    private int normalLevelsCount = 0;

    private int currentSavedLevel = 0;
    private int currentNormalLevel = 0;
    private LevelLayout currentGameLayout;

    public bool IsRandomMode => mode == Mode.RANDOM;

    public enum Mode
    {
        NORMAL,
        RANDOM,
        SAVED
    }

    private void Start()
    {
        ingredientsQuantity = PlayerPrefs.GetInt("ingredientsQuantity", 4);
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
        currentNormalLevel = PlayerPrefs.GetInt("normalLevel", 0);
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
        
        gridManager.FillGridWithSO(currentGameLayout, ingredientsQuantity);
    }

    public void NewRandomGame()
    {
        gridManager.CleanGrid();
        mode = Mode.RANDOM;
        currentGameLayout = gridManager.FillGridRandomly(ingredientsQuantity);
    }

    public bool WinGame()
    {
        switch (mode)
        {
            case Mode.SAVED:
                if (currentSavedLevel < savedLevelsLayout.levelLayouts.Count - 1)
                {
                    currentSavedLevel++;
                    return true;
                }
                return false;
            case Mode.NORMAL:
                if (currentNormalLevel < normalLevels.levelLayouts.Count - 1)
                {
                    currentNormalLevel++;
                    PlayerPrefs.SetInt("normalLevel", currentNormalLevel);
                    return true;
                }
                currentNormalLevel = 0;
                PlayerPrefs.SetInt("normalLevel", 0);
                return false;
            case Mode.RANDOM:
                return true;
        }
        return false;
    }

    public void NewGame()
    {
        if (IsRandomMode)
        {
            NewRandomGame();
        }
        else
        {
            NewGameWithSO();
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
            currentSavedLevel = 0;
            NewGameWithSO();
        }
    }

    public void NextSavedLevel()
    {
        currentSavedLevel++;
        NewGameWithSO();
        CheckNextButton();
        CheckPreviousButton();
    }

    public void PreviousSavedLevel()
    { 
        currentSavedLevel--;
        NewGameWithSO();
        CheckPreviousButton();
        CheckNextButton();
    }

    public bool CheckPreviousButton()
    {
        bool status = true;
        if(currentSavedLevel <= 0 || mode != Mode.SAVED )
        {
            status = false;
        }

        uiManager.UpdatePrevioustButton(status);
        return status;
    }

    public bool CheckNextButton()
    {
        bool status = true;
        if (currentSavedLevel >= savedLevelsCount - 1 || mode != Mode.SAVED)
        {
            status = false;
        }

        uiManager.UpdateNextButton(status);
        return status;
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

    public void SetIngredientsQuantity(int value)
    {
        ingredientsQuantity = value;
        PlayerPrefs.SetInt("ingredientsQuantity", value);
    }

    public bool CanSaveLevel()
    {
        if (mode != Mode.SAVED) return true;

        return false;
    }
}
