using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        if(SceneManager.GetActiveScene().buildIndex == GameManager.instance.bossSceneIndex)
        {
            int BraziersLit = MazeWinScript.BraziersLit;
            enemyCountText.text = "Braziers Deactivated: " + BraziersLit + "/4";
        }
        else 
        {
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            enemyCountText.text = "Enemies Left: " + enemyCount;

        }
    }
}
