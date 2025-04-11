using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPopup : MonoBehaviour
{
    public static StatsPopup instance;
    public GameObject popupPanel;
    public TextMeshProUGUI statsText;
    public GameManager gameManager;
    public Button button;

    public Sprite normalSprite;
    public Sprite selectedSprite;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this UI across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Destroy any duplicate instance
        }
    }

    void Start()
    {
        popupPanel.SetActive(false);
    }

    void Update()
    {
        // Replace KeyCode.T with whatever key you want
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleStats();
        }
    }

    public void ToggleStats()
    {
        bool isActive = popupPanel.activeSelf;

        if (!isActive)
        {
            string stats = $"Speed: {gameManager.playerSpeed}\nDamage: {gameManager.playerDamage}\nCooldown: {gameManager.attackCooldown}\nStarting Health: {gameManager.currentHealth}\nLuck: {gameManager.currentLuck}";
            statsText.text = stats;
            button.GetComponent<Image>().sprite = selectedSprite;
        }
        else
        {
            button.GetComponent<Image>().sprite = normalSprite;
        }
        popupPanel.SetActive(!isActive);
    }

    public void CloseStats()
    {
        popupPanel.SetActive(false);
        button.GetComponent<Image>().sprite = normalSprite;
    }
}
