using UnityEngine;

public class TriangleGunController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     protected override void Start()
    {
        base.Start();
    }
    protected override void Shoot()
    {
        Debug.Log("Shooting from Triangle");
        base.Shoot();
        Vector3 playerPosition = transform.position;
        Vector3 direction = (pm.mouseposition - playerPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Sets angle of projectile spawning aiming towards player's current mouse position.
        GameObject spawnedKnife = Instantiate(prefab, playerPosition, Quaternion.Euler(0, 0, angle));

        spawnedKnife.GetComponent<TriangleGunBehaviour>().SetDirection(direction);

    }
}
