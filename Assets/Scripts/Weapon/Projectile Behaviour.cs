using UnityEngine;

/// <summary>
/// Base Script of all projectiles
/// </summary>
public class ProjectileBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        if (destroyAfterSeconds > 0) {
            Destroy(gameObject, destroyAfterSeconds);
        }
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
