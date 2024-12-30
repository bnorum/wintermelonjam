using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float experienceValue;
    public float damage;
    public GameObject xpOrb;
    private GameLogic gameLogic;

    public AudioClip damageSound;
    public AudioClip deathSound;

    void Start()
    {
        gameLogic = FindFirstObjectByType<GameLogic>();
        currentHealth = maxHealth;
    }
    void Update()
    {

        if (currentHealth <= 0 && GetComponent<EnemyMovement>().isBoss) {
            gameLogic.bossDefeated = true;

        }
        if (currentHealth <= 0)
        {
            gameLogic.activeEnemies.Remove(gameObject);
            Vector3 xpPosition = gameObject.transform.position;
            Quaternion xpRotation = gameObject.transform.rotation;
            GameObject xp = Instantiate(xpOrb, xpPosition, xpRotation);
            xp.GetComponent<xpOrb>().Init(experienceValue);

            AudioSource audioSource = GameObject.Find("Enemy Death Audio").GetComponent<AudioSource>();
            audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(deathSound);

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashRed());
        AudioSource audioSource = GameObject.Find("Enemy Damage Audio").GetComponent<AudioSource>();
        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(damageSound);
    }

    IEnumerator FlashRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
