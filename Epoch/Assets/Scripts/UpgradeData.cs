using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering.Universal;
using UnityEngine;

public enum Rarity
    {
        //added variety for the differnt types of upgrades
        Common, 
        Rare,
        Epic,
        Legendary
    }

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public UpgradeType type;
    public float currentValue;
    public float baseValue;
    public Rarity rarity;
    public Rarity minimumRarity = Rarity.Common;
    public bool isOneTimeUpgrade = false;
    public bool hasBeenChosen = false;

    public enum UpgradeType
    {
        MoveSpeed,
        AttackDamage,
        AttackCoolDown,
        Luck,
        Knockback,
        DashQuantity,
        FireSword,
        HealPlayer,
        MaxHealthBoost,
        DoubleAttack,
        CritDamage,
        LegendaryAttackBuff,
        LegendaySpeedBuff,
        ElectricSword
    }

    public void ResetToBaseValue()
    {
        currentValue = baseValue;
    }
    public void ApplyRarityModifier()
    {
        switch(rarity)
        {
            case Rarity.Common:
                currentValue = baseValue * 0.75f;
                Debug.Log("Common Upgrade");
                if(type == UpgradeType.Luck) 
                    currentValue = 1;
                break;
            case Rarity.Epic:
                Debug.Log("Epic Upgrade");
                currentValue = baseValue * 1.25f;
                if(type == UpgradeType.DashQuantity) currentValue = 1;
                if(type == UpgradeType.Luck) currentValue = 3;
                break;
            default:
                Debug.Log("Rare Upgrade");
                currentValue = baseValue;
                if(type == UpgradeType.Luck)
                    currentValue = 2;
                break;
        }
    }

    public void ApplyUpgrade(PlayerMovement playerMovement, LightAttackControl lightAttack)
    {

        if(GameManager.instance != null)
        {
            //upgrades are handled through the GameManager for persistance.
            GameManager.instance.ApplyUpgrade(this);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
