using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSpriteHolder : MonoBehaviour
{
    public float circleRadius = 1.0f;
    public Transform circleCenter;

    public Sprite magnet;
    public Sprite gravityGun;

    private Vector2 lookInput;
    private PlayerInput playerInput;
    private InputAction lookAction; 
    public Vector3 direction;

    void Start()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
        string nameLookAction = LoadingParameters.isUsingMobileControls ? "Look_mcON" : "Look_mcOFF";
        lookAction = playerInput.actions[nameLookAction];
    }

    private Vector3 lastDirection = Vector3.down;

    void Update()
    {
        Vector3 closestPointOnCircle;
        if(lookAction.name == "Look_mcON")
        {
            lookInput = lookAction.ReadValue<Vector2>();
            if (lookInput == Vector2.zero)
            {
                direction = lastDirection;
            }
            else
            {
                direction = new Vector3(lookInput.x, lookInput.y, 0).normalized;
                lastDirection = direction;
            }
            closestPointOnCircle = circleCenter.position + direction * circleRadius;
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0;
            direction = (mousePos - circleCenter.position).normalized;
            closestPointOnCircle = circleCenter.position + direction * circleRadius;
        }
            if (closestPointOnCircle.y < circleCenter.position.y)
                closestPointOnCircle.z = -1;
            else
                closestPointOnCircle.z = 0;
            transform.position = closestPointOnCircle;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public void SetSprite(bool type)
    {
        if(type)
            gameObject.GetComponent<SpriteRenderer>().sprite = magnet;
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gravityGun;
            gameObject.transform.localScale *= 2f;
        }
    }
}
