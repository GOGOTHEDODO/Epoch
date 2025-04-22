using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class LevelManager : MonoBehaviour
{
    public int[] sceneIDs;
    public GameObject upgradeOrbPrefab; // Assign in Inspector
    private GameObject spawnedOrb;
    private bool upgradeSelected = false;
    private Transform player;
    public static LevelManager instance;
    public bool defeatAllEnemies = true;

    public int[] worldTwoSceneIDs;
    private bool inWorldTwo = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (SceneManager.GetActiveScene().buildIndex == GameManager.instance.bossSceneIndex)
        {
            StartCoroutine(ScanAfterMazeIsBuilt());
        }
    }

    IEnumerator ScanAfterMazeIsBuilt()
    {
        yield return new WaitForSeconds(0.1f); // Or wait for maze generator to complete
        AstarPath.active.Scan();
        Debug.Log("Maze scanned for boss room!");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemies();
    }
    void CheckForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0 && spawnedOrb == null && defeatAllEnemies)
        {
            if( SceneManager.GetActiveScene().buildIndex == 11)
            {
                Debug.Log("YOU WIN");
                if (WinScreenUI.instance != null)
                    WinScreenUI.instance.ShowWinScreen();
                
                return;
            }
            Debug.Log("All enemies have been defeated! Moving to next level...");
            SpawnUpgradeOrb();
        }
    }

    public void SpawnUpgradeOrb()
{
    if (upgradeOrbPrefab != null && player != null)
    {
        Vector3[] offsets = new Vector3[]
        {
            new Vector3(3f, 0, 0),   // right
            new Vector3(-3f, 0, 0),  // left
            new Vector3(0, 3f, 0),   // up
            new Vector3(0, -3f, 0),  // down
            new Vector3(2f, 2f, 0),  // top-right
            new Vector3(-2f, 2f, 0), // top-left
            new Vector3(2f, -2f, 0), // bottom-right
            new Vector3(-2f, -2f, 0) // bottom-left
        };

        foreach (var offset in offsets)
        {
            Vector3 spawnPosition = player.position + offset;

            // Check if the position is free (no walls or colliders)
            Collider2D hit = Physics2D.OverlapCircle(spawnPosition, 0.5f, LayerMask.GetMask("Obstacle", "Water"));

            if (hit == null)
            {
                // Found a safe position!
                spawnedOrb = Instantiate(upgradeOrbPrefab, spawnPosition, Quaternion.identity);

                UpgradeOrb upgradeScript = spawnedOrb.GetComponent<UpgradeOrb>();
                if (upgradeScript != null)
                {
                    upgradeScript.SetLevelManager(this);
                }

                return;
            }
        }

        Debug.LogWarning("No safe spawn location found for Upgrade Orb!");
    }
}

    public void OnUpgradeSelected()
    {
        if (GameManager.instance != null)
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if(playerHealth !=null)
            {
                GameManager.instance.SavePlayerHealth(playerHealth.GetCurrentHealth());
            }
        }
        upgradeSelected = true;
        if (spawnedOrb != null)
        {
            Destroy(spawnedOrb); // Remove Upgrade Orb after selection
        }

        // This prevents the cooldown being frozen
        CooldownManager.isOtherAttacking = false;
        StartCoroutine(LoadNextLevel()); 
    }

    public void ShowLegendaryUpgradeChoices()
    {
        List<UpgradeData> legendaryOptions = new List<UpgradeData>();

        foreach (UpgradeData upgrade in GameManager.instance.allUpgrades)
        {
            if (upgrade.rarity == Rarity.Legendary && !upgrade.hasBeenChosen)
            {
                legendaryOptions.Add(upgrade);
            }
        }

        if (legendaryOptions.Count != 2)
        {
            Debug.LogError($"Exactly 2 Legendary upgrades expected, but found: {legendaryOptions.Count}");
            return;
        }

        UpgradeData upgrade1 = legendaryOptions[0];
        UpgradeData upgrade2 = legendaryOptions[1];

        Debug.Log($"🟡 Showing Legendary Panel: {upgrade1.upgradeName} & {upgrade2.upgradeName}");
        LegendaryUpgradeUI.instance.ShowLegendaryChoices(upgrade1, upgrade2);
    }


    IEnumerator LoadNextLevel()
    {
        GameManager.instance.currentLevelCount++;
        if((GameManager.instance.currentLevelCount % 3) == 0 && GameManager.instance.currentLevelCount != 0)
        {
            GameManager.instance.metaUpgrades.starParts += 1;
        }
        yield return new WaitForSeconds(0.01f);

        if(sceneIDs.Length > 0)
        {
           if (!GameManager.instance.inWorldTwo && SceneManager.GetActiveScene().buildIndex != GameManager.instance.bossSceneIndex && GameManager.instance.currentLevelCount == GameManager.instance.maxLevelBeforeBoss)
            {
                // Load the boss scene
                GameManager.instance.metaUpgrades.starParts += 2;
                SceneManager.LoadScene(GameManager.instance.bossSceneIndex);
                Debug.Log("LOADING THE BOSS SCENE");
            }
            else if (!GameManager.instance.inWorldTwo && SceneManager.GetActiveScene().buildIndex == GameManager.instance.bossSceneIndex)
            {
                // We're in the boss scene, upgrade selected, now move on to world two
                AstarPath.active.Scan();
                Debug.Log("Entered World 2 — Next boss at level " + GameManager.instance.maxLevelBeforeBoss);


                // Load first world two level
                ShowLegendaryUpgradeChoices();
                yield break;
            }
            else if(GameManager.instance.inWorldTwo && SceneManager.GetActiveScene().buildIndex != 11 && GameManager.instance.currentLevelCount == GameManager.instance.maxLevelBeforeBoss)
            {
                Debug.Log("FINAL BOSS TIME");
                SceneManager.LoadScene(11);
                yield break;
            }
            else
            {
                // Standard level progression
                int[] currentScenePool;
                if(GameManager.instance.inWorldTwo)
                {
                    currentScenePool = worldTwoSceneIDs;
                }
                else 
                {
                    currentScenePool = sceneIDs;
                }
                int randomIndex = Random.Range(0, currentScenePool.Length);
                int nextSceneID = currentScenePool[randomIndex];
                SceneManager.LoadScene(nextSceneID);
            }
            
        }
        else 
        {
            Debug.LogError("No scenes available for randomization");
        }
        CooldownUI.instance.UpdateInitialCooldown();
        StatsPopup.instance.CloseStats();
    }
    public void LoadNextLevelAfterLegendary()
    {
        GameManager.instance.inWorldTwo = true;
        int additionalLevels = Random.Range(1, 2);
        GameManager.instance.maxLevelBeforeBoss = GameManager.instance.currentLevelCount + additionalLevels;
        int randomIndex = Random.Range(0, worldTwoSceneIDs.Length);
        int nextSceneID = worldTwoSceneIDs[randomIndex];
        SceneManager.LoadScene(nextSceneID);
    }
}
