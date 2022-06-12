using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] Button randomMode;
    [SerializeField] Button savedMode;
    [SerializeField] Button normalMode;
    [SerializeField] UiManager uiManager;
    [SerializeField] public CanvasGroup canvas;

    void Start()
    {
        randomMode.onClick.AddListener(() => RandomMode());
        normalMode.onClick.AddListener(() => NormalMode());
        savedMode.onClick.AddListener(() => SavedMode());
        uiManager.CheckSavedLayoutsButton();
    }

    public void Init(int savedLevels)
    {
        savedMode.interactable = savedLevels > 0;
    }

    private void RandomMode()
    {
        uiManager.RandomGame();
    }

    private void NormalMode()
    {
        uiManager.NormalGame();
    }
    private void SavedMode()
    {
        uiManager.OpenSavedGame();
    }
}
