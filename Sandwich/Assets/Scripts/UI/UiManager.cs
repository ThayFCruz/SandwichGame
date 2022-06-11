using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private WinScreen winScreen;
    [SerializeField] private UiInGame gameUI;
    [SerializeField] private PauseScreen pauseScreen;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Fade fade;
    private DragHandler dragHandler;

    private void Start()
    {
        dragHandler = DragHandler.Instance;
    }
    public void WinScreen()
    {
        gameUI.UpdateSaveButton(levelManager.CanSaveLevel());
        winScreen.SetContinueButton(levelManager.WinGame());
        ShowWindow(winScreen.canvas);
    }

    public void StartScreen()
    {
        CheckSavedLayoutsButton();
        ShowWindow(startScreen.canvas);
    }

    public void AfterWinningGame()
    {
        levelManager.NewGame();
        CloseWindow(winScreen.canvas);
    }

    public void OpenSavedGame()
    {
        changeGameInteraction(false);
        gameUI.UpdateSaveButton(false);
        int savedLevels = levelManager.GetSavedLayoutsCount();
        levelManager.LoadSavedLayout();
        CloseWindow(startScreen.canvas);
        gameUI.ShowLoadedGame(savedLevels);
    }

    public void RandomGame()
    {
        levelManager.NewRandomGame();
        CloseWindow(startScreen.canvas);
    }

    public void NormalGame()
    {
        levelManager.InitNormalGame();
        CloseWindow(startScreen.canvas);
    }

    public void PauseScreen()
    {
        pauseScreen.SetDifficultOption(levelManager.IsRandomMode);
        ShowWindow(pauseScreen.canvas);
    }

    public void BackPauseScreen(int ingredientsQuantity)
    {
        levelManager.SetIngredientsQuantity(ingredientsQuantity);
        CloseWindow(pauseScreen.canvas);
    }
  
    public void SaveCurrentGame()
    {
        levelManager.SaveLayout();
    }

    public void RestartGame()
    {
        levelManager.RestartGame();
    }
    
    public void CheckSavedLayoutsButton()
    {
       int savedLevels = levelManager.GetSavedLayoutsCount();
        startScreen.Init(savedLevels);
    }
    public void NextLayout()
    {
        levelManager.NextSavedLevel();
    }

    public void PreviousLayout()
    {
        levelManager.PreviousSavedLevel();
    }

    public void UpdateNextButton(bool status)
    {
        gameUI.UpdateNextButton(status);
    }

    public void UpdatePrevioustButton(bool status)
    {
        gameUI.UpdatePreviousButton(status);
    }

    public void CloseWindow(CanvasGroup canvas)
    {
        fade.StartFade(true, canvas);
    }

    public void ShowWindow(CanvasGroup canvas)
    {
        fade.StartFade(false, canvas);
    }

    public void changeGameInteraction(bool status)
    {
        dragHandler.SetInteractable(status);
    }
}
