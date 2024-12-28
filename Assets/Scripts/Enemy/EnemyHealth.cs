using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //assign these in prefab
    public float maxHealth;
    public float currentHealth;
    public float experienceValue;
    public GameObject xpOrb;
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
            Vector3 xpPosition = gameObject.transform.position;
            Quaternion xpRotation = gameObject.transform.rotation;
            Destroy(gameObject);
            GameObject xp = Instantiate(xpOrb, xpPosition, xpRotation);
            xp.GetComponent<xpOrb>().Init(experienceValue);
            
        }
    }

}
