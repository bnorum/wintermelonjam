using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityGunController : MonoBehaviour
{
    private PlayerMovement pm;
    public GameObject inProjectile;
    public int inwardMaxAmmo = 1;
    public int inwardAmmo;

    private float cooldownIn;
    private float cooldownDurationIn;
    private float cooldownTriangle;
    private float cooldownDurationTriangle;

    public GameObject trianglePrefab;
    public List<GameObject> liveInGrenades = new List<GameObject>();

    public Sprite gravityGunBlueShot;
    public Sprite gravityGunRedShot;
    public Sprite defaultSprite; // Add default sprite reference
    public float shotDuration = 0.25f;
    private float grenadeShotTimer = 0;
    private bool grenadeShooting = false;
    public AudioClip genadeSound;
    public AudioClip grenadeExplodeSound;
    public AudioClip triangleSound;


    private PlayerInput playerInput;
    private InputAction returnAction;
    GunSpriteHolder gunHolder;


    void Awake()
    {
        gunHolder = FindFirstObjectByType<GunSpriteHolder>();
        playerInput = FindFirstObjectByType<PlayerInput>();
        string actionName = LoadingParameters.isUsingMobileControls ? "Primary_mcON" : "Primary_mcOFF";
        returnAction = playerInput.actions[actionName];
    }

    void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement>();

        inwardAmmo = inwardMaxAmmo;
        cooldownDurationIn = PlayerStats.Singleton.grenadeCooldownDuration;
        cooldownIn = 0;
    }

    void Update()
    {
        if (grenadeShooting)
        {
            grenadeShotTimer -= Time.deltaTime;
            if (grenadeShotTimer <= 0)
            {
                grenadeShooting = false;
                grenadeShotTimer = shotDuration;
            }
        }

        if (returnAction.WasPressedThisFrame())
        {
            if (liveInGrenades.Count > 0)
            {
                DetonateGrenade();
            }
            else if (inwardAmmo > 0 && cooldownIn <= 0)
            {
                ShootGrenade();
            }
        }
        if(cooldownTriangle <= 0)
        {
            ShootTriangle();
            cooldownDurationTriangle = PlayerStats.Singleton.cooldownDuration;
        } 

        cooldownIn -= Time.deltaTime;
        cooldownTriangle -= Time.deltaTime;
    }

    void ShootGrenade()
    {
        Vector3 direction;
        float angle;
        if(LoadingParameters.isUsingMobileControls)
        {
            direction = gunHolder.direction;
            angle = gunHolder.transform.eulerAngles.z;
        }
        else
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPosition.z = 0;
            direction = mouseWorldPosition - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        Vector3 playerPosition = transform.position;
        GameObject spawnedGrenade = Instantiate(inProjectile, playerPosition, Quaternion.Euler(0, 0, angle));
        spawnedGrenade.GetComponent<GravityGunBehaviour>().SetDirection(direction);

        var grenadeRb = spawnedGrenade.GetComponent<Rigidbody2D>();
        grenadeRb.linearVelocity = direction * PlayerStats.Singleton.grenadeLaunchSpeed;
        if(LoadingParameters.isUsingMobileControls)
            spawnedGrenade.GetComponent<GravityGunBehaviour>()?.Init(playerPosition + direction * 5f);
        else
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPosition.z = 0;
            spawnedGrenade.GetComponent<GravityGunBehaviour>()?.Init(mouseWorldPosition);
        }
        AudioSource.PlayClipAtPoint(genadeSound, transform.position);
        inwardAmmo = 0;
        cooldownIn = cooldownDurationIn;
        liveInGrenades.Add(spawnedGrenade);

        gunHolder.GetComponent<SpriteRenderer>().sprite = gravityGunBlueShot;
        StartCoroutine(ResetSprite());
        grenadeShotTimer = shotDuration;
        grenadeShooting = true;
    }

    void DetonateGrenade()
    {
        liveInGrenades[0].GetComponent<GravityGunBehaviour>().Implode();        
    }

    void ShootTriangle() 
    {
        cooldownTriangle = cooldownDurationTriangle;
        pm = FindFirstObjectByType<PlayerMovement>();
        Vector3 playerPosition = transform.position;
        Vector3 direction = gunHolder.direction;
        float angle = gunHolder.transform.eulerAngles.z;
        GameObject spawnedKnife = Instantiate(trianglePrefab, playerPosition, Quaternion.Euler(0, 0, angle));
        spawnedKnife.GetComponent<TriangleGunBehaviour>().SetDirection(direction);
        gunHolder.GetComponent<SpriteRenderer>().sprite = gravityGunRedShot;
        StartCoroutine(ResetSprite());
        grenadeShotTimer = shotDuration;
        grenadeShooting = true;
    }

    IEnumerator ResetSprite()
    {
        yield return new WaitForSeconds(0.1f);
        gunHolder.GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }
}

