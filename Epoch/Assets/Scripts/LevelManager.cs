using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int[] sceneIDs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemies();
    }
    void CheckForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0)
        {
            Debug.Log("All enemies have been defeated! Moving to next level...");
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2f);

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
