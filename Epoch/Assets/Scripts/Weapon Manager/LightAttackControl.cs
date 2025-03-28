using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LightAttackControl : MonoBehaviour
{
    public SpriteRenderer attackRenderer;
    private Animator animator;
    private bool isAttacking = false;
    private Vector2 attackDirection;
    public double LightCooldown = 0.5;
    public float damage = 10f;

    void Start()
    {
        attackRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        damage = GameManager.instance.playerDamage;
        LightCooldown = GameManager.instance.attackCooldown;
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
        yield return new WaitForSeconds((float)LightCooldown / 2);

        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds((float)LightCooldown / 2);

        isAttacking = false;
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
        Collider2D closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Get the angle in degrees from attackDirection
        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        // Rotate the hitbox using OverlapBoxAll with rotation
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(boxOrigin.position, boxSize, attackAngle))
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToPlayer = Vector2.Distance(transform.position, collider.transform.position);

                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestEnemy = collider;
                }
            }
        }

        if (closestEnemy != null)
        {
            Debug.Log($"Dealing {damage} to {closestEnemy.gameObject.name}");
            EnemyRecieveDamage enemy = closestEnemy.GetComponent<EnemyRecieveDamage>();
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
