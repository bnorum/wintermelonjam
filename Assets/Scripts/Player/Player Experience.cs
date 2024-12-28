using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    //assign these in prefab
    public float maxExperience;
    public float currentExperience;

    public List<ScriptableObject> upgrades = new List<ScriptableObject>();
    private List<ScriptableObject> localUpgrades = new List<ScriptableObject>();

    void Start()
    {
        currentExperience = 0;
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "XP")
        {
            Debug.Log("proper collision");
            currentExperience += collision.gameObject.GetComponent<xpOrb>().value;
            Destroy(collision.gameObject);
        }
    }
    void Update()
    {
        if(currentExperience >= maxExperience)
        {
            GiveUpgrades();
        }
    }

    private void GiveUpgrades()
    {
        localUpgrades = upgrades;
        int choice1 = UnityEngine.Random.Range(0, localUpgrades.Count-1);
        localUpgrades.Remove(localUpgrades[choice1]);
        int choice2 = UnityEngine.Random.Range(0, localUpgrades.Count-1);
        localUpgrades.Remove(localUpgrades[choice2]);
        int choice3 = UnityEngine.Random.Range(0, localUpgrades.Count-1);
        localUpgrades.Remove(localUpgrades[choice3]);
        
    }
}
