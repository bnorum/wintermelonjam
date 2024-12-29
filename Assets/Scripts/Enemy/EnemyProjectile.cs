using UnityEngine;
using System;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;
    public float projectileSpeed;
    public float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (destroyAfterSeconds > 0) {
            Destroy(gameObject, destroyAfterSeconds);
        }
    }

    void Update()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerHealth>().currentHealth -= damage;
            Destroy(gameObject);
            StartCoroutine(other.GetComponent<PlayerHealth>().FlashRed());
        }
    }
}
