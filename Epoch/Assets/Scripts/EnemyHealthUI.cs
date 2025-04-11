using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyHealthUI : MonoBehaviour
{
    public Image healthBarFill;  // Image to control the health bar fill
    public Image healthBarBackground;  // Image to control the health bar background
    public Vector3 offset = new Vector3(0, 50f, 0); // Offset in screen space
    public GameObject enemy;
    public Camera mainCamera;
    public EnemyRecieveDamage enemyHealth;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(enemy.transform.position);
        healthBarFill.transform.position = screenPos + offset;
        healthBarBackground.transform.position = screenPos + offset;
        //UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        enemyHealth = enemy.GetComponent<EnemyRecieveDamage>();
        if (enemyHealth != null && healthBarFill != null)
        {
            // Calculate the health percentage
            float healthPercentage = enemyHealth.health / enemyHealth.maxHealth;

            Debug.Log("Health Percentage: " + healthPercentage * 100);

            // Ensure the health percentage is clamped between 0 and 1
            healthPercentage = Mathf.Clamp01(healthPercentage);

            // Update the fill amount based on health
            healthBarFill.fillAmount = healthPercentage;

            // Debugging to see if the health value is correct
            // Debug.Log("Health Percentage: " + healthPercentage);
        }
    }
}
