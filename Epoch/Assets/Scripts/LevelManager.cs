using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int[] sceneIDs;
    public GameObject upgradeOrbPrefab; // Assign in Inspector
    private GameObject spawnedOrb;
    private bool upgradeSelected = false;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemies();
    }
    void CheckForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0 && spawnedOrb == null)
        {
            Debug.Log("All enemies have been defeated! Moving to next level...");
            SpawnUpgradeOrb();
        }
    }

    void SpawnUpgradeOrb()
    {
        if (upgradeOrbPrefab != null && player != null)
        {
            Vector3 spawnPosition = player.position + new Vector3(1f, 0, 0); // Spawns near player
            spawnedOrb = Instantiate(upgradeOrbPrefab, spawnPosition, Quaternion.identity);

            // Link the Upgrade Orb to this Level Manager
            UpgradeOrb upgradeScript = spawnedOrb.GetComponent<UpgradeOrb>();
            if (upgradeScript != null)
            {
                upgradeScript.SetLevelManager(this);
            }
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
        StartCoroutine(LoadNextLevel()); // Now we can proceed
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0.01f);

        if(sceneIDs.Length > 0)
        {
            int randomIndex = Random.Range(0, sceneIDs.Length);
            int nextSceneID = sceneIDs[randomIndex];

            Debug.Log($"Loading Scene ID: {nextSceneID}");
            SceneManager.LoadScene(nextSceneID);
        }
        else 
        {
            Debug.LogError("No scenes available for randomization");
        }
    }
}
