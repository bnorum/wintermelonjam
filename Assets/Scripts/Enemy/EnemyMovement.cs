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
    private bool pulled = false;
    private bool pushed = false;
    float pushedTimer;
    Vector2 tempDirection;
    float tempSpeed;
    public bool isBoss = false;
    float displacedTimerAmount = .2f;

    //dirty hack to get displaced off cooldown
    float displacedtimer;

    public void Initialize(Transform playerTarget)
    {
        player = playerTarget;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (displaced)
        {
            displacedtimer -= Time.deltaTime;
            if (displacedtimer <= 0)
            {
                displaced = false;
                pulled = false;
                tempDirection = Vector2.zero;
                tempSpeed = 0;
                displacedtimer = displacedTimerAmount;

            }
        }
        if(displaced && pulled)
        {
            rb.linearVelocity = tempDirection * tempSpeed;
        }
        if (player != null && !displaced && !pushed)
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
                    //rb.linearVelocity = Vector2.zero;
                    Vector2 direction = (player.position - transform.position).normalized;
                    rb.linearVelocity = direction * moveSpeed;
                    int choice = UnityEngine.Random.Range(0, 3);
                    if (choice == 0)
                    {
                        GetComponent<BossAttacks>().AttemptRangedAttack();
                    } else if (choice == 1){
                        if (UnityEngine.Random.Range(0, 400) == 0)
                        {
                            GetComponent<BossAttacks>().TeleportRandomly();
                        }
                        GetComponent<BossAttacks>().SuperAttack();
                    }  else if (choice == 2) {
                        GetComponent<BossAttacks>().DoNothing();
                    }

                }
            }
        }
    }

    public IEnumerator Knockback(Vector2 direction, float strength, float duration, bool isDisplaced) {
        displaced = isDisplaced;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * strength / weight, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        displaced = false;

    }

    public void PushEnemy(Vector2 direction, float strength)
    {
        displaced = true;
        displacedtimer = displacedTimerAmount;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
    }
    
    public void PullEnemy(Transform point, float strength)
    {
        displaced = true;
        pulled = true;
        displacedtimer = displacedTimerAmount;
        tempDirection = (point.position - transform.position).normalized;
        tempSpeed = strength;
    }


    
}