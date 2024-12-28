using UnityEngine;

public class xpOrb : MonoBehaviour
{
    public float destroyAfterSeconds = 20f;
    public float value;
    
    Rigidbody2D rb;
    public void Init(float enemyValue)
    {
        value = enemyValue;
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (destroyAfterSeconds > 0) 
        {
            Destroy(gameObject, destroyAfterSeconds);
        } 
    }

}
