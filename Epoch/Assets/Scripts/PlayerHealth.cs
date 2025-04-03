using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth;
    public float health;

    private Renderer rend;
    private Color originalColor;
    public float flashDuration = 0.1f;

    private Animator animator;

    void Start()
    {
        if (GameManager.instance != null)
        {
            health = GameManager.instance.currentHealth;
            rend = GetComponent<Renderer>();
            originalColor = rend.material.color;
            animator = GetComponent<Animator>();
        }
        if (HealthUI.instance != null)
        {
            HealthUI.instance.UpdateHealthBar();
        }

    }

    public void TakeDamage(float damage, GameObject sender)
    {
        if (sender.layer == gameObject.layer) return;

        health -= damage;
        TintRed();

        if (HealthUI.instance != null)
        {
            HealthUI.instance.UpdateHealthBar();
        }

        Debug.Log($"Player took {damage} damage! Current HP: {health}");

        if (health <= 0)
        {
            CheckDeath();
        }
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    private void TintRed()
    {
        rend.material.color = Color.red;

        StartCoroutine(ResetColor());
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(flashDuration);

        rend.material.color = originalColor;
    }

    private void CheckDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("Dies");
        }

        StartCoroutine(DeathSequence());



        // Add logic for player death (e.g., restart level, show death screen)
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
        Debug.Log("Player has died!");
        GameManager.instance.RestartGame();
    }

}

