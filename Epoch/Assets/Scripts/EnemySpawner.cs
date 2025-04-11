using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 1;
    public GameObject player;

    public PolygonCollider2D spawnArea;

    void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        Bounds bounds = spawnArea.bounds;

        Debug.Log("SpawnEnemies()");

        int spawned = 0;
        int maxAttempts = numberOfEnemies * 100; // prevent infinite loop
        int attempts = 0;

        while (spawned < numberOfEnemies && attempts < maxAttempts)
        {
            attempts++;
            Vector2 randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            if (spawnArea.OverlapPoint(randomPoint))
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, randomPoint, Quaternion.identity);
                AIDestinationSetter destSetter = enemyInstance.GetComponent<AIDestinationSetter>();
                if (destSetter != null)
                {
                    destSetter.target = player.transform;
                }
                spawned++;
            }
        }
    }
}
