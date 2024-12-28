using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class GravityGunBehaviour : ProjectileBehaviour
{
    public GameObject pull;
    public GameObject push;

    private Vector2 targetPosition;
    private Vector2 startPosition;
    private Rigidbody2D rb;
    private Collider2D projectileCollider;
    public float magnitude = 50f;
    public float damage = 1f;
    private bool hasLanded = false;
    private Transform projectileTransform;
    GravityGunController gc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(Vector2 target)
    {
        startPosition = transform.position;
        targetPosition = target;
        projectileTransform = transform;
    }
    protected override void Start()
    {
        base.Start();
        gc = FindFirstObjectByType<GravityGunController>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(EnableColliderOnLanding());
    }

    private void Update()
    {
        if (!hasLanded)
        {
            AdjustScale();
        }
    }

    private void AdjustScale()
    {
        float totalDistance = Vector2.Distance(startPosition, targetPosition);
        float currentDistance = Vector2.Distance(transform.position, targetPosition);
        float progress = Mathf.Clamp01(1 - (currentDistance / totalDistance));
        if(progress <.5)
        {
            progress *= 2;
            float scaleFactor = Mathf.Lerp(0.5f, 1.5f, progress);
            projectileTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        }
        else
        {
            progress -=.5f;
            progress *= 2;
            float scaleFactor = Mathf.Lerp(1.5f, .5f, progress);
            projectileTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        }
    }


    private IEnumerator EnableColliderOnLanding()
    {
        while (!hasLanded)
        {
            if(Vector2.Distance(transform.position, targetPosition) <= .1f)
            {
                hasLanded = true;
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                yield break;
            }
            yield return null;
        }
    }

    public void Explode()
    {
        if(hasLanded)
            Impulse(true);
    }
    public void Implode()
    {
        if(hasLanded)
            Impulse(false);
    }
    private void Impulse(bool impulseType)
    {
        if(impulseType == true)
        {
            var child = Instantiate(push, transform.position, Quaternion.identity);
            child.GetComponent<GrenadeSprite>().Init(damage, magnitude, impulseType);
        }
        else
        {
            var child = Instantiate(pull, transform.position, Quaternion.identity);
            child.GetComponent<GrenadeSprite>().Init(damage, magnitude, impulseType);

        }
        Destroy(gameObject);
    }
}
