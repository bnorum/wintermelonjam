using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

public class WeaponController : MonoBehaviour
{
    [Header("Base Weapon Class")]
    public GameObject prefab;
    public float damage;
    public int pierce;
    public float speed;
    public float cooldownDuration;
    float cooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        cooldown = cooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0f)
        {
            Shoot();
            Debug.Log("Shooting");
        }
    }

    protected virtual void Shoot()
    {
        cooldown = cooldownDuration;
    }
}
