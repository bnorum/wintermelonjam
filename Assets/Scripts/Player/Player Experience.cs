using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    //assign these in prefab
    public float currentExperience;

    public List<PlayerUpgrade> upgrades = new List<PlayerUpgrade>();
    private List<PlayerUpgrade> localUpgrades = new List<PlayerUpgrade>();
    private List<PlayerUpgrade> savedUpgrades = new List<PlayerUpgrade>();

    public UpgradeMenu upgradeMenu;
    bool canUpdate = true;

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
        if(currentExperience >= PlayerStats.Singleton.maxXp && canUpdate)
        {
            GiveUpgrades();
        }
    }

private void GiveUpgrades()
{
    canUpdate = false;
    localUpgrades = new List<PlayerUpgrade>(upgrades);
    for (int i = 1; i <= 3; i++)
    {
        int choice = UnityEngine.Random.Range(0, localUpgrades.Count);
        PlayerUpgrade selectedUpgrade = localUpgrades[choice];
        upgradeMenu.SetUpgrade(i, selectedUpgrade.upgradeName, selectedUpgrade.upgradeSprite);
        Debug.Log($"Upgrade {i}: {selectedUpgrade.upgradeName}");

        savedUpgrades.Add(selectedUpgrade);
        localUpgrades.RemoveAt(choice);
    }
    Time.timeScale = 0;
    upgradeMenu.gameObject.SetActive(true);
    localUpgrades.Clear();
}

    public void ReceiveUpgrade(int choice)
    {
        Debug.Log($"Player Choice: {choice}. Associated Upgrade: {savedUpgrades[choice].upgradeName}");
        // Validate the index
        if (choice < 0 || choice >= savedUpgrades.Count)
        {
            Debug.LogError($"Invalid upgrade choice index: {choice}. List count: {savedUpgrades.Count}");
            return;
        }

        // Cast the selected upgrade
        PlayerUpgrade upgradeComponent = savedUpgrades[choice] as PlayerUpgrade;

        if (upgradeComponent == null)
        {
            Debug.LogError("Selected upgrade is not a PlayerUpgrade!");
            return;
        }

        string affectedVar = upgradeComponent.affectedVariable;

        // Get the field from PlayerStats
        var fieldInfo = typeof(PlayerStats).GetField(affectedVar, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

        if (fieldInfo == null)
        {
            Debug.LogError($"{affectedVar} doesn't exist in PlayerStats.");
            return;
        }

        // Get the current value of the field
        object currentValue = fieldInfo.GetValue(PlayerStats.Singleton);

        // Ensure the value is numeric
        if (currentValue is float currentFloat)
        {
            float newValue;

            if (upgradeComponent.upgradeType == 1) // Additive upgrade
            {
                newValue = currentFloat + upgradeComponent.amount;
            }
            else if (upgradeComponent.upgradeType == 2) // Multiplicative upgrade
            {
                newValue = currentFloat * upgradeComponent.amount;
            }
            else //will add bool support
            {
                return; 
            }

            fieldInfo.SetValue(PlayerStats.Singleton, newValue);

            Debug.LogWarning($"Upgraded {affectedVar}: {currentFloat} -> {newValue}");
        }
        savedUpgrades.Clear();
        Time.timeScale = 1;
        upgradeMenu.gameObject.SetActive(false);
        currentExperience = 0f;
        canUpdate = true;
        PlayerStats.Singleton.maxXp *= 1.2f;
        
    }


    

}
