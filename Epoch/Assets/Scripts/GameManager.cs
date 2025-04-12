using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    public bool inWorldTwo = false;
    public List<UpgradeData> allUpgrades;

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
    public bool hasFireSword = false;
    public bool hasDoubleAttack = false;

    //base stats when player is killed

    [System.Serializable]
    public class MetaUpgradeData
    {
        public int starParts = 0;
        public float basePlayerSpeed = 5f;
        public float basePlayerDamage = 10f;
        public float baseAttackCoolDown = 0.5f;
        public float baseMaxHealth = 100f;
        public float baseCurrentHealth = 100f;
        public float baseLuck = 0f;
        public float baseKnockback = 0.03f;
        public int baseDashQuantity = 1;
    }
    public MetaUpgradeData metaUpgrades = new MetaUpgradeData();


    //private List<UpgradeData> allUpgrades = new List<UpgradeData>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMetaData();
        }   
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        maxLevelBeforeBoss = Random.Range(6,9);
        metaUpgrades.starParts = 30;
        ApplyMetaUpgrades();
        ResetUpgrades();
    }

     public void ApplyMetaUpgrades()
    {
        playerSpeed = metaUpgrades.basePlayerSpeed;
        playerDamage = metaUpgrades.basePlayerDamage;
        attackCooldown = metaUpgrades.baseAttackCoolDown;
        maxHealth = metaUpgrades.baseMaxHealth;
        currentHealth = metaUpgrades.baseMaxHealth;
        currentLuck = metaUpgrades.baseLuck;
        currentKnockback = metaUpgrades.baseKnockback;
        currentDashQuantity = metaUpgrades.baseDashQuantity;
    }

    public void RestartGame()
    {
        currentLevelCount = 0;
        maxLevelBeforeBoss = Random.Range(6,9);
        Debug.Log("Restarting Game...");
        ApplyMetaUpgrades();
        CooldownManager.isOtherAttacking = false;
        hasFireSword = false;
        hasDoubleAttack = false;
        ResetUpgrades();
        metaUpgrades.starParts = 30;
        SceneManager.LoadScene(0);
    }

    public void SavePlayerHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }

     // Method to apply upgrades globally
    public void ApplyUpgrade(UpgradeData upgrade)
    {
        PlayerHealth ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //Apply the Chosen upgrade which is known through UpgradeData and update the corresponding stat.
        //At the start of each level the stats here are put onto the player 
        switch (upgrade.type)
        {
            case UpgradeData.UpgradeType.MoveSpeed:
                playerSpeed += metaUpgrades.basePlayerSpeed * (upgrade.currentValue / 100);
                break;
            case UpgradeData.UpgradeType.AttackDamage:
                playerDamage += metaUpgrades.basePlayerDamage * (upgrade.currentValue / 100);
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
            case UpgradeData.UpgradeType.FireSword:
                hasFireSword = true;
                break;
            case UpgradeData.UpgradeType.HealPlayer:
                currentHealth += upgrade.currentValue;
                if(currentHealth > maxHealth)
                   currentHealth = maxHealth;
                ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
                if (ph != null) ph.SetHealth(currentHealth);
                
                break;
            case UpgradeData.UpgradeType.MaxHealthBoost:
                maxHealth += upgrade.currentValue;

                ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.SetMaxHealth(maxHealth);
                    ph.SetHealth(currentHealth); // Optional: also update bar visually
                }
                break;
            case UpgradeData.UpgradeType.DoubleAttack:
                hasDoubleAttack = true;
                break;
        }

        Debug.Log($"Applied {upgrade.upgradeName} - New Stats -> Speed: {playerSpeed}, Damage: {playerDamage}, Cooldown: {attackCooldown}, CurrentHealth: {currentHealth}, CurrentLuck: {currentLuck}");
        upgrade.ResetToBaseValue();
    }

    public void ResetUpgrades()
    {
        //UpgradeData[] upgrades = Resources.LoadAll<UpgradeData>("");

        foreach (UpgradeData upgrade in allUpgrades)
        {
            upgrade.currentValue = 0;
            upgrade.hasBeenChosen = false;
        }

        Debug.Log("All upgrades have been reset.");
    }

   

    //Meta Upgrade Persistence
    [System.Serializable]
    class SaveData
    {
        public MetaUpgradeData meta;
    }

    public void SaveMetaData()
    {
        SaveData data = new SaveData {meta = metaUpgrades};
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/meta_upgrades.json", json);
        Debug.Log("Saving meta upgrades to: " + Application.persistentDataPath);
    }

    public void LoadMetaData()
    {
        string path = Application.persistentDataPath + "/meta_upgrades.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            metaUpgrades = data.meta;
            Debug.Log("Loading meta upgrades from: " + Application.persistentDataPath);
        }
        else 
        {
            Debug.Log("No meta upgrade save file found. Using Defaults.");
        }
    }

     private void OnApplicationQuit()
    {
        SaveMetaData();
    }
}
