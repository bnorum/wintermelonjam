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

    void Start()
    {
        gameLogic = FindFirstObjectByType<GameLogic>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (currentHealth <= 0)
        {
            gameLogic.activeEnemies.Remove(gameObject);
            Vector3 xpPosition = gameObject.transform.position;
            Quaternion xpRotation = gameObject.transform.rotation;
            GameObject xp = Instantiate(xpOrb, xpPosition, xpRotation);
            xp.GetComponent<xpOrb>().Init(experienceValue);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
