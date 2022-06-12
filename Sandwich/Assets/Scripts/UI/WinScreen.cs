using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backButton;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private GameObject congratualtionsMessage;
    [SerializeField] public CanvasGroup canvas;

    void Start()
    {
        continueButton.onClick.AddListener(() => ContinueButton());
        backButton.onClick.AddListener(() => BackButton());
    }

    //continue to next lvel
    private void ContinueButton()
    {
        uiManager.AfterWinningGame();
    }

    //back to menu
    private void BackButton()
    {
        uiManager.CloseWindow(canvas);
        uiManager.ShowStartScreen();
    }

    //deefines if exists more levels to continue or not
    public void SetContinueButton(bool status)
    {
        continueButton.gameObject.SetActive(status);
        congratualtionsMessage.gameObject.SetActive(!status);
    }


}
