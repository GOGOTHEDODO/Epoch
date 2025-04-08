using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int[] sceneIDs;

    public GameObject settingsPanel;

    // ✅ Add this reference for the meta upgrades panel
    public GameObject metaUpgradePanel;
    public MetaUpgradeManager upgradeManager;


    public void StartGame()
    {
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

    // ✅ New function to open the meta upgrades panel
    public void OpenMetaUpgrades()
    {
        if (metaUpgradePanel != null)
        {
            metaUpgradePanel.SetActive(true);
            upgradeManager.OpenPanel();
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