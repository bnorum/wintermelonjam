using UnityEngine;

public class xpOrb : MonoBehaviour
{
    public float destroyAfterSeconds = 20f;
    public float value;
    
    Rigidbody2D rb;

    public GameObject player;


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

        player = GameObject.Find("Player");
    }

    void Update() {
        if (Vector2.Distance(transform.position, player.transform.position) <= PlayerStats.Singleton.xpSuckRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5 * PlayerStats.Singleton.xpSuckRange / 2);
        }

    }

}
