using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    public float moveSpeed;

    public event Action OnEnemyDestroyed;

    public void Initialize(Transform playerTarget)
    {
        player = playerTarget;
    }

    void Update()
    {
        if (player != null)
        {
            // Move toward the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
    void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke();
    }
}