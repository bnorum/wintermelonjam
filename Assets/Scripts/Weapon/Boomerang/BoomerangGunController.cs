using UnityEngine;
using UnityEngine.InputSystem;

public class BoomerangGunController : WeaponController
{
    [Header("Boomerang Gun Settings")]
    public int maxAmmo;
    public int ammo;
    private float returnSpeed;
    public float spikyDamage = 0;

    public Sprite regular;
    public Sprite zap;
    public AudioClip shootSound;
    public AudioClip returnSound;

    public float zapDuration = 0.3f;
    private float zapCooldown = 0;
    private bool zapping = false;

    private PlayerInput playerInput;
    private InputAction returnAction;

    void Awake()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
        string actionName = LoadingParameters.isUsingMobileControls ? "Primary_mcOn" : "Primary_mcOff";
        returnAction = playerInput.actions[actionName];
    }

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

        if (zapping)
        {
            zapCooldown -= Time.deltaTime;
            if (zapCooldown <= 0)
            {
                zapping = false;
                zapCooldown = zapDuration;
                gsh.GetComponent<SpriteRenderer>().sprite = regular;
            }
        }

        if (returnAction.WasPressedThisFrame())
        {
            GameObject.Find("Boomerang Return Audio").GetComponent<AudioSource>().pitch = 1;
            GameObject.Find("Boomerang Return Audio").GetComponent<AudioSource>().PlayOneShot(returnSound);
            ReturnBoomerangs();
        }

        if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }

        spikyDamage = PlayerStats.Singleton.boomerangSpiky;
    }

    protected override void Shoot()
    {
        if (ammo > 0)
        {
            GunSpriteHolder gunHolder = FindFirstObjectByType<GunSpriteHolder>();
            GameObject.Find("Boomerang Throw Audio").GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            GameObject.Find("Boomerang Throw Audio").GetComponent<AudioSource>().PlayOneShot(shootSound);
            ammo--;

            Debug.Log("Shooting from Boomerang");
            base.Shoot();

            Vector3 playerPosition = transform.position;
            Vector3 direction = gunHolder.direction;
            float angle = gunHolder.transform.eulerAngles.z; 

            GameObject proj = Instantiate(prefab, playerPosition, Quaternion.Euler(0, 0, angle));
            proj.GetComponent<BoomerangGunBehaviour>().SetDirection(direction);
        }
    }

    protected void ReturnBoomerangs()
    {
        foreach (GameObject proj in GameObject.FindGameObjectsWithTag("BoomerangBullet"))
        {
            proj.GetComponent<BoomerangGunBehaviour>().ReturnToPlayer();
            gsh.GetComponent<SpriteRenderer>().sprite = zap;
            zapCooldown = zapDuration;
            zapping = true;
        }
    }
}
