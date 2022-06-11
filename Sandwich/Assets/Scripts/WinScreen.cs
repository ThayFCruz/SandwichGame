using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backButton;
    [SerializeField] private StartScreen startScreen;
    private CanvasGroup canvas;
    private Fade fade;

    void Start()
    {
        fade = Fade.Instance;
        canvas = GetComponent<CanvasGroup>();
        continueButton.onClick.AddListener(() => ContinueButton());
        backButton.onClick.AddListener(() => BackButton());
    }

    private void ContinueButton()
    {
        levelManager.WinGame();
        fade.StartFade(true, canvas);
    }

    private void BackButton()
    {
        startScreen.ShowWindow();
        fade.StartFade(true, canvas);
    }

    public void ShowWindow()
    {
       fade.StartFade(false, canvas);
    }

}
