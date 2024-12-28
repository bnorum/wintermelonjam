using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    public float moveSpeed;
    public float weight;
    Rigidbody2D rb;
    private bool displaced = false;

    //dirty hack to get displaced off cooldown
    float displacedtimer = .2f;

    public void Initialize(Transform playerTarget)
    {
        player = playerTarget;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //dirty hack to get displaced off cooldown
        if (displaced)
        {
            displacedtimer -= Time.deltaTime;
            if (displacedtimer <= 0)
            {
                displaced = false;
                displacedtimer = .2f;
            }
        }

        if (player != null)
        {
            // Move toward the player
            
            if (rb != null && !displaced)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.linearVelocity = direction * moveSpeed;
            }
        }
    }

    public IEnumerator Knockback(Vector2 direction, float strength) {
        displaced = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * strength / weight, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        displaced = false;

    }
    
}