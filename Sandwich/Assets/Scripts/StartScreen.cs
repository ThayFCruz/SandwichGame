using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] Button randomMode;
    [SerializeField] Button savedMode;
    [SerializeField] Button normalMode;
    private CanvasGroup canvas;
    private Fade fade;

    void Start()
    {
        fade = Fade.Instance;
        canvas = GetComponent<CanvasGroup>();
        randomMode.onClick.AddListener(() => RandomMode());
        normalMode.onClick.AddListener(() => NormalMode());

        savedMode.interactable = levelManager.GetSavedLayoutsCount() > 0;
        savedMode.onClick.AddListener(() => SavedMode());
    }

    private void RandomMode()
    {
        levelManager.NewRandomGame();
        fade.StartFade(true, canvas);

    }

    private void NormalMode()
    {
        levelManager.InitNormalGame();
        fade.StartFade(true,canvas);
    }
    private void SavedMode()
    {
        levelManager.LoadSavedLayout();
        fade.StartFade(true, canvas);
    }

    public void ShowWindow()
    {
        fade.StartFade(false, canvas);
    }
}
