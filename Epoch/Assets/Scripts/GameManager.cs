using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
     // Persistent player stats
    public float playerSpeed = 5f;
    public float playerDamage = 10f;
    public float attackCooldown = 0.5f;

    public float maxHealth = 100f;
    public float currentHealth = 100f;

    private float basePlayerSpeed = 5f;
    private float basePlayerDamage = 10f;
    private float baseAttackCoolDown = 0.5f;
    private float baseMaxHealth = 100f;
    private float baseCurrentHealth = 100f;

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

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        playerDamage = basePlayerDamage;
        playerSpeed =  basePlayerSpeed;
        attackCooldown = baseAttackCoolDown;
        maxHealth = baseMaxHealth;
        currentHealth = baseCurrentHealth;

        SceneManager.LoadScene(0);
    }

    public void SavePlayerHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }

     // Method to apply upgrades globally
    public void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeData.UpgradeType.MoveSpeed:
                playerSpeed += playerSpeed * (upgrade.value / 100);
                break;
            case UpgradeData.UpgradeType.AttackDamage:
                playerDamage += playerDamage * (upgrade.value / 100);
                break;
            case UpgradeData.UpgradeType.AttackCoolDown:
                attackCooldown *= (1 - upgrade.value / 100); // Reduce cooldown
                break;
        }

        Debug.Log($"Applied {upgrade.upgradeName} - New Stats -> Speed: {playerSpeed}, Damage: {playerDamage}, Cooldown: {attackCooldown}, CurrentHealth: {currentHealth}");
    }
}
