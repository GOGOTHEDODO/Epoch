using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LightAttackControl : MonoBehaviour
{
    public SpriteRenderer attackRenderer;
    private Animator animator;
    private bool isAttacking = false;
    private Vector2 attackDirection;
    public double LightCooldown;
    public float damage = 10f;
    public float knockbackForce;
    public float stun;
    private Color originalBladeColor;
    private bool hasHitEnemyThisAttack = false;
    public GameObject lightningEffectPrefab;
    

    void Start()
    {
        StartCoroutine(DelayedInit());
    }

    IEnumerator DelayedInit()
    {
        // Wait until GameManager.instance is assigned (max 0.5 sec)
        float timeout = 0.5f;
        while (GameManager.instance == null && timeout > 0)
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager.instance still null after delay!");
            yield break;
        }

        attackRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        damage = GameManager.instance.playerDamage;
        LightCooldown = GameManager.instance.attackCooldown;
        stun = GameManager.instance.currentKnockback;
        CooldownManager.isOtherAttacking = false;

        originalBladeColor = attackRenderer.color;

        if (GameManager.instance.inWorldTwo)
        {
            attackRenderer.color = Color.blue;
            originalBladeColor = attackRenderer.color;
        }
        // KNOCKBACK FORCE SHOULD NOT BE CHANGED BY UPGRADES, UPGRADE STUN INSTEAD, IT WORKS BETTER I PROMISE,
        knockbackForce = 2f;
    }

    // Hitbox function
    public Transform boxOrigin;
    public Vector2 boxSize = new Vector2(2f, 1f);

    public void IncreaseDamage(float percentage)
    {
        damage += damage * (percentage / 100);
        Debug.Log("New Sword Damage: " + damage);
    }

    public void DecreaseCoolDown(float percentage)
    {
        LightCooldown = LightCooldown * (1 - (percentage / 100));
    }

    void Update()
    {
        // On left click, make sure we aren't attacking then start the attack
        if (Input.GetMouseButtonDown(0) && !isAttacking && !CooldownManager.isOtherAttacking)
        {
            // Get mouse position when the player clicks the mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Find the direction
            attackDirection = (mousePosition - transform.position).normalized;

            // Set the direction for the attack
            transform.right = attackDirection;

            // If this attack would occur on the left side of the player then flip it's Y, looks better this way
            attackRenderer.flipY = attackDirection.x < 0;

            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        CooldownManager.isOtherAttacking = true;
        // Lock the facing direction before starting the animation
        isAttacking = true;
        damage = GameManager.instance.playerDamage;

        hasHitEnemyThisAttack = false;
        GetComponent<Collider2D>().enabled = true;

        // Trigger the attack animation
        animator.SetTrigger("Attack");
        DetectColliders();
        CooldownUI.instance.StartCooldown(LightCooldown);

        // Cooldown for attack
        yield return new WaitForSeconds((float)LightCooldown / 2);

        GetComponent<Collider2D>().enabled = false;

        if (GameManager.instance.hasDoubleAttack)
        {
            yield return new WaitForSeconds(0.05f);

            damage = GameManager.instance.playerDamage *0.25f;
            hasHitEnemyThisAttack = false;
            Debug.Log($"DAMAGE IS {damage}");
            GetComponent<Collider2D>().enabled = true;
            animator.SetTrigger("Attack");
            DetectColliders();

            yield return new WaitForSeconds((float)LightCooldown / 4);
            GetComponent<Collider2D>().enabled = false;
        }

        yield return new WaitForSeconds((float)LightCooldown / 2);

        isAttacking = false;
        CooldownManager.isOtherAttacking = false;
    }

    // draw functions
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 position = boxOrigin == null ? Vector3.zero : boxOrigin.position;
        Gizmos.DrawWireCube(position, boxSize);
    }



    public void DetectColliders()
    {
        // Prevents hitting two enemies at once when you kill the first enemy, weird bug
        if (hasHitEnemyThisAttack) return;

        Collider2D closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Get the angle in degrees from attackDirection
        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        // Rotate the hitbox like a pendulum instead of like the circle from before
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(boxOrigin.position, boxSize, attackAngle))
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToPlayer = Vector2.Distance(transform.position, collider.transform.position);

                // Check for the closest distance to player and only deal damage to that enemy
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestEnemy = collider;
                }
            }
        }

        // Deal damage to closest enemy
        if (closestEnemy != null)
        {
            hasHitEnemyThisAttack = true;


            bool isCritHit = Random.value < GameManager.instance.currentCritRate;
            float finalDamage = damage * GameManager.instance.currentCritDamage;
            if (isCritHit)
            {
                StartCoroutine(FlashBladeRed());
            }
            else
            {
                finalDamage = damage;
            }


            bool isSecondAttack = damage < GameManager.instance.playerDamage;

            Debug.Log(isSecondAttack
                ? $"🔥 Second attack hit for {damage} (25% of base)"
                : $"🗡️ Primary attack hit for {damage}");
            
            Debug.Log($"Dealing {damage} to {closestEnemy.gameObject.name}");
            
            EnemyRecieveDamage enemy = closestEnemy.GetComponent<EnemyRecieveDamage>();
            if (enemy != null)
            {
                if(GameManager.instance.hasFireSword)
                {
                 enemy.ApplyBurn(2f, 3f);
                }
                Vector2 knockbackDirection = (closestEnemy.transform.position - transform.position).normalized;
                enemy.DealDamage(finalDamage, knockbackDirection, knockbackForce, stun);
                if(GameManager.instance.hasElectricSword && lightningEffectPrefab != null)
                {
                    GameObject lightning = Instantiate(lightningEffectPrefab, closestEnemy.transform.position, Quaternion.identity);
                    
                }
                if (damage < GameManager.instance.playerDamage)
                {
                    Debug.Log($"Second attack hit for {damage} (25% of base)");
                }
                else
                {
                    Debug.Log($"Primary attack hit for {damage}");
                }
                
            }
            else
            {
                Debug.LogError("EnemyRecieveDamage script is missing on the enemy");
            }
        }
    }

    private IEnumerator FlashBladeRed()
    {
        attackRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        attackRenderer.color = originalBladeColor;
    }
}
