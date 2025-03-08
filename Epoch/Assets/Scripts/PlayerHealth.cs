using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public float maxHealth;
    public float health;

    void Start()
    {
        if (GameManager.instance!= null)
        {
            health = GameManager.instance.currentHealth;
        }
        
    }

    public void TakeDamage(float damage, GameObject sender)
    {
        if (sender.layer == gameObject.layer) return;
  
        health -= damage;
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

    private void CheckDeath()
    {
        Destroy(gameObject);
        Debug.Log("Player has died!");
        GameManager.instance.RestartGame();
        // Add logic for player death (e.g., restart level, show death screen)
    }
}

