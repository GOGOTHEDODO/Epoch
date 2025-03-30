using UnityEngine;

public class UpgradeOrb : MonoBehaviour
{
    private LevelManager levelManager;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;  // Stop any movement
            rb.bodyType = RigidbodyType2D.Kinematic; // Ensure it doesn't move
        }
    }

    public void SetLevelManager(LevelManager manager)
    {
        levelManager = manager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Upgrade Orb activated!");
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if(playerMovement != null)
            {
                playerMovement.enableMovement(false);
            }
            ShowUpgradeMenu();
        }
    }

    void ShowUpgradeMenu()
    {
        UpgradeUI upgradeUI = FindObjectOfType<UpgradeUI>();
        if (upgradeUI != null)
        {
            upgradeUI.ShowUpgrade();
            upgradeUI.SetUpgradeOrb(this);
        }
    }

    public void UpgradeChosen()
    {
        Debug.Log("Upgrade selected! Informing LevelManager...");
        if (levelManager != null)
        {
            levelManager.OnUpgradeSelected();
        }
        Destroy(gameObject);
    }
}
