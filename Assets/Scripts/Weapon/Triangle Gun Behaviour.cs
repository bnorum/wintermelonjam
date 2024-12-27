using UnityEngine;

public class TriangleGunBehaviour : ProjectileBehaviour
{
    TriangleGunController tc;
    private int pierced = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        tc = FindFirstObjectByType<TriangleGunController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = direction * tc.speed * Time.deltaTime;
        newPosition.z = 0;
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            pierced++;
            if(pierced >= tc.pierce)
                Destroy(gameObject);
        }
    }
}
