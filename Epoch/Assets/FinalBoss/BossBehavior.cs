using System.Collections;
using UnityEngine;

public class FinalBossDamageHandler : MonoBehaviour
{
    public float maxHealth = 1000f;
    private float currentHealth;

    private Renderer rend;
    private Color originalColor;
    public float flashDuration = 0.1f;

    public EnemyHealthUI enemyHealthUI;

    private Coroutine burnCoroutine;
    private bool recentlyHit = false;
    public float hitLockoutDuration = 0.05f;

    // Boss phases or damage-triggered events
    [System.Serializable]
    public class BossPhase
    {
        public float healthThreshold;
        public Vector2 moveToPosition;
        public bool triggered;
    }

    public BossPhase[] bossPhases;

    void Start()
    {
        currentHealth = maxHealth;

        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        if (enemyHealthUI != null)
            enemyHealthUI.UpdateHealthBar();
    }

    void Update()
    {
        CheckPhases();
    }

    public void DealDamage(float damage)
    {
        if (recentlyHit) return;

        recentlyHit = true;
        Invoke(nameof(ResetHitLockout), hitLockoutDuration);

        currentHealth -= damage;

        TintRed();
        UpdateHealthUI();
        CheckPhases();
        CheckDeath();
    }

    private void ResetHitLockout()
    {
        recentlyHit = false;
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

    private void UpdateHealthUI()
    {
        if (enemyHealthUI != null)
            enemyHealthUI.UpdateHealthBar();
    }

    private void CheckPhases()
    {
        foreach (var phase in bossPhases)
        {
            if (!phase.triggered && currentHealth <= phase.healthThreshold)
            {
                phase.triggered = true;
                StartCoroutine(HandlePhase(phase));
            }
        }
    }

    private IEnumerator HandlePhase(BossPhase phase)
    {
        // You can add animation or effects here before moving
        yield return new WaitForSeconds(0.5f);

        // Manual movement
        transform.position = phase.moveToPosition;

        // Add any behavior changes for this phase here (attacks, etc.)
        Debug.Log($"Boss moved to new phase at {phase.healthThreshold} HP!");
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Boss defeated!");
            Destroy(gameObject);
        }
    }

    public void ApplyBurn(float damagePerTick, float duration)
    {
        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        burnCoroutine = StartCoroutine(BurnOverTime(damagePerTick, duration));
    }

    private IEnumerator BurnOverTime(float damagePerTick, float duration)
    {
        float elapsedTime = 0f;
        float tickRate = 1f;

        while (elapsedTime < duration)
        {
            ApplyBurnTick(damagePerTick);
            yield return new WaitForSeconds(tickRate);
            elapsedTime += tickRate;
        }
    }

    private void ApplyBurnTick(float damage)
    {
        currentHealth -= damage;
        TintRed();
        UpdateHealthUI();
        CheckPhases();
        CheckDeath();
    }
}
