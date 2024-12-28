using UnityEngine;

public class BoomerangGunBehaviour : ProjectileBehaviour
{
    BoomerangGunController bc;
    Rigidbody2D rb;
    bool recalled = false;
    public GameObject recallHitbox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        bc = FindFirstObjectByType<BoomerangGunController>();

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bc.speed;
        
        recallHitbox = transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        if (rb != null)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime);
        }
        
        if (recalled) {
            Vector2 directionToPlayer = (bc.transform.position - transform.position).normalized;
            rb.linearVelocity = directionToPlayer * bc.returnSpeed;
        }
    }

    public void ReturnToPlayer() {
        recalled = true;
        recallHitbox.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && recalled)
        {
            bc.ammo++;
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy" && recalled)
        {
            var eh = collision.gameObject.GetComponent<EnemyHealth>();
            eh.currentHealth -= bc.damage;
        }
    }
}
