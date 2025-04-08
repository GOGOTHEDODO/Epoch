using UnityEngine;
using UnityEngine.SceneManagement;

public class CooldownUIManager : MonoBehaviour
{
    private static CooldownUIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}