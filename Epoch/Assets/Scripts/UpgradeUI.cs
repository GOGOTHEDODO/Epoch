using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeUI : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public LightAttackControl LightAttackControl;
    public GameObject upgradePanel;
    public GameObject upgradeChoices;
    public GameObject upgradeButtonPrefab;
    public Transform buttonContainer;
    public List<UpgradeData> availableUpgrades;

    private UpgradeOrb upgradeOrb;

    //public float speedIncreasePercentage = 10f; // % increase
    //public float damageIncreasePercentage = 15f;
    //public float attackCoolDownPercentage = 15f;

    
    // Start is called before the first frame update
    void Start()
    {
     
    }


    public void ShowUpgrade()
    {
        upgradePanel.SetActive(true);
        upgradeChoices.SetActive(true);
        GenerateUpgradeButtons();
    }

    public void SetUpgradeOrb(UpgradeOrb orb)
    {
        upgradeOrb = orb;
    }

    private void GenerateUpgradeButtons()
    {
        Debug.Log("Gnerating Buttons");
        foreach(Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        if(availableUpgrades.Count == 0)
        {
            Debug.Log("No Upgrades");
        }

        List<UpgradeData> selectedUpgrades = new List<UpgradeData>();
        while(selectedUpgrades.Count < 3 && availableUpgrades.Count > 0)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[randomIndex]);
        }

        foreach(UpgradeData upgrade in selectedUpgrades)
        {
            GameObject buttonObj = Instantiate(upgradeButtonPrefab, buttonContainer);
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            buttonText.text = $"{upgrade.upgradeName} (+{upgrade.value}%)";

            Button button = buttonObj.GetComponent<Button>();
            if(button !=null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
            {
                Debug.Log($"Clicked on {upgrade.upgradeName}!");
                ApplyUpgrade(upgrade);
            });
                
            }
            
        }
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.ApplyUpgrade(upgrade);
        }
    //    upgrade.ApplyUpgrade(playerMovement, LightAttackControl);
    //    upgradePanel.SetActive(false);
    //    upgradeChoices.SetActive(false);

    //    if (upgradeOrb != null)
    //     {
    //         upgradeOrb.UpgradeChosen();
    //     }

        gameObject.SetActive(false);
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
