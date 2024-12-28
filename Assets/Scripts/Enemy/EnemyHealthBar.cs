using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public EnemyHealth em;
    public RectTransform healthBar;
     float maxHealthWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        maxHealthWidth = healthBar.sizeDelta.x;
        em = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = Mathf.Clamp01(em.currentHealth / em.maxHealth);
        healthBar.sizeDelta = new Vector2(maxHealthWidth * healthPercent, healthBar.sizeDelta.y);
    }
}
