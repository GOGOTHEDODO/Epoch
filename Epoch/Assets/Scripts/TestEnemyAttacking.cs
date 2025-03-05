using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAttacking : MonoBehaviour
{
    public float attackDamage = 10f;
    public float attackCooldown;
    public float attackRange = 1f; // Distance from player
    private float lastAttackTime = 0f;

    private Transform player;
    private PlayerHealth playerHealth;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
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
        if(animator != null)
        {
            animator.SetTrigger("Attack");
        }
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Enemy attacked the player!");
        }
    }
}
