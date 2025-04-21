using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;
    public GameObject panel;
    public TextMeshProUGUI starPartsText;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);

        gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        if(starPartsText != null)
        {
            starPartsText.text = "Star Parts: " + GameManager.instance.metaUpgrades.starParts;
        }
        gameObject.SetActive(true);
    }
    public void OnMainMenuButton()
    {
        GameManager.instance.SaveMetaData();
        gameObject.SetActive(false);
        GameManager.instance.RestartGame();
    }
}
