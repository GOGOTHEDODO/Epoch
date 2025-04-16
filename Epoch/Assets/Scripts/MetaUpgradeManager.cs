using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MetaUpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI starPartsText;

    public Button moveSpeedButton;
    public Button attackDamageButton;
    public Button attackSpeedButton;
    public Button maxHealthButton;
    public Button knockbackButton;
    public Button dashQuantityButton;
    public Button luckButton;
    public Button critDamageButton;
    public Button critRateButton;

    public GameObject panelRoot;

    private void Start()
    {
        AssignButtonListeners();
        UpdateUI();
    }

    public void OpenPanel()
    {
        if (panelRoot != null)
        {
            panelRoot.SetActive(true);
        }

        UpdateUI(); // Always refresh UI when panel is opened
    }

    public void UpdateUI()
    {
        if (starPartsText != null)
        {
            starPartsText.text = "Star Parts: " + GameManager.instance.metaUpgrades.starParts;
        }
    }

    private void AssignButtonListeners()
    {
        if (moveSpeedButton != null)
        {
            moveSpeedButton.onClick.RemoveAllListeners();
            moveSpeedButton.onClick.AddListener(UpgradeMoveSpeed);
        }

        if (attackDamageButton != null)
        {
            attackDamageButton.onClick.RemoveAllListeners();
            attackDamageButton.onClick.AddListener(UpgradeAttackDamage);
        }

        if (attackSpeedButton != null)
        {
            attackSpeedButton.onClick.RemoveAllListeners();
            attackSpeedButton.onClick.AddListener(UpgradeAttackSpeed);
        }

        if (maxHealthButton != null)
        {
            maxHealthButton.onClick.RemoveAllListeners();
            maxHealthButton.onClick.AddListener(UpgradeMaxHealth);
        }

        if (knockbackButton != null)
        {
            knockbackButton.onClick.RemoveAllListeners();
            knockbackButton.onClick.AddListener(UpgradeKnockback);
        }

        if (dashQuantityButton != null)
        {
            dashQuantityButton.onClick.RemoveAllListeners();
            dashQuantityButton.onClick.AddListener(UpgradeDashQuantity);
        }

        if (luckButton != null)
        {
            luckButton.onClick.RemoveAllListeners();
            luckButton.onClick.AddListener(UpgradeLuck);
        }

        if (critDamageButton != null)
        {
            critDamageButton.onClick.RemoveAllListeners();
            critDamageButton.onClick.AddListener(UpgradeCritDamage);
        }

        if (critRateButton != null)
        {
            critRateButton.onClick.RemoveAllListeners();
            critRateButton.onClick.AddListener(UpgradeCritRate);
        }
    }

    public void UpgradeMoveSpeed()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.basePlayerSpeed += 0.25f;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

    public void UpgradeAttackDamage()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.basePlayerDamage += 1f;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.baseAttackCoolDown *= 0.95f;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

    public void UpgradeMaxHealth()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.baseMaxHealth += 10f;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

    public void UpgradeKnockback()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.baseKnockback += 0.015f;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

    public void UpgradeDashQuantity()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.baseDashQuantity += 1;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

    public void UpgradeLuck()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.baseLuck += 1;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

    public void UpgradeCritDamage()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.baseCritDamage += 0.1f;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

    public void UpgradeCritRate()
    {
        if (GameManager.instance.metaUpgrades.starParts > 0)
        {
            GameManager.instance.metaUpgrades.baseCritRate += 0.02f;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }
}