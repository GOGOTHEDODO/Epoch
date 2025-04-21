using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LegendaryUpgradeUI : MonoBehaviour
{
    public static LegendaryUpgradeUI instance;
    public GameObject panelRoot;
    public TextMeshProUGUI titleText;
    public Button upgradeButton1;
    public Button upgradeButton2;
    public TextMeshProUGUI button1Text;
    public TextMeshProUGUI button2Text;

    private UpgradeData chosenUpgrade1;
    private UpgradeData chosenUpgrade2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // prevent duplicates if using DontDestroyOnLoad
        }
        panelRoot.SetActive(false);
    }


    public void ShowLegendaryChoices(UpgradeData option1, UpgradeData option2)
    {
        Time.timeScale = 0f; // Pause game

        panelRoot.SetActive(true);
        titleText.text = "Choose a Legendary Upgrade";

        chosenUpgrade1 = option1;
        chosenUpgrade2 = option2;

        button1Text.text = option1.upgradeName + ":\n" + option1.description;
        button2Text.text = option2.upgradeName + ":\n" + option1.description;

        upgradeButton1.onClick.RemoveAllListeners();
        upgradeButton2.onClick.RemoveAllListeners();

        upgradeButton1.onClick.AddListener(() => ChooseUpgrade(chosenUpgrade1));
        upgradeButton2.onClick.AddListener(() => ChooseUpgrade(chosenUpgrade2));
    }


    // Update is called once per frame
    void ChooseUpgrade(UpgradeData upgrade)
    {
        GameManager.instance.ApplyUpgrade(upgrade);
        ConfirmChoice();
    }

    public void ConfirmChoice()
    {
        Time.timeScale = 1f;

        if (panelRoot != null)
            panelRoot.SetActive(false);

        // Continue to next level after legendary choice
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.LoadNextLevelAfterLegendary();
        }
        else
        {
            Debug.LogError("Could not find LevelManager to continue the game after legendary upgrade.");
        }
    }
}
