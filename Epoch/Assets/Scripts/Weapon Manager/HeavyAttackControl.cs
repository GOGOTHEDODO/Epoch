using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyAttackControl : MonoBehaviour
{
    public SpriteRenderer attackRenderer;
    private Animator animator;
    private bool isAttacking = false;
    private Vector2 attackDirection;
    public double HeavyCooldown;
    public float damage = 15f;
    public float knockbackForce;
    public float stun;

    void Start()
    {
        attackRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        damage = GameManager.instance.playerDamage * 1.5f; // Heavy attack damage should scale off of normal damage
        HeavyCooldown = GameManager.instance.attackCooldown * 1.5; // Heavy attack cooldown should scale off of the other cooldown
        stun = GameManager.instance.currentKnockback * 1.5f;

        // KNOCKBACK FORCE SHOULD NOT BE CHANGED BY UPGRADES, UPGRADE STUN INSTEAD, IT WORKS BETTER I PROMISE, ITS A LITTLE JANK REGARDLESS
        knockbackForce = 3f;
    }

    // Hitbox function
    public Transform boxOrigin; // Position of the hitbox (the center of the rectangle)
    public Vector2 boxSize = new Vector2(2f, 1f); // Width and height of our hitbox

    public void IncreaseDamage(float percentage)
    {
        damage += damage * (percentage / 100);
        Debug.Log("New Sword Damage: " + damage);
    }

    public void DecreaseCoolDown(float percentage)
    {
        HeavyCooldown = HeavyCooldown * (1 - (percentage / 100));
    }

    void Update()
    {
        // On left click, make sure we aren't attacking then start the attack
        if (Input.GetMouseButtonDown(1) && !isAttacking && !CooldownManager.isOtherAttacking)
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
        // Prevents spamming light attack then heavy attack, heavy attacking needs to be intentional
        CooldownManager.isOtherAttacking = true;

        // Lock the facing direction before starting the animation
        isAttacking = true;

        GetComponent<Collider2D>().enabled = true;

        // Trigger the attack animation
        animator.SetTrigger("HeavyAttack");

        // Cooldown for attack
        yield return new WaitForSeconds((float)HeavyCooldown / 2);

        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds((float)HeavyCooldown / 2);

        isAttacking = false;
        CooldownManager.isOtherAttacking = false;
    }

    // Draw functions
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 position = boxOrigin == null ? Vector3.zero : boxOrigin.position;
        Gizmos.DrawWireCube(position, boxSize); // Use WireCube for a rectangle hitbox
    }

    public void DetectHeavyColliders()
    {
        // Get the angle in degrees from attackDirection
        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        // Rotate the hitbox like a pendulum instead of like the circle used to be rotated
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(boxOrigin.position, boxSize, attackAngle))
        {
            if (collider.CompareTag("Enemy"))
            {
                Debug.Log($"Dealing {damage} to {collider.gameObject.name}");
                EnemyRecieveDamage enemy = collider.GetComponent<EnemyRecieveDamage>();
                if (enemy != null)
                {
                    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    enemy.DealDamage(damage, knockbackDirection, knockbackForce, stun);
                }
                else
                {
                    Debug.LogError("EnemyRecieveDamage script is missing on the enemy");
                }
            }
        }


    }

}
