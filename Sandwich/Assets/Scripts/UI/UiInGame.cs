using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInGame : MonoBehaviour
{
    [SerializeField] UiManager uiManager;
    [SerializeField] Button restartButton;
    [SerializeField] Button saveLayout;
    [SerializeField] Button nextLayout;
    [SerializeField] Button previousLayout;
    [SerializeField] GameObject overlay;
    [SerializeField] Button pauseButton;
    [SerializeField] Button startButton;
    void Start()
    {
        overlay.SetActive(false);
        restartButton.onClick.AddListener(() => RestartGame());
        saveLayout.onClick.AddListener(() => SaveCurrentLevelButton());
        nextLayout.onClick.AddListener(() => NextLayout());
        previousLayout.onClick.AddListener(() => PreviousLayout());
        startButton.onClick.AddListener(() => StartSavedGame());
        pauseButton.onClick.AddListener(() => PauseGame());
    }

    #region Saving Game
    private void SaveCurrentLevelButton()
    {
        uiManager.SaveCurrentGame();
        saveLayout.gameObject.SetActive(false);
    }

    public void UpdateSaveButton(bool status)
    {
        saveLayout.gameObject.SetActive(status);
    }

    #endregion

    private void RestartGame()
    {
        uiManager.RestartGame();
    }

    private void PauseGame()
    {
        uiManager.ShowPauseScreen();
    }

    #region loading games and scrolling the saved layouts list

    public void ShowLoadedGame(int savedLevels)
    {
        overlay.SetActive(true);
        startButton.gameObject.SetActive(true);
        nextLayout.gameObject.SetActive(savedLevels > 1);
    }
    private void NextLayout()
    {
        uiManager.NextLayout();
    }

    public void UpdateNextButton(bool status)
    {
        nextLayout.gameObject.SetActive(status);
    }

    private void PreviousLayout()
    {
        uiManager.PreviousLayout();
    }
    public void UpdatePreviousButton(bool status)
    {
        previousLayout.gameObject.SetActive(status);
    }

    //when the player decides to play one of teh saved games
    private void StartSavedGame()
    {
        uiManager.changeGameInteraction(true);
        overlay.SetActive(false);
        previousLayout.gameObject.SetActive(false);
        nextLayout.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    #endregion
}
