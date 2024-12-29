using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI xpText;
    public RectTransform healthBar;
    public RectTransform xpBar;
    private float maxHealthWidth;
    private float maxXpWidth;

    private PlayerExperience playerExperience;

    public TextMeshProUGUI currencyText;

    private BoomerangGunController boomerangGunController;
    public TextMeshProUGUI boomerangAmmoText;

    public TextMeshProUGUI timerText;

    public UnityEngine.UI.Image dashIndicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerExperience = FindFirstObjectByType<PlayerExperience>();
        boomerangGunController = FindFirstObjectByType<BoomerangGunController>();
        maxHealthWidth = healthBar.sizeDelta.x;
        maxXpWidth = xpBar.sizeDelta.x;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }


    void UpdateUI() {
        float healthPercent = Mathf.Clamp01(playerHealth.currentHealth / PlayerStats.Singleton.maxHealth);
        float xpPercent = Mathf.Clamp01(playerExperience.currentExperience / PlayerStats.Singleton.maxXp);
        healthBar.sizeDelta = new Vector2(maxHealthWidth * healthPercent, healthBar.sizeDelta.y);
        xpBar.sizeDelta = new Vector2(maxXpWidth * xpPercent, xpBar.sizeDelta.y);
        healthText.text = Mathf.FloorToInt(playerHealth.currentHealth) + "/" + Mathf.FloorToInt(PlayerStats.Singleton.maxHealth);
        xpText.text = playerExperience.currentExperience + "/" + Mathf.FloorToInt(PlayerStats.Singleton.maxXp);
        timerText.text = "Time: " + GetTime(FindFirstObjectByType<GameLogic>().timer);
        //currencyText.text = "Currency: " + playerHealth.currency;
        if (boomerangGunController != null) boomerangAmmoText.text = "Boomerang Ammo: " + boomerangGunController.ammo;
        if (FindFirstObjectByType<PlayerMovement>() != null) {
            dashIndicator.fillAmount =  (PlayerStats.Singleton.cooldownDuration - FindFirstObjectByType<PlayerMovement>().dashCooldown) / PlayerStats.Singleton.cooldownDuration;
        }
        
    }

    string GetTime(float time) {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
