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
    public CanvasGroup canvas;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        continueButton.onClick.AddListener(() => ContinueButton());
        backButton.onClick.AddListener(() => BackButton());
    }

    private void ContinueButton()
    {
        uiManager.AfterWinningGame();
    }

    private void BackButton()
    {
        uiManager.CloseWindow(canvas);
        uiManager.StartScreen();
    }

    public void SetContinueButton(bool status)
    {
        continueButton.gameObject.SetActive(status);
        congratualtionsMessage.gameObject.SetActive(!status);
    }


}
