using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    Rigidbody2D rb;
    public Vector2 movement; //probably shouldnt be public, need it for now

    public Vector3 mouseposition;
    private Camera mainCamera;

    private bool isDashing = false;
    private float dashCooldownFull;
    public float dashCooldown;


    void Start()
    {
        dashCooldown = PlayerStats.Singleton.cooldownDuration;
        dashCooldownFull = dashCooldown;
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        InputMovement();
        InputMouse();

        dashCooldownFull = PlayerStats.Singleton.cooldownDuration; //only here to refresh for upgrades
        dashCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldown <= 0)
        {
            StartCoroutine(Dash());
            dashCooldown = dashCooldownFull;
        }
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

    IEnumerator Dash() 
    {
        GetComponent<PlayerHealth>().invincibilityTime = PlayerStats.Singleton.dashTime;
        float dashSpeed = PlayerStats.Singleton.speed * 2;
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
        rb.linearVelocity = Vector2.zero;

    }
}
