using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    Rigidbody2D rb;
    public Vector2 movement;

    public Animator animator;
    private Vector3 lastPosition;
    private Camera mainCamera;
    private bool isDashing = false;
    private float dashCooldownFull;
    public float dashCooldown;
    private bool isMoving;

    [Header("Sound Settings")]
    public AudioClip[] stepSounds;
    private float stepCooldown;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction dashAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (LoadingParameters.isUsingMobileControls)
        {
            moveAction = playerInput.actions["Move_mcON"];
            dashAction = playerInput.actions["Dash_mcON"];
        }
        else
        {
            moveAction = playerInput.actions["Move_mcOFF"];
            dashAction = playerInput.actions["Dash_mcOFF"];
        }
    }

    void Start()
    {
        dashCooldown = PlayerStats.Singleton.cooldownDuration;
        dashCooldownFull = dashCooldown;
        rb = GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private float noInputTimer = 0f;
    private const float INPUT_THRESHOLD = 0.15f;
    private Vector2 lastValidMovement;

    void Update()
    {
        InputMovement();
        UpdateAnimation();

        dashCooldownFull = PlayerStats.Singleton.cooldownDuration;
        dashCooldown -= Time.deltaTime;

        if (dashAction.WasPressedThisFrame() && dashCooldown <= 0)
        {
            StartCoroutine(Dash());
            dashCooldown = dashCooldownFull;
        }

        // Handle movement smoothing
        Vector2 rawInput = moveAction.ReadValue<Vector2>();
        if (rawInput.magnitude > 0.05f)
        {
            noInputTimer = 0f;
            lastValidMovement = rawInput.normalized;
        }
        else
        {
            noInputTimer += Time.deltaTime;
            if (noInputTimer >= INPUT_THRESHOLD)
            {
                movement = Vector2.zero;
            }
            else
            {
                movement = lastValidMovement;
            }
        }

        if (movement != Vector2.zero && stepCooldown <= 0)
        {
            AudioClip stepSound = stepSounds[Random.Range(0, stepSounds.Length)];
            GameObject.Find("Walking Audio").GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            GameObject.Find("Walking Audio").GetComponent<AudioSource>().PlayOneShot(stepSound);
            stepCooldown = 2 / PlayerStats.Singleton.speed;
        }

        stepCooldown -= Time.deltaTime;
        UpdateMovementStatus();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputMovement()
    {
        Vector2 rawInput = moveAction.ReadValue<Vector2>();
        movement = rawInput.magnitude > 0.05f ? rawInput.normalized : Vector2.zero;
    }

    void Move()
    {
        float currentSpeed = PlayerStats.Singleton.speed;
        if (!isDashing) rb.linearVelocity = new Vector2(movement.x * currentSpeed, movement.y * currentSpeed);
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

    bool wasLastDirectionUp = false;
    void UpdateAnimation()
    {
        if(LoadingParameters.isUsingMobileControls)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();


            if (moveInput.magnitude > 0)
            {
                if (moveInput.y > 0)
                {
                    wasLastDirectionUp = true;
                    animator.Play("player_walking_back");
                }
                else
                {
                    wasLastDirectionUp = false;
                    animator.Play("player_walking_front");
                }
            }
            else
            {
                if (wasLastDirectionUp)
                {
                    animator.Play("player_idle_back");
                }
                else
                {
                    animator.Play("player_idle_front");
                }
            }
        }
        else
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePosition.z = 0;
            bool isMouseAbove = mousePosition.y > transform.position.y;
            if (isMoving)
            {
                animator.Play(isMouseAbove ? "player_walking_back" : "player_walking_front");
            }
            else
            {
                animator.Play(isMouseAbove ? "player_idle_back" : "player_idle_front");
            }
        }
    }
}