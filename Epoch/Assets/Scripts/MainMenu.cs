using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public int[] sceneIDs;

    public GameObject settingsPanel;

    // ✅ Add this reference for the meta upgrades panel
    public GameObject metaUpgradePanel;
    public MetaUpgradeManager upgradeManager;

    void Start()
    {
        if (upgradeManager == null)
        {
            GameObject upgradeManagerGO = GameObject.Find("MetaUpgradeManager");
            if (upgradeManagerGO != null)
            {
                upgradeManager = upgradeManagerGO.GetComponent<MetaUpgradeManager>();
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIDs[Random.Range(0, sceneIDs.Length)]);
        CooldownUI.instance.StartCooldown(0.05f);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenMetaUpgrades()
    {
        
    if (metaUpgradePanel != null && upgradeManager != null)
        {
            metaUpgradePanel.SetActive(true);
            upgradeManager.OpenPanel();

            // Set first selected button (optional)
            GameObject firstButton = GameObject.Find("UpgradeSpeed");
            if (firstButton != null)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
        else
        {
            Debug.LogError("❌ MetaUpgradePanel or UpgradeManager not assigned in Inspector!");
        }
    }

    public void CloseMetaUpgrades()
    {
        if (metaUpgradePanel != null)
        {
            metaUpgradePanel.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}