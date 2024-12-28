using UnityEngine;
using UnityEngine.Rendering;

public class GravityGunController
 : WeaponController
{
    
    public float LaunchSpeed = 10f;
    public float gravity = -9.8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
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
        grenadeRb.gravityScale = Mathf.Abs(gravity / Physics2D.gravity.y);

        Collider2D grenadeCollider = spawnedGrenade.GetComponent<Collider2D>();
        if(grenadeCollider != null)
        {
            grenadeCollider.enabled = false;
        }
        spawnedGrenade.GetComponent<GravityGunBehaviour>()?.Init(pm.mouseposition);

    }
}
