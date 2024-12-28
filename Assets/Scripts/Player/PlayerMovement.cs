using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    Rigidbody2D rb;
    public Vector2 movement; //probably shouldnt be public, need it for now

    public Vector3 mouseposition;
    private Camera mainCamera;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        InputMovement();
        InputMouse();
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
        rb.linearVelocity= new Vector2(movement.x * currentSpeed, movement.y * currentSpeed);
    }
}
