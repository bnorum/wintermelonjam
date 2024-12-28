using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

public class WeaponController : MonoBehaviour
{
    [Header("Base Weapon Class")]
    public GameObject prefab;
    private float cooldown;
    protected PlayerMovement pm;

    public bool automatic = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement>();
        cooldown = PlayerStats.Singleton.cooldownDuration;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(automatic)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0f)
            {
                Shoot();
                Debug.Log("Shooting");
            }
        }
    }

    protected virtual void Shoot()
    {
        cooldown = PlayerStats.Singleton.cooldownDuration;
    }
}
