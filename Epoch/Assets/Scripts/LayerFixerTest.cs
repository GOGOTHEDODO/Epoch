using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSorting : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;

    void Start()
    {
        // Get the TilemapRenderer component
        tilemapRenderer = GetComponent<TilemapRenderer>();

        if (tilemapRenderer != null)
        {
            // Set sorting order based on Y position
            tilemapRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
        else
        {
            Debug.LogError("TilemapRenderer not found on " + gameObject.name);
        }
    }
}
