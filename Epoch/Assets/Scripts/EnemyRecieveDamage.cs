using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;

    private bool recentlyHit = false;
    public float hitLockoutDuration = 0.05f;

    // This will be changed based on attack

    private Renderer rend;
    private Color originalColor;
    public float flashDuration = 0.1f;
    private Rigidbody2D rb;
    public EnemyHealthUI enemyHealthUI;

    // We are gonna turn the ai for a bit...
    private AIPath aiPath;

    private Coroutine burnCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (GameManager.instance != null && GameManager.instance.inWorldTwo)
        {
            float healthMultiplier = 1.5f;
            maxHealth *= healthMultiplier;
            health = maxHealth;
        }
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
    }

    public void DealDamage(float damage, Vector2 knockbackDirection, float knockbackForce, float hitstun)
    {
        if (recentlyHit) return;
        recentlyHit = true;
        Invoke(nameof(ResetHitLockout), hitLockoutDuration);

        health -= damage;
        ApplyKnockback(knockbackDirection, knockbackForce, hitstun);
        enemyHealthUI.GetComponent<EnemyHealthUI>().UpdateHealthBar();

        TintRed();
        checkDeath();
    }

    private void ResetHitLockout()
    {
        recentlyHit = false;
    }

    void ApplyKnockback(Vector2 direction, float force, float hitstun)
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

            StartCoroutine(ReenableAIPath(hitstun));
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

    public void ApplyBurn(float damagerPerTick, float duration)
    {
        if(burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }
        burnCoroutine = StartCoroutine(BurnOverTime(damagerPerTick, duration));
    }

    private IEnumerator BurnOverTime(float damagerPerTick, float duration)
    {
        float elapsedTime = 0f;
        float tickRate = 1f;

        yield return new WaitForSeconds(tickRate);

        while(elapsedTime < duration)
        {
            ApplyBurnTick(damagerPerTick);
            yield return new WaitForSeconds(tickRate);
            elapsedTime += tickRate;
        }
    }

    public void ApplyBurnTick(float damage)
    {
        health -= damage;
        enemyHealthUI.GetComponent<EnemyHealthUI>().UpdateHealthBar();
        TintRed();
        checkDeath();
    }

}
