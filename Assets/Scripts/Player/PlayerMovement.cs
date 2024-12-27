using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float speed = 5f;
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
    }

    void Move()
    {
        rb.linearVelocity= new Vector2(movement.x * speed, movement.y * speed);
    }
}
