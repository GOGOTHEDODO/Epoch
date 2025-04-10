using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUIManager : MonoBehaviour
{
    private static StatsUIManager instance;

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
