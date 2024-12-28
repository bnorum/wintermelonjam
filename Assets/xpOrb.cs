using UnityEngine;

public class xpOrb : MonoBehaviour
{
    public float destroyAfterSeconds = 20f;
    public float value;
    public void Init(float enemyValue)
    {
        value = enemyValue;
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
