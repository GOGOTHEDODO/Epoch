using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public GameObject gameOverUIPrefab;
    private static GameObject gameOverUIInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes

            SpawnGameOverUI();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void SpawnGameOverUI()
    {
        if (gameOverUIPrefab != null && gameOverUIInstance == null)
        {
            gameOverUIInstance = Instantiate(gameOverUIPrefab);
            DontDestroyOnLoad(gameOverUIInstance);
        }
    }
}