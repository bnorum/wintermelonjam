using UnityEngine;

public class BoomerangGunController : WeaponController
{

    [Header("Boomerang Gun Settings")]
    public int maxAmmo = 6;
    public int ammo = 6;
    public float returnSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(1)) {
            ReturnBoomerangs();
        }

        if (ammo > maxAmmo) {
            ammo = maxAmmo;
        }
    }
    

    protected override void Shoot()
    {
        if (ammo > 0) {
            ammo--;
            Debug.Log("Shooting from Boomerang");
            base.Shoot();
            Vector3 playerPosition = transform.position;
            Vector3 direction = (pm.mouseposition - playerPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Sets angle of projectile spawning aiming towards player's current mouse position.
            GameObject proj = Instantiate(prefab, playerPosition, Quaternion.Euler(0, 0, angle));
            proj.GetComponent<BoomerangGunBehaviour>().SetDirection(direction);

            


        }
        
    }

    protected void ReturnBoomerangs() {
        foreach (GameObject proj in GameObject.FindGameObjectsWithTag("BoomerangBullet")) {
            proj.GetComponent<BoomerangGunBehaviour>().ReturnToPlayer();
        }
    }
}
