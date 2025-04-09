using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttacking : MonoBehaviour
{
    public float attackDamage = 10f;
    public float attackCooldown;
    public float attackRange = 1f;
    private float lastAttackTime = 0f;

    private Transform player;
    private PlayerHealth playerHealth;
    private Animator attackAnimator;
    private Transform attackManagerTransform;
    private SpriteRenderer attackRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();

        attackManagerTransform = transform.Find("AttackManager");

        if (attackManagerTransform != null)
        {
            Transform attackAnimationTransform = attackManagerTransform.Find("AttackAnimation");
            attackAnimator = attackAnimationTransform?.GetComponent<Animator>();
            attackRenderer = attackAnimationTransform?.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogWarning("AttackManager object not found in hierarchy.");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            RotateTowardPlayer();
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    private void RotateTowardPlayer()
    {
        if (attackManagerTransform == null || player == null) return;

        Vector3 direction = (player.position - attackManagerTransform.position).normalized;

        // Face the direction
        attackManagerTransform.right = direction;

        // Flip sprite if facing left
        if (attackRenderer != null)
        {
            attackRenderer.flipY = direction.x < 0;
        }
    }

    private void AttackPlayer()
    {
        if (attackAnimator != null)
        {
            attackAnimator.SetTrigger("Attack");
        }

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage, gameObject);
            Debug.Log("Enemy attacked the player!");
        }
    }
}
