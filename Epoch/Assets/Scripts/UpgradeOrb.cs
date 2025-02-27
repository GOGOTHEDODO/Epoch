using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOrb : MonoBehaviour
{

    public UpgradeUI upgradeUI;
    // Start is called before the first frame update
    void Start()
    {
        
        upgradeUI = FindObjectOfType<UpgradeUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeUI.ShowUpgrade();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
