using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttacking : MonoBehaviour
{
    public float attackDamage = 10f;
    public float attackCooldown;
    public float attackRange = 1f; // Distance from player
    private float lastAttackTime = 0f;

    public Transform attackHolder;
    public SpriteRenderer attackRenderer;
    private Animator attackAnimator; // We'll grab this from attackHolder


    private Transform player;
    private PlayerHealth playerHealth;
    private Animator animator;



void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();

        if (attackHolder != null)
        {
            attackAnimator = attackHolder.GetComponent<Animator>();
        }
}

    void Update()
    {
        if (player == null) return;

        // Check distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    private void AttackPlayer()
    {

        if (player == null || attackHolder == null) return;

        // Get direction to player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Rotate the AttackHolder to face the player
        attackHolder.right = directionToPlayer;

        // Optional: Flip sprite if needed (e.g., if weapon or VFX looks better when flipped)
        SpriteRenderer attackRenderer = attackHolder.GetComponent<SpriteRenderer>();
        if (attackRenderer != null)
        {
            attackRenderer.flipY = directionToPlayer.x < 0;
        }

        // We are basically faking a real attack as we don't have much time left (I, Luke Supan, will be coming back to this project after the class is over)
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage, gameObject);
            Debug.Log("Enemy attacked the player!");
        }

        lastAttackTime = Time.time; // Cooldown starts here
    }
}
