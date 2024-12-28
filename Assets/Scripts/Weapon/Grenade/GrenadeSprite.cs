using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeSprite : MonoBehaviour
{
    private Collider2D collider;
    public float destroyAfterSeconds = 1f;
    private float damage;
    private float magnitude;
    [Tooltip("True if Push, false for pull")]
    public bool impulseType;
    public void Init(float inputDamage, float inputMagnitude)
    {
        damage = inputDamage;
        magnitude = inputMagnitude;
        Destroy(gameObject, destroyAfterSeconds);
        collider = GetComponent<Collider2D>();
        StartCoroutine(Damage());
    }

    IEnumerator Damage()
    {
        Collider2D[] affectedObjects = Physics2D.OverlapCircleAll(transform.position, collider.GetComponent<CircleCollider2D>().radius);
        foreach (Collider2D obj in affectedObjects)
        {
            if (obj.gameObject.tag == "Enemy")
            {
                // Apply damage
                EnemyHealth enemyHealth = obj.GetComponent<EnemyHealth>();
                enemyHealth.currentHealth -= damage;

                Vector2 direction = (obj.transform.position - transform.position).normalized;
                int impulseDirection;
                if(impulseType == true)
                    impulseDirection = 1;
                else
                    impulseDirection = -1;
                float force = magnitude * impulseDirection;
                StartCoroutine(obj.GetComponent<EnemyMovement>().Knockback(direction, force));
            }
        }
        yield return null;
    }
    
}
