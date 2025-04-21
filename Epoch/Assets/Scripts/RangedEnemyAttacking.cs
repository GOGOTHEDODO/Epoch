using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttacking : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackCooldown = 2f;
    public float attackRange = 5f;
    private float lastAttackTime = 0f;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {

        if (player == null || projectilePrefab == null)
        {
            Debug.Log("Okay something is missing");
        }



        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Attempting attack!");

            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    private void AttackPlayer()
    {
        Vector2 shootDirection = (player.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = shootDirection * projectileSpeed;
        }

        Debug.Log("Ranged enemy fired a projectile!");
    }
}
