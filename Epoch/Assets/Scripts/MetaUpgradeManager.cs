using UnityEngine;
using TMPro;

public class MetaUpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI starPartsText;

    public void OpenPanel()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        starPartsText.text = "Star Parts: " + GameManager.instance.metaUpgrades.starParts;
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
            GameManager.instance.metaUpgrades.basePlayerDamage += 2f;
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
            GameManager.instance.metaUpgrades.baseMaxHealth += 50f;
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
            GameManager.instance.metaUpgrades.baseLuck += 2;
            GameManager.instance.metaUpgrades.starParts--;
            GameManager.instance.SaveMetaData();
            UpdateUI();
            GameManager.instance.ApplyMetaUpgrades();
        }
    }

}