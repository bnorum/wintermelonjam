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
        GameObject spawnedKnife = Instantiate(prefab);
        spawnedKnife.transform.position = transform.position;

    }
}
