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

    //both used to manage the lists of saved and normal game layouts 
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

    //restart current level
    public void RestartGame()
    {
        gridManager.CleanGrid();
        NewGameWithSO();
    }

    //new game in normal mode
    public void InitNormalGame()
    {
        mode = Mode.NORMAL;
        currentNormalLevel = PlayerPrefs.GetInt("normalLevel", 0);
        NewGameWithSO();
    }

    //new game in random mode
    public void NewRandomGame()
    {
        gridManager.CleanGrid();
        mode = Mode.RANDOM;
        currentGameLayout = gridManager.FillGridRandomly(ingredientsQuantity);
    }

    //new game in saved mode
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

    //fill grid with a layout already set in SO
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

    //manage the win in each game mode
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

    //when the player choose to continue playing after winning a level
    public void NewGameAfterWinning()
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

    //when the player asks to save the current level
    public void SaveLayout()
    {
        savedLevelsLayout.levelLayouts.Add(currentGameLayout);
        GetSavedLayoutsCount();
    }


    //both methods behind are used by the player when scrolling through saved games list
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

    //both used to manage next and previous buttons availability
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

    //when the player choose the qtt of ingredients in random levels
    public void SetIngredientsQuantity(int value)
    {
        ingredientsQuantity = value;
        PlayerPrefs.SetInt("ingredientsQuantity", value);
    }

    //making the player unable to save an already saved level
    public bool CanSaveLevel()
    {
        if (mode != Mode.SAVED) return true;

        return false;
    }
}
