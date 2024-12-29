using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GrenadeSprite : MonoBehaviour
{
    public CircleCollider2D myCollider;
    public float destroyAfterSeconds = 1.5f;
    private float damage;
    private float magnitude;
    [Tooltip("True if Push, false for pull")]
    public int impulseType;
    private List<GameObject> damagedEnemies = new List<GameObject>();
    public bool initalized = false;
    public void Init(float inputDamage, float inputMagnitude, int inputImpulseType)
    {
        damage = inputDamage;
        magnitude = inputMagnitude;
        Destroy(gameObject, destroyAfterSeconds);
        impulseType = inputImpulseType;
        initalized = true;
    }

    void OnDestroy()
    {
        damagedEnemies.Clear();
    }


    void Update()
    {
        if(initalized == false) return;
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemies"));
        List<Collider2D> affectedObjects = new List<Collider2D>();
        Physics2D.OverlapCollider(myCollider, affectedObjects);
        float pushRadius = myCollider.radius;
        foreach (Collider2D obj in affectedObjects)
        {
            if (obj.gameObject.tag == "Enemy")
            {
                if(impulseType == 0)
                {
                    Vector2 _direction = (obj.transform.position - transform.position).normalized;
                    float newMagnitude = PlayerStats.Singleton.tempModifier;
                    obj.GetComponent<EnemyMovement>().PushEnemy(_direction, newMagnitude);   
                }
                if(impulseType == 1)
                {
                    Debug.LogWarning("pulling");
                    obj.GetComponent<EnemyMovement>().PullEnemy(gameObject.transform, magnitude);
                }

                // Apply damage
                if(!damagedEnemies.Contains(obj.gameObject))
                {
                    Debug.Log("Dealing Damage");
                    EnemyHealth enemyHealth = obj.GetComponent<EnemyHealth>();
                    enemyHealth.currentHealth -= damage;
                    damagedEnemies.Add(obj.gameObject);
                }
            }
        } 
    }
}