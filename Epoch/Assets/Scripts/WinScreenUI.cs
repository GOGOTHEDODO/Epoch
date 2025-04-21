using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WinScreenUI : MonoBehaviour
{
    public static WinScreenUI instance;
    public GameObject winPanel;
    public Button mainMenuButton;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        winPanel.SetActive(false);

        mainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameManager.instance.RestartGame();
        });
    }

    public void ShowWinScreen()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }
}
