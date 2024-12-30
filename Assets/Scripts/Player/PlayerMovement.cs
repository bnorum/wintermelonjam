using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    Rigidbody2D rb;
    public Vector2 movement; //probably shouldnt be public, need it for now

    public Animator animator;
    private Vector3 lastPosition; //animations
    public Vector3 mouseposition;
    private Camera mainCamera;

    private bool isDashing = false;
    private float dashCooldownFull;
    public float dashCooldown;
    private bool isMoving;

    [Header("Sound Settings")]
    public AudioClip[] stepSounds;
    private float stepCooldown;

    void Start()
    {
        dashCooldown = PlayerStats.Singleton.cooldownDuration;
        dashCooldownFull = dashCooldown;
        rb = GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        InputMovement();
        InputMouse();
        UpdateMovementStatus();
        UpdateAnimation();

        dashCooldownFull = PlayerStats.Singleton.cooldownDuration; //only here to refresh for upgrades
        dashCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldown <= 0)
        {
            StartCoroutine(Dash());
            dashCooldown = dashCooldownFull;
        }

        if (movement != Vector2.zero && stepCooldown <= 0)
        {
                AudioClip stepSound = stepSounds[Random.Range(0, stepSounds.Length)];
                GameObject.Find("Walking Audio").GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.2f);
                GameObject.Find("Walking Audio").GetComponent<AudioSource>().PlayOneShot(stepSound);
                stepCooldown = 2 / PlayerStats.Singleton.speed;
        }

        stepCooldown -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Move();
    }
    void InputMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;
    }
    void InputMouse()
    {
        Vector3 screenMousePosition = Input.mousePosition;
        mouseposition = mainCamera.ScreenToWorldPoint(new Vector3(screenMousePosition.x, screenMousePosition.y, mainCamera.nearClipPlane));
        mouseposition.z = 0;
    }

    void Move()
    {
        float currentSpeed = PlayerStats.Singleton.speed;
        if (!isDashing) rb.linearVelocity= new Vector2(movement.x * currentSpeed, movement.y * currentSpeed);
    }
    void UpdateMovementStatus()
    {
        isMoving = transform.position != lastPosition;
        lastPosition = transform.position;
    }

    IEnumerator Dash() 
    {
        int originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Dash");
        
        GetComponent<PlayerHealth>().invincibilityTime = PlayerStats.Singleton.dashTime;
        float dashSpeed = PlayerStats.Singleton.speed * PlayerStats.Singleton.dashSpeed;
        float dashTime = PlayerStats.Singleton.dashTime;
        float dashTimer = 0;
        isDashing = true;
        Vector2 dashDirection = movement;
        rb.linearVelocity = dashDirection * dashSpeed;
        while (dashTimer < dashTime)
        {
            dashTimer += Time.deltaTime;
            yield return null;
        }
        isDashing = false;
        gameObject.layer = originalLayer;
        rb.linearVelocity = Vector2.zero;

    }
    void UpdateAnimation()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane)
        );
        bool isMouseAbove = mousePosition.y > transform.position.y;
        if (isMoving)
        {
            if (isMouseAbove)
            {
                animator.Play("player_walking_back");
            }
            else
            {
                animator.Play("player_walking_front");
            }
        }
        else
        {
                if (isMouseAbove)
            {
                animator.Play("player_idle_back");
            }
            else
            {
                animator.Play("player_idle_front");
            }
        }
    }
}
