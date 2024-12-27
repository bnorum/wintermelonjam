using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float speed = 5f;
    Rigidbody2D rb;
    public Vector2 movement; //probably shouldnt be public, need it for now

    public Vector3 mouseposition;
    private Camera myCamera;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCamera = FindFirstObjectByType<Camera>();
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
        mouseposition = myCamera.ScreenToWorldPoint(new Vector3(screenMousePosition.x, screenMousePosition.y, myCamera.nearClipPlane));
    }

    void Move()
    {
        rb.linearVelocity= new Vector2(movement.x * speed, movement.y * speed);
    }
}
