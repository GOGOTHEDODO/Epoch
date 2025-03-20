using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;

    private Renderer rend;
    private Color originalColor;
    public float flashDuration = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        TintRed();
        checkDeath();
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
