using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWinScript : MonoBehaviour
{
    private bool isCaptured = false;
    private static int number = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(isCaptured);
        if (other.CompareTag("Player") && !isCaptured)
        {
            number++;
            Debug.Log("Braziers collected: " + number);
            isCaptured = true;

            if (number == 4)
            {
                LevelManager levelManager = FindObjectOfType<LevelManager>();

                if (levelManager != null)
                {
                   levelManager.SpawnUpgradeOrb();
                }
            }
        }  
    }
}
