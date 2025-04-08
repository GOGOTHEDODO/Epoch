using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public static HealthUI instance;  // Singleton to persist across scenes
    public Image healthBarFill;  // Image to control the health bar fill
    public Image healthBarBackground;  // Image to control the health bar background
    public TextMeshProUGUI healthText;  // Text element for health percentage
    public PlayerHealth playerHealth;  // Reference to the PlayerHealth script
    public float healthBarXPos;
    public float healthBarYPos;
    public float textXPos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this UI across scenes
        }
        else
        {
            Destroy(gameObject);  // Prevent duplicate instances
        }
    }

    void Start()
    {
        // Find PlayerHealth and sync health
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not found in the scene!");
        }
        else
        {
            UpdateHealthBar();  // Initialize health bar
        }
    }

    void Update()
    {
        RectTransform healthBarFillRect = healthBarFill.GetComponent<RectTransform>();
        RectTransform healthBarBackgroundRect = healthBarBackground.GetComponent<RectTransform>();
        RectTransform healthTextRect = healthText.GetComponent<RectTransform>();
        textXPos = healthBarXPos + 115;
        // Continuously update the health bar whenever health changes
        if (playerHealth != null && healthBarFill != null)
        {
            UpdateHealthBar();
        }
        if (SceneManager.GetActiveScene().name == "Main Menu") 
        {
            healthBarFillRect.anchoredPosition = new Vector2(1000, 0);
            healthBarBackgroundRect.anchoredPosition = new Vector2(1000, 0);
            healthTextRect.anchoredPosition = new Vector2(1000, 0);
        }
        else
        {
            healthBarFillRect.anchoredPosition = new Vector2(healthBarXPos, healthBarYPos);
            healthBarBackgroundRect.anchoredPosition = new Vector2(healthBarXPos, healthBarYPos);
            healthTextRect.anchoredPosition = new Vector2(textXPos, healthBarYPos);
        }
    }

    // Method to update the health bar fill based on current health
    public void UpdateHealthBar()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        if (playerHealth != null && healthBarFill != null)
        {
            // Calculate the health percentage
            float healthPercentage = playerHealth.health / playerHealth.maxHealth;

            // Ensure the health percentage is clamped between 0 and 1
            healthPercentage = Mathf.Clamp01(healthPercentage);

            // Update the fill amount based on health
            healthBarFill.fillAmount = healthPercentage;

            healthText.text = "(" + Mathf.RoundToInt(healthPercentage * 100) + "%)";

            // Debugging to see if the health value is correct
           // Debug.Log("Health Percentage: " + healthPercentage);
        }
    }
}