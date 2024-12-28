using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //assign these in prefab
    public float maxHealth;
    public float currentHealth;


    public GameLogic gameLogic;

    void Start()
    {
        gameLogic = FindFirstObjectByType<GameLogic>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            gameLogic.activeEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    

    
}
