using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

public class WeaponController : MonoBehaviour
{
    [Header("Base Weapon Class")]
    public GameObject prefab;
    private float cooldown;
    protected PlayerMovement pm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement>();
        cooldown = PlayerStats.Singleton.cooldownDuration;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0f && Input.GetMouseButtonDown(0))
        {
            Shoot();
            Debug.Log("Shooting");
        }
    }

    protected virtual void Shoot()
    {
        cooldown = PlayerStats.Singleton.cooldownDuration;
    }
}
