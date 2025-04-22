using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float damage = 12f;
    public float lifetime = 5f;
    public GameObject shooter; // Optional: assign who shot it (can be used for damage credit)

    void Start()
    {
        if(GameManager.instance.inWorldTwo)
        {
            damage *= 1.5f;
        }
        // Auto-destroy after a while to prevent projectiles lingering forever
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Only damage the player
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, shooter != null ? shooter : gameObject);
            }

            Destroy(gameObject);
        }

    }
}
