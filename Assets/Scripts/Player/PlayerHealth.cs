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

    public bool isDead = false;

    void Start()
    {
        currentHealth = PlayerStats.Singleton.maxHealth;
        invincibilityDuration = PlayerStats.Singleton.invincibilityDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityTime >= 0) invincibilityTime -= Time.deltaTime;

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(FindFirstObjectByType<UIManager>().GameOver());
            DisablePlayer();
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

    public void DisablePlayer()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("Boomerang Gun Controller")?.SetActive(false);
        GameObject.Find("Grenade Gun Controller")?.SetActive(false);
        GameObject.Find("GunSpriteHolder")?.SetActive(false);
    }

}
