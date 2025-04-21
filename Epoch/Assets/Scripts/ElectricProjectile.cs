using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricProjectile : MonoBehaviour
{
    public float damage = 5f;
    public float arcRange = 3f;
    public float lifeTime = 0.2f;
    public LineRenderer lineRenderer;
    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        ArcToNearbyEnemies();
        Destroy(gameObject, lifeTime);
    }

    void ArcToNearbyEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, arcRange, enemyLayer);
        List<Vector3> arcPoints = new List<Vector3>();

        arcPoints.Add(transform.position); // Start at player

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                arcPoints.Add(hit.transform.position);

                // Deal damage
                EnemyRecieveDamage enemy = hit.GetComponent<EnemyRecieveDamage>();
                if (enemy != null)
                {
                    Vector2 knockbackDirection = (hit.transform.position - transform.position).normalized;
                    enemy.DealDamage(damage, knockbackDirection, 0f, 0f);
                }
            }
        }

        if (arcPoints.Count > 1)
        {
            lineRenderer.positionCount = arcPoints.Count;
            lineRenderer.SetPositions(arcPoints.ToArray());
        }
    }
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, arcRange);
    }
}
