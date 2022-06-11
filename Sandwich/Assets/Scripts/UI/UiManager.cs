using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] Button restartButton;
    [SerializeField] Button saveLayout;
    [SerializeField] Button loadLayout;
    [SerializeField] Button nextLayout;
    [SerializeField] Button previousLayout;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(() => levelManager.RestartGame());
        saveLayout.onClick.AddListener(() => SaveButton());
        loadLayout.onClick.AddListener(() => LoadButton());
        nextLayout.onClick.AddListener(() => nextLayout.gameObject.SetActive(levelManager.NextSavedLevel()));
        previousLayout.onClick.AddListener(() => previousLayout.gameObject.SetActive(levelManager.PreviousSavedLevel()));
    }

    private void LoadButton()
    {
        levelManager.LoadSavedLayout();
        nextLayout.gameObject.SetActive(levelManager.NextSavedLevel());
    }

    private void SaveButton()
    {
        levelManager.SaveLayout();
        saveLayout.gameObject.SetActive(false);
    }
    
}
