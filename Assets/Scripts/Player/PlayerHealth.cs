using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //assign these in prefab
    public float currentHealth;
    public float invincibilityTime;
    public float invincibilityDuration;


    void Start()
    {
        currentHealth = PlayerStats.Singleton.maxHealth;
        invincibilityDuration = PlayerStats.Singleton.invincibilityDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityTime >= 0) invincibilityTime -= Time.deltaTime;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (currentHealth > PlayerStats.Singleton.maxHealth) currentHealth = PlayerStats.Singleton.maxHealth;
        currentHealth += PlayerStats.Singleton.regeneration * Time.deltaTime;
    }

    public void OnCollisionStay2D(Collision2D collision) {

        if (collision.gameObject.tag == "Enemy" && invincibilityTime <= 0)
        {
            float damage = collision.gameObject.GetComponent<EnemyHealth>().damage;
            currentHealth -= damage;
            invincibilityTime = invincibilityDuration;
            StartCoroutine(FlashRed());
        }
    }

    public IEnumerator FlashRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
