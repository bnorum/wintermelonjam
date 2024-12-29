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
    public float magnitude = 5f;
    public float damage;
    private bool hasLanded = false;
    private Transform projectileTransform;
    GravityGunController gc;
    int type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(Vector2 target, int inputType)
    {
        startPosition = transform.position;
        targetPosition = target;
        projectileTransform = transform;
        type = inputType;
        damage = PlayerStats.Singleton.damage;
    }
    protected override void Start()
    {
        base.Start();
        gc = FindFirstObjectByType<GravityGunController>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(EnableColliderOnLanding(type));
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


    private IEnumerator EnableColliderOnLanding(int type)
    {
        while (!hasLanded)
        {
            if(Vector2.Distance(transform.position, targetPosition) <= .1f)
            {
                hasLanded = true;
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                if(type == 0)
                    FindFirstObjectByType<GravityGunController>().liveOutGrenades.Add(gameObject);
                else
                    FindFirstObjectByType<GravityGunController>().liveInGrenades.Add(gameObject);
                yield break;
            }
            yield return null;
        }
    }

    public void Explode()
    {
        if(hasLanded)
        {
            magnitude *= 1.5f;
            Impulse(0);
            magnitude /= 1.5f;
        }
    }
    public void Implode()
    {
        if(hasLanded)
            Impulse(1);
    }
    private void Impulse(int impulseType)
    {
        if(impulseType == 0)
            FindFirstObjectByType<GravityGunController>().liveOutGrenades.RemoveAt(0);
        else
            FindFirstObjectByType<GravityGunController>().liveInGrenades.RemoveAt(0);
        if(impulseType == 0)
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
