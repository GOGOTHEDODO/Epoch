using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int currentLevelCount = 0;
    public int maxLevelBeforeBoss = 0;
    public int bossSceneIndex = 5;
    public static GameManager instance;
    // Persistent player stats (upgradeable)
    public float playerSpeed = 5f;
    public float playerDamage = 10f;
    public float attackCooldown = 0.5f;
    public float currentLuck = 0f;
    public float currentKnockback = 0.03f;

    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public int currentDashQuantity = 1;

    //base stats when player is killed
    private float basePlayerSpeed = 5f;
    private float basePlayerDamage = 10f;
    private float baseAttackCoolDown = 0.5f;
    private float baseMaxHealth = 100f;
    private float baseCurrentHealth = 100f;
    private float baseLuck = 0f;
    private float baseKnockback = 0.3f;
    private int baseDashQuantity = 1;

    private List<UpgradeData> allUpgrades = new List<UpgradeData>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }   
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        maxLevelBeforeBoss = Random.Range(4,5);
        ResetUpgrades();
    }

    public void RestartGame()
    {
        currentLevelCount = 0;
        maxLevelBeforeBoss = Random.Range(4,5);
        Debug.Log("Restarting Game...");
        playerDamage = basePlayerDamage;
        playerSpeed =  basePlayerSpeed;
        attackCooldown = baseAttackCoolDown;
        maxHealth = baseMaxHealth;
        currentHealth = baseCurrentHealth;
        currentLuck = baseLuck;
        currentKnockback = baseKnockback;
        CooldownManager.isOtherAttacking = false;
        currentDashQuantity = baseDashQuantity;
        ResetUpgrades();

        SceneManager.LoadScene(0);
    }

    public void SavePlayerHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }

     // Method to apply upgrades globally
    public void ApplyUpgrade(UpgradeData upgrade)
    {
        //Apply the Chosen upgrade which is known through UpgradeData and update the corresponding stat.
        //At the start of each level the stats here are put onto the player 
        switch (upgrade.type)
        {
            case UpgradeData.UpgradeType.MoveSpeed:
                playerSpeed += basePlayerSpeed * (upgrade.currentValue / 100);
                break;
            case UpgradeData.UpgradeType.AttackDamage:
                playerDamage += basePlayerDamage * (upgrade.currentValue / 100);
                break;
            case UpgradeData.UpgradeType.AttackCoolDown:
                attackCooldown *= (1 - upgrade.currentValue / 100); // Reduce cooldown
                break;
            case UpgradeData.UpgradeType.Luck:
                currentLuck +=upgrade.currentValue;
                break;
            case UpgradeData.UpgradeType.Knockback:
                currentKnockback += upgrade.currentValue;
                break;
            case UpgradeData.UpgradeType.DashQuantity:
                currentDashQuantity += (int)upgrade.currentValue;
                break;
        }

        Debug.Log($"Applied {upgrade.upgradeName} - New Stats -> Speed: {playerSpeed}, Damage: {playerDamage}, Cooldown: {attackCooldown}, CurrentHealth: {currentHealth}, CurrentLuck: {currentLuck}");
        upgrade.ResetToBaseValue();
    }

    public void ResetUpgrades()
    {
        foreach(UpgradeData upgrade in allUpgrades)
        {
            Debug.Log($"Resetting {upgrade.upgradeName}");
            upgrade.currentValue = 0;
        }
    }
}
