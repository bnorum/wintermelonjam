using UnityEngine;

public class BoomerangGunController : WeaponController
{

    [Header("Boomerang Gun Settings")]
    private int maxAmmo;
    public int ammo;
    private float returnSpeed;
    public float spikyDamage = 0;

    public AudioClip shootSound;
    public AudioClip returnSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        
        spikyDamage = PlayerStats.Singleton.boomerangSpiky;
        maxAmmo = PlayerStats.Singleton.maxAmmo;
        returnSpeed = PlayerStats.Singleton.returnSpeed;
        ammo = maxAmmo;
    }

    protected override void Update()
    {
        
        base.Update();
        if (Input.GetMouseButtonDown(1)) {
            GameObject.Find("Boomerang Return Audio").GetComponent<AudioSource>().pitch = 1;
            GameObject.Find("Boomerang Return Audio").GetComponent<AudioSource>().PlayOneShot(returnSound);
            ReturnBoomerangs();
        }

        if (ammo > maxAmmo) {
            ammo = maxAmmo;
        }

        
        spikyDamage = PlayerStats.Singleton.boomerangSpiky;
    }
    

    protected override void Shoot()
    {
        if (ammo > 0) {
            GameObject.Find("Boomerang Throw Audio").GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            GameObject.Find("Boomerang Throw Audio").GetComponent<AudioSource>().PlayOneShot(shootSound);
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
