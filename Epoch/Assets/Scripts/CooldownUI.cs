using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CooldownUI : MonoBehaviour
{
    public static CooldownUI instance;
    public Image cooldownFill; 
    public Image cooldownBackground;
    public float cooldownXPos;
    public float cooldownYPos;
    public GameManager gameManager;
    private float initialCooldown;
    private float currentCooldown;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this UI across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Destroy any duplicate instance
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialCooldown = gameManager.GetComponent<GameManager>().attackCooldown;
        currentCooldown = initialCooldown;
        UpdateCooldownBar();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform cooldownFillRect = cooldownFill.GetComponent<RectTransform>();
        RectTransform cooldownBackgroundRect = cooldownBackground.GetComponent<RectTransform>();
        // Continuously update the health bar whenever health changes
        if (cooldownFill != null)
        {
            UpdateCooldownBar();
        }
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            cooldownFillRect.anchoredPosition = new Vector2(1000, 0);
            cooldownBackgroundRect.anchoredPosition = new Vector2(1000, 0);
        }
        else
        {
            cooldownFillRect.anchoredPosition = new Vector2(cooldownXPos, cooldownYPos);
            cooldownBackgroundRect.anchoredPosition = new Vector2(cooldownXPos, cooldownYPos);
        }

    }

    public void UpdateInitialCooldown()
    {
        initialCooldown = gameManager.GetComponent<GameManager>().attackCooldown;
    }

    // Method to update the health bar fill based on current health
    public void UpdateCooldownBar()
    {
        float cooldownPercentage = currentCooldown / initialCooldown;
        cooldownPercentage = Mathf.Clamp01(cooldownPercentage); // Ensure it's between 0 and 1

        // Update the fill amount based on the cooldown
        cooldownFill.fillAmount = cooldownPercentage;
    }

    // Call this method when the cooldown starts
    public void StartCooldown(double cooldownTime)
    {
        currentCooldown = (float)cooldownTime;
        StartCoroutine(CooldownTimer());
    }

    // Coroutine to track the cooldown over time
    private IEnumerator CooldownTimer()
    {
        while (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;  // Reduce cooldown time every frame
            UpdateCooldownBar();  // Update UI based on current cooldown
            yield return null;
        }

        // When cooldown is finished, reset the cooldown bar
        currentCooldown = 0;
        UpdateCooldownBar();
    }
}

