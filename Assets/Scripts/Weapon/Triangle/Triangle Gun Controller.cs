using UnityEngine;

public class TriangleGunController : WeaponController
{
   
     protected override void Start()
    {
        base.Start();
    }
    protected override void Shoot()
    {
        // Debug.Log("Shooting from Triangle");
        // base.Shoot();
        // Vector3 playerPosition = transform.position;
        // // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // // Vector3 direction = (mousePosition - playerPosition).normalized;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // GameObject spawnedKnife = Instantiate(prefab, playerPosition, Quaternion.Euler(0, 0, angle));

        // spawnedKnife.GetComponent<TriangleGunBehaviour>().SetDirection(direction);

    }
}
