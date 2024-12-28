using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Singleton {get; set;}
    public List<PlayerUpgrade> collectedUpgrades = new List<PlayerUpgrade>();
    [Header("Player Movement Related Stats")]
    public float speed;
    public float dashTime;

    [Header("Player Health Related Stats")]
    public float maxHealth;
    public float invincibilityDuration;

    public float maxXp;
    [Header("Player Weapon Related Stats")]
    public bool usingBoomberang;
    public bool usingTriangle;
    public bool usingGrenade;
    public float boomerangSpiky;
    public float damage;
    public int pierce;
    public float cooldownDuration;
    float cooldown;
    public int maxAmmo = 6;
    public int ammo = 6;
    public float returnSpeed = 10f;

    public float tempModifier = 5f;
    private void Awake()
    {
        if (LoadingParameters.weaponAbility == 0) {
            usingBoomberang = true;
        }
        else if (LoadingParameters.weaponAbility == 1) {
            usingTriangle = true;
        }
        else if (LoadingParameters.weaponAbility == 2) {
            usingGrenade = true;
        }
        
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Singleton != this)
            Destroy(gameObject);
    }
}
