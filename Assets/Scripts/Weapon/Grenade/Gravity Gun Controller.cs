using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GravityGunController : MonoBehaviour
{
    private PlayerMovement pm;
    public GameObject inProjectile;
    public float inwardMaxAmmo = 1;
    private float inwardAmmo;
    // public float outwardMaxAmmo = 1;
    // private float outwardAmmo;

    private float cooldownIn;
    private float cooldownDurationIn;
    private float cooldownTriangle;
    private float cooldownDurationTriangle;

    public GameObject trianglePrefab;

    public List<GameObject> liveInGrenades = new List<GameObject>();
    // public List<GameObject> liveOutGrenades = new List<GameObject>();

    public AudioClip GrenadeFireAudio;
    public AudioClip GrenadeExplode;
    public AudioClip TriangleFire;

    public Sprite gravityGunBlueShot;
    public Sprite gravityGunRedShot;
    public Sprite gravityGunRegular;
    public float shotDuration =.25f;
    private float redShotTimer = 0;
    private float blueShotTimer = 0;
    private bool blueShooting = false;
    private bool redShooting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.LogWarning("Gravity Gun On");
        pm = FindFirstObjectByType<PlayerMovement>();

        inwardAmmo = inwardMaxAmmo;

        cooldownDurationIn = PlayerStats.Singleton.cooldownDuration;
        cooldownIn = PlayerStats.Singleton.grenadeCooldownDuration;
        // cooldownDurationOut = PlayerStats.Singleton.cooldownDuration;
        cooldownTriangle = cooldownDurationTriangle;
        
    }
    void Update()
    {
        if (blueShooting)
        {
            blueShotTimer -= Time.deltaTime;
            if (blueShotTimer <= 0)
            {
                blueShooting = false;
                blueShotTimer = shotDuration;
                FindFirstObjectByType<GunSpriteHolder>().GetComponent<SpriteRenderer>().sprite = gravityGunRegular;
            }
        }
        if (redShooting)
        {
            redShotTimer -= Time.deltaTime;
            if (redShotTimer <= 0)
            {
                redShooting = false;
                redShotTimer = shotDuration;
                FindFirstObjectByType<GunSpriteHolder>().GetComponent<SpriteRenderer>().sprite = gravityGunRegular;
            }
        }
        if(Input.GetMouseButtonDown(1) && liveInGrenades.Count > 0) {
            Debug.LogWarning("Triggering");
            Trigger(1);
        }
        else if(Input.GetMouseButtonDown(1) && inwardAmmo > 0 && cooldownIn <= 0)
        {
            Debug.LogWarning("Shooting");

            Shoot(1);
        }

        if(cooldownTriangle <= 0)
        {
            ShootTriangle();
            cooldownDurationTriangle = PlayerStats.Singleton.cooldownDuration;
        }
        else
            cooldownTriangle -= Time.deltaTime;
            cooldownIn -=Time.deltaTime;
    }  
    void Shoot(int type)
    {
        
        GameObject.Find("Grenade Fire Audio").GetComponent<AudioSource>().PlayOneShot(GrenadeFireAudio);
        if(type == 1 && inwardAmmo == 1)
        {

            Vector3 playerPosition = transform.position;
            Vector3 direction = (pm.mouseposition - playerPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Sets angle of projectile spawning aiming towards player's current mouse position.
            GameObject spawnedGrenade = Instantiate(inProjectile, playerPosition, Quaternion.Euler(0, 0, angle));

            spawnedGrenade.GetComponent<GravityGunBehaviour>().SetDirection(direction);
            var grenadeRb = spawnedGrenade.GetComponent<Rigidbody2D>();
            Vector2 velocity = direction * PlayerStats.Singleton.grenadeLaunchSpeed;
            grenadeRb.linearVelocity = velocity;
            spawnedGrenade.GetComponent<GravityGunBehaviour>()?.Init(pm.mouseposition, type);
                inwardAmmo = 0;
                cooldownIn = cooldownDurationIn;
            FindFirstObjectByType<GunSpriteHolder>().GetComponent<SpriteRenderer>().sprite = gravityGunRedShot;
            redShotTimer = shotDuration;
            redShooting = true;
            }
        }
    protected void Trigger(int type)
    {
        
        GameObject.Find("Grenade Explode Audio").GetComponent<AudioSource>().PlayOneShot(GrenadeExplode);
        if(type == 1 && liveInGrenades.Count <= 0) return;
        {
            liveInGrenades[0].GetComponent<GravityGunBehaviour>().Implode();
            inwardAmmo = 1;
        }
    }

    void ShootTriangle() 
    {
        cooldownTriangle = cooldownDurationTriangle;
        pm = FindFirstObjectByType<PlayerMovement>();
        Vector3 playerPosition = transform.position;
        Vector3 direction = (pm.mouseposition - playerPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Sets angle of projectile spawning aiming towards player's current mouse position.
        GameObject spawnedKnife = Instantiate(trianglePrefab, playerPosition, Quaternion.Euler(0, 0, angle));

        spawnedKnife.GetComponent<TriangleGunBehaviour>().SetDirection(direction);
        FindFirstObjectByType<GunSpriteHolder>().GetComponent<SpriteRenderer>().sprite = gravityGunBlueShot;
            blueShotTimer = shotDuration;
            blueShooting = true;
    }


}

