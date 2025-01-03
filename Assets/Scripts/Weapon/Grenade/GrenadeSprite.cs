using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
        gameObject.transform.localScale *= PlayerStats.Singleton.implodeRadiusModifier;
        damage = inputDamage;
        magnitude = inputMagnitude;
        Destroy(gameObject, destroyAfterSeconds);
        impulseType = inputImpulseType;
        initalized = true;
    }

    void OnDestroy()
    {
        foreach(var enemy in damagedEnemies)
        {
            if(enemy)
            {
                enemy.GetComponent<EnemyMovement>().pulled = false;
                enemy.GetComponent<EnemyMovement>().frozen = false;
                enemy.GetComponent<Collider2D>().isTrigger = false;
            }
        }
        damagedEnemies.Clear();
    }


    void Update()
    {
        if(initalized == false) return;
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemies"));
        List<Collider2D> affectedObjects = new List<Collider2D>();

        Physics2D.OverlapCollider(myCollider, affectedObjects);
        foreach (Collider2D obj in affectedObjects)
        {
            if (obj.gameObject.tag == "Enemy" && obj.GetComponent<EnemyMovement>().pulled == false)
            {
                if(impulseType == 1)
                {
                    if (Vector2.Distance(obj.transform.position, transform.position) > 0.5f)
                    {
                        obj.GetComponent<EnemyMovement>().PullEnemy(gameObject.transform, magnitude);
                        obj.GetComponent<Collider2D>().isTrigger = true;
                        damagedEnemies.Add(obj.gameObject);
                    }
                }
            }
        } 
    }
}
