using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GravityGunController : MonoBehaviour
{
    private PlayerMovement pm;
    public GameObject inProjectile;
    public GameObject outProjectile;
    public float inwardMaxAmmo = 1;
    private float inwardAmmo;
    public float outwardMaxAmmo = 1;
    private float outwardAmmo;

    private float cooldownIn;
    private float cooldownDurationIn;
    private float cooldownOut;
    private float cooldownDurationOut;

    public GameObject trianglePrefab;
    public bool TriangleExperiment = true;

    public float LaunchSpeed = 10f;
    public List<GameObject> liveInGrenades = new List<GameObject>();
    public List<GameObject> liveOutGrenades = new List<GameObject>();

    public AudioClip GrenadeFireAudio;
    public AudioClip GrenadeExplode;
    public AudioClip TriangleFire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement>();

        inwardAmmo = inwardMaxAmmo;
        outwardAmmo = outwardMaxAmmo;

        cooldownDurationIn = PlayerStats.Singleton.cooldownDuration;
        cooldownIn = cooldownDurationIn;
        cooldownDurationOut = PlayerStats.Singleton.cooldownDuration;
        cooldownOut = cooldownDurationOut;
        
    }
    void Update()
    {
        if (!TriangleExperiment) {
            
            if (Input.GetMouseButtonDown(0) && liveOutGrenades.Count > 0) {
                Trigger(0);
                
            }
            else if(Input.GetMouseButtonDown(0) && outwardAmmo > 0 && cooldownOut <= 0){
                Shoot(0);
            }

        }
        else {
            if(Input.GetMouseButtonDown(0) && outwardAmmo > 0 && cooldownOut <= 0)
            {
                ShootTriangle();
                GameObject.Find("Triangle Fire Audio").GetComponent<AudioSource>().PlayOneShot(TriangleFire);
            }
        }
        
        if(Input.GetMouseButtonDown(1) && liveInGrenades.Count > 0) {
            Trigger(1);
        }
        else if(Input.GetMouseButtonDown(1) && inwardAmmo > 0 && cooldownIn <= 0)
        {
            Shoot(1);
        }

        //here so that it updates when we get upgrades
        cooldownDurationIn = PlayerStats.Singleton.cooldownDuration;
        cooldownDurationOut = PlayerStats.Singleton.cooldownDuration;
        cooldownIn -= Time.deltaTime;
        cooldownOut -= Time.deltaTime;

    }
    void Shoot(int type)
    {
        
        GameObject.Find("Grenade Fire Audio").GetComponent<AudioSource>().PlayOneShot(GrenadeFireAudio);
        Debug.LogWarning("Shooting!");
        if(type == 0 && outwardAmmo == 1 || type == 1 && inwardAmmo == 1)
        {

            Vector3 playerPosition = transform.position;
            Vector3 direction = (pm.mouseposition - playerPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Sets angle of projectile spawning aiming towards player's current mouse position.
            GameObject spawnedGrenade = Instantiate(type == 0 ? outProjectile : inProjectile, playerPosition, Quaternion.Euler(0, 0, angle));

            spawnedGrenade.GetComponent<GravityGunBehaviour>().SetDirection(direction);
            var grenadeRb = spawnedGrenade.GetComponent<Rigidbody2D>();
            Vector2 velocity = direction * LaunchSpeed;
            grenadeRb.linearVelocity = velocity;
            spawnedGrenade.GetComponent<GravityGunBehaviour>()?.Init(pm.mouseposition, type);
            if(type == 0) {
                outwardAmmo = 0;
                cooldownOut = cooldownDurationOut;

            }
                
            else {
                inwardAmmo = 0;
                cooldownIn = cooldownDurationIn;
            }
                
        }
    }
    protected void Trigger(int type)
    {
        
        GameObject.Find("Grenade Explode Audio").GetComponent<AudioSource>().PlayOneShot(GrenadeExplode);
        if(type == 0 && liveOutGrenades.Count <= 0 || type == 1 && liveInGrenades.Count <= 0) return;
        if(type == 0)
        {
            liveOutGrenades[0].GetComponent<GravityGunBehaviour>().Explode(); 
            outwardAmmo = 1;
        }
        else
        {
            liveInGrenades[0].GetComponent<GravityGunBehaviour>().Implode();
            inwardAmmo = 1;
        }
    }

    void ShootTriangle() 
    {
        cooldownOut = cooldownDurationOut;
        pm = FindFirstObjectByType<PlayerMovement>();
        Vector3 playerPosition = transform.position;
        Vector3 direction = (pm.mouseposition - playerPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Sets angle of projectile spawning aiming towards player's current mouse position.
        GameObject spawnedKnife = Instantiate(trianglePrefab, playerPosition, Quaternion.Euler(0, 0, angle));

        spawnedKnife.GetComponent<TriangleGunBehaviour>().SetDirection(direction);

    }
}

