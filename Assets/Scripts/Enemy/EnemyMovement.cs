using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    public float moveSpeed;
    public float weight;
    public float range;
    Rigidbody2D rb;
    private bool displaced = false;
    public bool pulled = false;
    private bool pushed = false;
    float pulledTimer;
    Vector2 tempDirection;
    Transform impulseDirection;
    float tempSpeed;
    public bool isBoss = false;

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
                displaced = false;;
                displacedtimer = .3f;
                tempDirection = Vector2.zero;
                tempSpeed = 0f;

            }
        }
        if(pulled)
        {
            if(impulseDirection)
                tempDirection = (impulseDirection.position - transform.position).normalized;
            rb.linearVelocity = tempDirection * tempSpeed;
        
        }
        if (player != null && !displaced && !pulled)
        {
            // Move toward the player
            
            if (range <= 0)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.linearVelocity = (direction+tempDirection) * (moveSpeed+tempSpeed);
            } else {
                if (Vector2.Distance(player.position, transform.position) > range)
                {
                    Vector2 direction = (player.position - transform.position).normalized;
                    rb.linearVelocity = direction * moveSpeed;
                } else if (!isBoss) {
                    rb.linearVelocity = Vector2.zero;
                    GetComponent<EnemyRangedAttack>().AttemptRangedAttack();
                } else {
                    rb.linearVelocity = Vector2.zero;
                    int choice = UnityEngine.Random.Range(0, 2);
                    if (choice == 0)
                    {
                        GetComponent<BossAttacks>().AttemptRangedAttack();
                    } else {
                        GetComponent<BossAttacks>().SuperAttack();
                    }

                }
            }
        }
        tempSpeed *= .99f;
    }

    public IEnumerator Knockback(Vector2 direction, float strength, float duration, bool isDisplaced) {
        displaced = isDisplaced;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * strength / weight, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        displaced = false;

    }
    
    public void PullEnemy(Transform point, float strength)
    {
        pulled = true;
        impulseDirection = point;
        tempSpeed = strength;
    }


    
}