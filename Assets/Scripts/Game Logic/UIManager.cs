using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;

    public PlayerExperience playerExperience;
    public TextMeshProUGUI expText;

    public TextMeshProUGUI currencyText;

    public BoomerangGunController boomerangGunController;
    public TextMeshProUGUI boomerangAmmoText;

    public TextMeshProUGUI timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerExperience = FindFirstObjectByType<PlayerExperience>();
        boomerangGunController = FindFirstObjectByType<BoomerangGunController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIText();
    }


    void UpdateUIText() {
        healthText.text = "Health: " + playerHealth.currentHealth;
        expText.text = "Exp: " + playerExperience.currentExperience;
        //timerText.text = "Time: " + FindFirstObjectByType<GameLogic>().timer;
        //currencyText.text = "Currency: " + playerHealth.currency;
        if (boomerangGunController != null) boomerangAmmoText.text = "Boomerang Ammo: " + boomerangGunController.ammo;

    }
}
