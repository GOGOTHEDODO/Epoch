using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
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
        SceneManager.LoadScene(0);
    }
}
