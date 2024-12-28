using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GravityGunBehaviour : ProjectileBehaviour
{
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float duration = 1.5f;
    private float elapsedTime = 0f;
    private float arcHeight = 5f;
    private Rigidbody2D rb;
    private bool hasLanded = false;
    GravityGunController gc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(Vector2 target)
    {
        startPosition = transform.position;
        targetPosition = target;
    }
    protected override void Start()
    {
        base.Start();
        gc = FindFirstObjectByType<GravityGunController>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SimulateArc());
    }



    private IEnumerator SimulateArc()
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime/duration;

            Vector3 horizontal = Vector3.Lerp(startPosition, targetPosition, t);
            float verticalOffset = arcHeight * Mathf.Sin(MathF.PI*t);
            transform.position = new Vector3(horizontal.x, horizontal.y + verticalOffset, horizontal.z);   
            yield return null;
        }
        transform.position = targetPosition;
        OnLand();
    }

    private void OnLand()
    {
        rb.linearVelocity = Vector2.zero;
        Collider2D collider = GetComponent<Collider2D>();
        if(collider != null)
            collider.enabled = true;
        Debug.Log("Projectile has Landed");
    }
}
