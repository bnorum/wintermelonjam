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
    [Header("PLayer Weapon Related Stats")]
    public bool usingBoomberang;
    public bool usingTriangle;
    public float damage;
    public int pierce;
    public float cooldownDuration;
    float cooldown;
    public int maxAmmo = 6;
    public int ammo = 6;
    public float returnSpeed = 10f;
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Singleton != this)
            Destroy(gameObject);
    }
}
