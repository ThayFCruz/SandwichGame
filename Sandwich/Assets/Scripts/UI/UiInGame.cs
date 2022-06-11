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
        saveLayout.onClick.AddListener(() => SaveButton());
        nextLayout.onClick.AddListener(() => NextLayout());
        previousLayout.onClick.AddListener(() => PreviousLayout());
        startButton.onClick.AddListener(() => StartSavedGame());
        pauseButton.onClick.AddListener(() => PauseGame());
    }

    private void SaveButton()
    {
        uiManager.SaveCurrentGame();
        saveLayout.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        uiManager.RestartGame();
    }

    private void NextLayout()
    {
        uiManager.NextLayout();
    }

    private void PreviousLayout()
    {
        uiManager.PreviousLayout();
    }

    private void StartSavedGame()
    {
        uiManager.changeGameInteraction(true);
        overlay.SetActive(false);
        previousLayout.gameObject.SetActive(false);
        nextLayout.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        uiManager.PauseScreen();
    }

    public void ShowLoadedGame(int savedLevels)
    {
        overlay.SetActive(true);
        startButton.gameObject.SetActive(true);
        nextLayout.gameObject.SetActive(savedLevels > 1);
    }

    public void UpdateNextButton(bool status)
    {
        nextLayout.gameObject.SetActive(status);
    }
    public void UpdatePreviousButton(bool status)
    {
        previousLayout.gameObject.SetActive(status);
    }

    public void UpdateSaveButton(bool status)
    {
        saveLayout.gameObject.SetActive(status);
    }
}
