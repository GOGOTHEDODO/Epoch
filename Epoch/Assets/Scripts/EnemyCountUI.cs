using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCountUI : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCountText.text = "Enemies Left: " + enemyCount;
    }
}
