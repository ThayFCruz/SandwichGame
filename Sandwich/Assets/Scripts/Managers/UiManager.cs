using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    //used to manage the UI interactions between different screens and interact them with level manager
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

    #region Show and close windows region
    public void ShowWinScreen()
    {
        gameUI.UpdateSaveButton(levelManager.CanSaveLevel());
        winScreen.SetContinueButton(levelManager.WinGame());
        ShowWindow(winScreen.canvas);
    }

    public void ShowStartScreen()
    {
        CheckSavedLayoutsButton();
        ShowWindow(startScreen.canvas);
    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetDifficultOption(levelManager.IsRandomMode);
        ShowWindow(pauseScreen.canvas);
    }

    //after returning back to game from pause screen
    public void BackPauseScreen(int ingredientsQuantity)
    {
        levelManager.SetIngredientsQuantity(ingredientsQuantity);
        CloseWindow(pauseScreen.canvas);
    }

    public void CloseWindow(CanvasGroup canvas)
    {
        fade.StartFade(true, canvas);
    }

    public void ShowWindow(CanvasGroup canvas)
    {
        fade.StartFade(false, canvas);
    }


    #endregion

    #region new game in different modes

    //new game after the victory
    public void AfterWinningGame()
    {
        levelManager.NewGameAfterWinning();
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

    public void RestartGame()
    {
        levelManager.RestartGame();
    }
    #endregion

    #region managing the saving games

    public void SaveCurrentGame()
    {
        levelManager.SaveLayout();
    }
    
    //checking if exists any saved layout to enable this option or not
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

    //removing the player interaction with the grid when seeing the saved layouts list
    public void changeGameInteraction(bool status)
    {
        dragHandler.SetInteractable(status);
    }
    #endregion

   
}
