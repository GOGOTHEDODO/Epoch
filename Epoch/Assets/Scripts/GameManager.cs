using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
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
<<<<<<< HEAD
    public float knockback = 0.03f;
=======
    public float currentKnockback = 0.03f;
>>>>>>> RyansBranch

    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public int currentDashQuantity = 1;
    public bool hasFireSword = false;

    //base stats when player is killed
<<<<<<< HEAD
    private float basePlayerSpeed = 5f;
    private float basePlayerDamage = 10f;
    private float baseAttackCoolDown = 0.5f;
    private float baseMaxHealth = 100f;
    private float baseCurrentHealth = 100f;
    private float baseLuck = 0f;
    private float baseKnockback = 0.3f;
=======

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

>>>>>>> RyansBranch

    private List<UpgradeData> allUpgrades = new List<UpgradeData>();

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
<<<<<<< HEAD
        playerDamage = basePlayerDamage;
        playerSpeed =  basePlayerSpeed;
        attackCooldown = baseAttackCoolDown;
        maxHealth = baseMaxHealth;
        currentHealth = baseCurrentHealth;
        currentLuck = baseLuck;
        knockback = baseKnockback;
        CooldownManager.isOtherAttacking = false;
=======
        ApplyMetaUpgrades();
        CooldownManager.isOtherAttacking = false;
        hasFireSword = false;
>>>>>>> RyansBranch
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
<<<<<<< HEAD
                knockback += upgrade.currentValue;
=======
                currentKnockback += upgrade.currentValue;
                break;
            case UpgradeData.UpgradeType.DashQuantity:
                currentDashQuantity += (int)upgrade.currentValue;
                break;
            case UpgradeData.UpgradeType.FireSword:
                hasFireSword = true;
>>>>>>> RyansBranch
                break;
        }

        Debug.Log($"Applied {upgrade.upgradeName} - New Stats -> Speed: {playerSpeed}, Damage: {playerDamage}, Cooldown: {attackCooldown}, CurrentHealth: {currentHealth}, CurrentLuck: {currentLuck}");
        upgrade.ResetToBaseValue();
    }

    public void ResetUpgrades()
    {
        UpgradeData[] upgrades = Resources.LoadAll<UpgradeData>("");

        foreach (UpgradeData upgrade in upgrades)
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
