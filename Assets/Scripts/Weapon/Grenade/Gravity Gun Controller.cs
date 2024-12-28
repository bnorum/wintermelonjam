using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GravityGunController : WeaponController
{
    public float ammo = 3;
    public float LaunchSpeed = 10f;
    private List<GameObject> liveGrenades = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0)) {
            Trigger(0);
        }
        else if(Input.GetMouseButtonDown(1)) {
            Trigger(1);
        }

    }
    protected override void Shoot()
    {
        base.Shoot();
        if(liveGrenades.Count < ammo)
        {
            Vector3 playerPosition = transform.position;
            Vector3 direction = (pm.mouseposition - playerPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Sets angle of projectile spawning aiming towards player's current mouse position.
            GameObject spawnedGrenade = Instantiate(prefab, playerPosition, Quaternion.Euler(0, 0, angle));

            spawnedGrenade.GetComponent<GravityGunBehaviour>().SetDirection(direction);
            var grenadeRb = spawnedGrenade.GetComponent<Rigidbody2D>();
            Vector2 velocity = direction * LaunchSpeed;
            grenadeRb.linearVelocity = velocity;
            spawnedGrenade.GetComponent<GravityGunBehaviour>()?.Init(pm.mouseposition);
            liveGrenades.Add(spawnedGrenade);
        }
    }
    protected void Trigger(int setting)
    {
        GameObject grenade = liveGrenades[0];
        if(setting ==0)
            grenade.GetComponent<GravityGunBehaviour>().Explode();
        if(setting ==1)
            grenade.GetComponent<GravityGunBehaviour>().Implode();
        
        liveGrenades.RemoveAt(0);
    }
}

