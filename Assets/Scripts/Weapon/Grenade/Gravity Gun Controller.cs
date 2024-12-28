using UnityEngine;
using UnityEngine.Rendering;

public class GravityGunController : WeaponController
{
    
    public float LaunchSpeed = 10f;
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
        Vector3 playerPosition = transform.position;
        Vector3 direction = (pm.mouseposition - playerPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Sets angle of projectile spawning aiming towards player's current mouse position.
        GameObject spawnedGrenade = Instantiate(prefab, playerPosition, Quaternion.Euler(0, 0, angle));

        spawnedGrenade.GetComponent<GravityGunBehaviour>().SetDirection(direction);
        var grenadeRb = spawnedGrenade.GetComponent<Rigidbody2D>();
        Vector2 velocity = direction * LaunchSpeed;
        grenadeRb.linearVelocity = velocity;
        spawnedGrenade.GetComponent<GravityGunBehaviour>()?.Init(pm.mouseposition);

    }
    protected void Trigger(int setting)
    {
        foreach (GameObject proj in GameObject.FindGameObjectsWithTag("GravityBullet")) {
            if(setting ==0)
                proj.GetComponent<GravityGunBehaviour>().Explode();
            if(setting ==1)
                proj.GetComponent<GravityGunBehaviour>().Implode();

        }
    }
}
