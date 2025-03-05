using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public float maxHealth;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage} damage! Current HP: {health}");

        if (health <= 0)
        {
            CheckDeath();
        }
    }

    private void CheckDeath()
    {
        Destroy(gameObject);
        Debug.Log("Player has died!");
        GameManager.instance.RestartGame();
        // Add logic for player death (e.g., restart level, show death screen)
    }
}

