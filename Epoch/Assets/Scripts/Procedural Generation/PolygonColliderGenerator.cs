using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonColliderGenerator : MonoBehaviour
{
    // Array of coordinates for the polygon
    public Vector2[] points;
    public float trim  = 1f;
    PolygonCollider2D polygonCollider;

    void Start()
    {
        // Add a PolygonCollider2D component to the GameObject

        polygonCollider.enabled = false;
    }

    public void SetPoints(float top_y, float bottom_y, float left_x, float right_x)
    {
        if (polygonCollider == null)
        {
            polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        }

        points = new Vector2[4];
        points[0] = new Vector2(left_x - trim, bottom_y + trim);   // Bottom-left
        points[1] = new Vector2(right_x + trim, bottom_y + trim);   // Bottom-right
        points[2] = new Vector2(right_x + trim, top_y - trim);      // Top-right
        points[3] = new Vector2(left_x - trim, top_y - trim);       // Top-left

        polygonCollider.SetPath(0, points);

        polygonCollider.enabled = true;

        GameObject spawner = new GameObject("EnemySpawner");
        EnemySpawner spawnerScript = spawner.AddComponent<EnemySpawner>();

        spawnerScript.spawnArea = polygonCollider;

        spawnerScript.player = GameObject.Find("Player");

        spawnerScript.numberOfMeleeEnemies = (int) Mathf.Floor((float) Random.Range(0, 1000) / 950); //Adjust spawn rates in maze
        spawnerScript.meleeEnemyPrefab = GameObject.Find("Melee Enemy");
        spawnerScript.SpawnMeleeEnemies();

        spawnerScript.numberOfRangedEnemies = (int)Mathf.Floor((float)Random.Range(0, 1000) / 950); //Adjust spawn rates in maze
        spawnerScript.rangedEnemyPrefab = GameObject.Find("Ranged Enemy");
        spawnerScript.SpawnRangedEnemies();

        spawnerScript.numberOfRogueEnemies = (int)Mathf.Floor((float)Random.Range(0, 1000) / 950); //Adjust spawn rates in maze
        spawnerScript.rogueEnemyPrefab = GameObject.Find("Rogue Enemy");
        spawnerScript.SpawnRogueEnemies();

        polygonCollider.enabled = false;
    }
}
