using UnityEngine;

public class GunSpriteHolder : MonoBehaviour
{
    public float circleRadius = 1.0f;
    public Transform circleCenter;

    void Update()
    {
        //mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(circleCenter.position).z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        //point on circle
        Vector3 direction = mouseWorldPos - circleCenter.position;
        direction = direction.normalized;
        Vector3 closestPointOnCircle = circleCenter.position + direction * circleRadius;

        //infront / behind
        if (closestPointOnCircle.y < circleCenter.position.y)
            closestPointOnCircle.z = -1;
        else
            closestPointOnCircle.z = 0;
        transform.position = closestPointOnCircle;

        //sprite face up
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
