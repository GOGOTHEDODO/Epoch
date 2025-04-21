using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySpawner : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject rogueEnemyPrefab;
    public int numberOfMeleeEnemies = 1;
    public int numberOfRangedEnemies = 1;
    public int numberOfRogueEnemies = 1;
    public GameObject player;

    public PolygonCollider2D spawnArea;

    void Start()
    {
        SpawnMeleeEnemies();
        SpawnRangedEnemies();
        SpawnRogueEnemies();
    }

    public void SpawnMeleeEnemies()
    {
        Bounds bounds = spawnArea.bounds;

        int spawned = 0;
        int maxAttempts = numberOfMeleeEnemies * 100; // prevent infinite loop
        int attempts = 0;

        while (spawned < numberOfMeleeEnemies && attempts < maxAttempts)
        {
            attempts++;
            Vector2 randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            if (spawnArea.OverlapPoint(randomPoint))
            {
                GameObject enemyInstance = Instantiate(meleeEnemyPrefab, randomPoint, Quaternion.identity);
                AIDestinationSetter destSetter = enemyInstance.GetComponent<AIDestinationSetter>();
                if (destSetter != null)
                {
                    destSetter.target = player.transform;
                }
                spawned++;
            }
        }
    }
    public void SpawnRangedEnemies()
    {
        Bounds bounds = spawnArea.bounds;

        int spawned = 0;
        int maxAttempts = numberOfRangedEnemies * 100; // prevent infinite loop
        int attempts = 0;

        while (spawned < numberOfRangedEnemies && attempts < maxAttempts)
        {
            attempts++;
            Vector2 randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            if (spawnArea.OverlapPoint(randomPoint))
            {
                GameObject enemyInstance = Instantiate(rangedEnemyPrefab, randomPoint, Quaternion.identity);
                AIDestinationSetter destSetter = enemyInstance.GetComponent<AIDestinationSetter>();
                if (destSetter != null)
                {
                    destSetter.target = player.transform;
                }
                spawned++;
            }
        }
    }

    public void SpawnRogueEnemies()
    {
        Bounds bounds = spawnArea.bounds;

        int spawned = 0;
        int maxAttempts = numberOfRogueEnemies * 100; // prevent infinite loop
        int attempts = 0;

        while (spawned < numberOfRogueEnemies && attempts < maxAttempts)
        {
            attempts++;
            Vector2 randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            if (spawnArea.OverlapPoint(randomPoint))
            {
                GameObject enemyInstance = Instantiate(rogueEnemyPrefab, randomPoint, Quaternion.identity);
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
