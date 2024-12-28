using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float projectileSpeed;
    protected GameObject player;
    protected float cooldown;
    public float maxCooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public virtual void AttemptRangedAttack() {
        if (cooldown <= 0) {
            cooldown = maxCooldown;
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = (transform.position - playerPosition).normalized;
            GameObject spawnedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            spawnedProjectile.GetComponent<EnemyProjectile>().projectileSpeed = projectileSpeed;
            spawnedProjectile.GetComponent<EnemyProjectile>().SetDirection(-direction);
        }
        
    }
}
