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
    private VerticalLayoutGroup layoutGroup;
    private Color commonColor = Color.gray;
    private Color rareColor = new Color(0.3f, 0.5f, 1f);//Blue
    private Color epicColor = new Color(0.6f, 0.2f, 0.8f);//Purple

    
    // Start is called before the first frame update
    void Start()
    {
        layoutGroup = buttonContainer.GetComponent<VerticalLayoutGroup>();
    }


    public void ShowUpgrade()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if(playerMovement != null)
        {
            playerMovement.enableMovement(false);
        }
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

        float luck = GameManager.instance.currentLuck;
        HashSet<UpgradeData> selectedUpgrades = new HashSet<UpgradeData>();
        int attempts = 0;
        while(selectedUpgrades.Count < 3 && attempts < 50)
        {
            Rarity rolledRarity = DetermineRarity(luck);
            
            List<UpgradeData> validUpgrades = availableUpgrades.FindAll(u => 
            u.minimumRarity <= rolledRarity && 
            !selectedUpgrades.Contains(u) && 
            (!u.isOneTimeUpgrade || (u.isOneTimeUpgrade && !u.hasBeenChosen)));

            if(validUpgrades.Count == 0)
            {
                attempts++;
                continue;
            }
            UpgradeData chosen = validUpgrades[Random.Range(0, validUpgrades.Count)];
            chosen.rarity = rolledRarity;
            chosen.ApplyRarityModifier();
            selectedUpgrades.Add(chosen);
        }

        foreach(UpgradeData upgrade in selectedUpgrades)
        {
            GameObject buttonObj = Instantiate(upgradeButtonPrefab, buttonContainer);
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            Button button = buttonObj.GetComponent<Button>();

            if(button !=null)
            {
                //get the rarity color and attach it to the buttons.

                Color rarityColor = GetRarityColor(upgrade.rarity);

                button.GetComponent<Image>().color = rarityColor; 
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
            {
                Debug.Log($"Clicked on {upgrade.upgradeName}!");
                ApplyUpgrade(upgrade);
            });  

            } 
            if(upgrade.type == UpgradeData.UpgradeType.DashQuantity 
            || upgrade.type == UpgradeData.UpgradeType.Luck 
            || upgrade.type == UpgradeData.UpgradeType.HealPlayer
            || upgrade.type == UpgradeData.UpgradeType.MaxHealthBoost)
            {
                buttonText.text = $"{upgrade.upgradeName} {upgrade.description} (+{upgrade.currentValue})";
            } 
            else if (upgrade.type == UpgradeData.UpgradeType.FireSword)
            {
                buttonText.text = $"{upgrade.upgradeName} {upgrade.description}";
            }
            else 
            {
                buttonText.text = $"{upgrade.upgradeName} {upgrade.description} (+{upgrade.currentValue}%)";           
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(buttonContainer.GetComponent<RectTransform>());

    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.ApplyUpgrade(upgrade);
        }

        if (upgradeOrb != null)
        {
            upgradeOrb.UpgradeChosen();
        }

        if(upgrade.isOneTimeUpgrade)
        {
            upgrade.hasBeenChosen = true;
        }

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.enableMovement(true);
        }

        gameObject.SetActive(false);
    }

    private Rarity DetermineRarity(float luck)
    {
        //determine what level of upgrade that the current upgrade will be and update that buttons rarity for upgradeData 
        //so we can change the amount of bonus that it gives.
        float roll = Random.Range(0f, 100f) + luck;
        if(roll >= 90) return Rarity.Epic; 
        if(roll >= 50) return Rarity.Rare;
        return Rarity.Common;
    }

    private Color GetRarityColor(Rarity rarity)
    {
        switch(rarity)
        {
            case Rarity.Rare: return rareColor;
            case Rarity.Epic: return epicColor;
            default: return commonColor;

        }
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
