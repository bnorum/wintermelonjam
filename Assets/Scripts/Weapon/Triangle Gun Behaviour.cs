using UnityEngine;

public class TriangleGunBehaviour : ProjectileBehaviour
{
    TriangleGunController tc;
    int piercesRemaining;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        tc = FindFirstObjectByType<TriangleGunController>();
        piercesRemaining = tc.pierce;
    }

    // Update is called once per frame
    void Update()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * tc.speed;

        if (piercesRemaining < 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var eh = collision.gameObject.GetComponent<EnemyHealth>();
            eh.currentHealth -= tc.damage;

            var bulletRigidbody = GetComponent<Rigidbody2D>();

            

            var enemyMovement = collision.gameObject.GetComponent<EnemyMovement>(); 
            StartCoroutine(enemyMovement.Knockback(direction, (bulletRigidbody.linearVelocity.magnitude/20)));
            piercesRemaining--;
        }
    }
}
