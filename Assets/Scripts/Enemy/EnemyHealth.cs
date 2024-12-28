using System;
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
            Destroy(gameObject);
            GameObject xp = Instantiate(xpOrb, xpPosition, xpRotation);
            xp.GetComponent<xpOrb>().Init(experienceValue);
            
        }
    }

}
