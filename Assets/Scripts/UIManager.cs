using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI currencyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIText();
    }


    void UpdateUIText() {
        healthText.text = "Health: " + playerHealth.currentHealth;

    }
}
