using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeSprite : MonoBehaviour
{
    public CircleCollider2D myCollider;
    public float destroyAfterSeconds = 1.5f;
    private float damage;
    private float magnitude;
    [Tooltip("True if Push, false for pull")]
    public bool impulseType;
    private List<GameObject> damagedEnemies = new List<GameObject>();
    public void Init(float inputDamage, float inputMagnitude, bool inputImpulseType)
    {
        damage = inputDamage;
        magnitude = inputMagnitude;
        Destroy(gameObject, destroyAfterSeconds);
        impulseType = inputImpulseType;
    }

    void Update()
    {
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> affectedObjects = new List<Collider2D>();
        Physics2D.OverlapCollider(myCollider, affectedObjects);
        foreach (Collider2D obj in affectedObjects)
        {
            if (obj.gameObject.tag == "Enemy")
            {
                // Apply damage
                if(damagedEnemies.Contains(obj.gameObject))
                {
                    Debug.Log("Dealing Damage");
                    EnemyHealth enemyHealth = obj.GetComponent<EnemyHealth>();
                    enemyHealth.currentHealth -= damage;
                    damagedEnemies.Add(obj.gameObject);
                }

                Vector2 direction = (obj.transform.position - transform.position).normalized;
                if(impulseType == true)
                {
                    Debug.LogWarning("pushing");
                    obj.GetComponent<EnemyMovement>().AddMovement(direction, magnitude);
                }
                else
                {
                    Debug.LogWarning("pulling");
                    obj.GetComponent<EnemyMovement>().PullEnemy(gameObject.transform, magnitude);
                }
            }
        } 
    }
}
