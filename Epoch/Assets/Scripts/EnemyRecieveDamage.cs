using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;

    // This will be changed based on attack

    private Renderer rend;
    private Color originalColor;
    public float flashDuration = 0.1f;
    private Rigidbody2D rb;

    // We are gonna turn the ai for a bit...
    private AIPath aiPath;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
    }

    public void DealDamage(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        health -= damage;
        ApplyKnockback(knockbackDirection, knockbackForce);

        TintRed();
        checkDeath();
    }

    void ApplyKnockback(Vector2 direction, float force)
    {
        if(rb!= null)
        {
            if(aiPath != null)
            {
                 aiPath.enabled = false;
            }

            // this gets rid of velocity, might turn this off
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * force, ForceMode2D.Impulse);

            StartCoroutine(ReenableAIPath(0.3f));
        }
    }

    IEnumerator ReenableAIPath(float duration)
    {
        yield return new WaitForSeconds(duration);

       if (aiPath != null)
        {
            aiPath.enabled = true;
        }
    }

    private void CheckOverHeal()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void checkDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
