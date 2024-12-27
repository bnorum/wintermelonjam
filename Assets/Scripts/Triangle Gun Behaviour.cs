using UnityEngine;

public class TriangleGunBehaviour : ProjectileBehaviour
{
    TriangleGunController tc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        tc = FindFirstObjectByType<TriangleGunController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * tc.speed * Time.deltaTime;
    }
}
