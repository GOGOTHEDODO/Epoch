using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public UpgradeType type;
    public float value;

    public enum UpgradeType
    {
        MoveSpeed,
        AttackDamage,
        AttackCoolDown
    }

    public void ApplyUpgrade(PlayerMovement playerMovement, LightAttackControl lightAttack)
    {
        switch(type)
        {
            case UpgradeType.MoveSpeed:
                playerMovement.IncreaseMovementSpeed(value);
                break;
            case UpgradeType.AttackDamage:
                lightAttack.IncreaseDamage(value);
                break;
            case UpgradeType.AttackCoolDown:
                lightAttack.DecreaseCoolDown(value);
                break;
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
