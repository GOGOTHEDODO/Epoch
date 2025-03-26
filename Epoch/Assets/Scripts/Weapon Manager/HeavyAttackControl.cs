using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyAttackControl : MonoBehaviour
{
    public SpriteRenderer attackRenderer;
    private Animator animator;
    private bool isAttacking = false;
    private Vector2 attackDirection;
    public double HeavyCooldown = 1;
    public float damage = 15f;

    void Start()
    {
        attackRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        damage = GameManager.instance.playerDamage;
        HeavyCooldown = GameManager.instance.attackCooldown;
    }

    // Hitbox function
    public Transform boxOrigin; // Position of the hitbox (the center of the rectangle)
    public Vector2 boxSize = new Vector2(2f, 1f); // Width and Height of the rectangle hitbox

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
        if (Input.GetMouseButtonDown(0) && !isAttacking)
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
        // Lock the facing direction before starting the animation
        isAttacking = true;

        GetComponent<Collider2D>().enabled = true;

        // Trigger the attack animation
        animator.SetTrigger("Attack");

        // Cooldown for attack
        yield return new WaitForSeconds((float)HeavyCooldown / 2);

        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds((float)HeavyCooldown / 2);

        isAttacking = false;
    }

    // draw functions
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 position = boxOrigin == null ? Vector3.zero : boxOrigin.position;
        Gizmos.DrawWireCube(position, boxSize); // Use WireCube for a rectangle hitbox
    }

    public void DetectHeavyColliders()
    {
        // Detect colliders within the rectangular hitbox (Box Collider area)
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(boxOrigin.position, boxSize, 0f)) // 0 rotation
        {
            Debug.Log(collider.name);

            if (collider.CompareTag("Enemy"))
            {
                Debug.Log($"Dealing {damage} to {collider.gameObject.name}");
                EnemyRecieveDamage enemy = collider.GetComponent<EnemyRecieveDamage>();
                if (enemy != null)
                {
                    enemy.DealDamage(damage);
                }
                else
                {
                    Debug.LogError("EnemyRecieveDamage script is missing on the enemy");
                }
            }
        }
    }

}
