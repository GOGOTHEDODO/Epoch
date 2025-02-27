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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            //EnemyHealth enemyHealth = other.GetComponent<EnemyHeath>();
            // if(enemyHealth != null)
            // {
            //     enemyHealth.TakeDamage(damage);
            // }
        }
    }

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

        // Trigger the attack animation
        animator.SetTrigger("Attack");

        // Cooldown for attack, this can be changed with a buff if we want, so im including it as a variable, its also probably slow right now but it doesn't matter yet
        yield return new WaitForSeconds((float)LightCooldown);

        isAttacking = false;
    }
}
