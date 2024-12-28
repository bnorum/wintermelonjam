using UnityEngine;

public class xpOrb : MonoBehaviour
{
    public int value = 1;
    public float destroyAfterSeconds = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (destroyAfterSeconds > 0) 
        {
            Destroy(gameObject, destroyAfterSeconds);
        } 
    }

}
