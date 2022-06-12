using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private Slider difficultSlider;
    [SerializeField] public CanvasGroup canvas;

    void Start()
    {
        difficultSlider.value = PlayerPrefs.GetInt("ingredientsQuantity",4);
        continueButton.onClick.AddListener(() => ContinueButton());
        menuButton.onClick.AddListener(() => MenuButton());
    }

    private void ContinueButton()
    {
        uiManager.BackPauseScreen((int)difficultSlider.value);
    }

    private void MenuButton()
    {
        uiManager.BackPauseScreen((int)difficultSlider.value);
        uiManager.ShowStartScreen();
    }

    //defines if the slider of ingredients quantity shoul be visible (only visible in random mode)
    public void SetDifficultOption(bool status)
    {
        difficultSlider.gameObject.SetActive(status);
    }
}
