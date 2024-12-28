using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //assign these in prefab
    public float maxHealth;
    public float currentHealth;
    public float invincibilityTime;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        invincibilityTime -= Time.deltaTime;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D collision) {

        if (collision.gameObject.tag == "Enemy" && invincibilityTime <= 0)
        {
            currentHealth -= 1;
            invincibilityTime = 0.5f;
        }
    }
}
