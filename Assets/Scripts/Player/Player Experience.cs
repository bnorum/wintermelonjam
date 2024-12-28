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
    public float maxExperience;
    public float currentExperience;

    public List<ScriptableObject> upgrades = new List<ScriptableObject>();
    private List<ScriptableObject> localUpgrades = new List<ScriptableObject>();
    private List<ScriptableObject> savedUpgrades = new List<ScriptableObject>();

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
        if(currentExperience >= maxExperience && canUpdate)
        {
            GiveUpgrades();
        }
    }

    private void GiveUpgrades()
    {
        canUpdate = false;
        localUpgrades = new List<ScriptableObject>(upgrades);
        for (int i = 0; i < 3; i++)
        {
            int choice = UnityEngine.Random.Range(0, localUpgrades.Count);
            ScriptableObject selectedUpgrade = localUpgrades[choice];

            if (selectedUpgrade is PlayerUpgrade playerUpgrade)
            {
                upgradeMenu.SetUpgrade(i, playerUpgrade.upgradeName, playerUpgrade.upgradeImage);
            }
            else if (selectedUpgrade is WeaponUpgrade weaponUpgrade)
            {
                upgradeMenu.SetUpgrade(i, weaponUpgrade.upgradeName, weaponUpgrade.upgradeImage);
            }
            savedUpgrades.Add(selectedUpgrade);
            localUpgrades.RemoveAt(choice);
        }

        Time.timeScale = 0;
        upgradeMenu.gameObject.SetActive(true);
    }

    public void ReceiveUpgrade(int choice)
    {
        ScriptableObject selectedUpgrade = savedUpgrades[choice];
        if (selectedUpgrade is PlayerUpgrade upgrade)
        {
            string affectedScript = upgrade.affectedScript;
            string affectedVariable = upgrade.affectedVariable;  
            Component targetComponent = GetComponent(affectedScript);
            if (targetComponent != null)
            {
                var targetField = targetComponent.GetType().GetField(affectedVariable);
                var targetProperty = targetComponent.GetType().GetProperty(affectedVariable);

                if (targetField != null)
                {
                    object currentValue = targetField.GetValue(targetComponent);
                    if (upgrade.additive)
                    {
                        if (currentValue is int currentInt)
                        {
                            targetField.SetValue(targetComponent, currentInt + upgrade.additiveAmount);
                        }
                        else if (currentValue is float currentFloat)
                        {
                            targetField.SetValue(targetComponent, currentFloat + upgrade.additiveAmount);
                        }
                    }
                    else if (upgrade.multiplicative)
                    {
                        if (currentValue is int currentInt)
                        {
                            targetField.SetValue(targetComponent, (int)(currentInt * upgrade.multiplicativeAmount));
                        }
                        else if (currentValue is float currentFloat)
                        {
                            targetField.SetValue(targetComponent, currentFloat * upgrade.multiplicativeAmount);
                        }
                    }
                }
                else if (targetProperty != null && targetProperty.CanWrite)
                {
                    object currentValue = targetProperty.GetValue(targetComponent);
                    if (upgrade.additive)
                    {
                        if (currentValue is int currentInt)
                        {
                            targetProperty.SetValue(targetComponent, currentInt + upgrade.additiveAmount);
                        }
                        else if (currentValue is float currentFloat)
                        {
                            targetProperty.SetValue(targetComponent, currentFloat + upgrade.additiveAmount);
                        }
                    }
                    else if (upgrade.multiplicative)
                    {
                        if (currentValue is int currentInt)
                        {
                            targetProperty.SetValue(targetComponent, (int)(currentInt * upgrade.multiplicativeAmount));
                        }
                        else if (currentValue is float currentFloat)
                        {
                            targetProperty.SetValue(targetComponent, currentFloat * upgrade.multiplicativeAmount);
                        }
                    }
                }
            }
        }
        Time.timeScale = 1;
        upgradeMenu.gameObject.SetActive(false);
        currentExperience = 0f;
        canUpdate = true;
    }
    

}
