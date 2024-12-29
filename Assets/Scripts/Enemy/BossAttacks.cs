using UnityEngine;

public class BossAttacks : EnemyRangedAttack
{
    

    public override void AttemptRangedAttack()
    {
        Debug.Log("Boss is attempting a ranged attack!");

        if (cooldown <= 0) {
            cooldown = maxCooldown;
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = (transform.position - playerPosition).normalized;
            GameObject spawnedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            spawnedProjectile.GetComponent<EnemyProjectile>().projectileSpeed = projectileSpeed;
            spawnedProjectile.GetComponent<EnemyProjectile>().SetDirection(-direction);
            // Calculate the rotation for the additional projectiles
            Quaternion rotation30 = Quaternion.Euler(0, 0, 30);
            Quaternion rotationMinus30 = Quaternion.Euler(0, 0, -30);

            Vector3 direction30 = rotation30 * direction;
            GameObject projectile30 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile30.GetComponent<EnemyProjectile>().projectileSpeed = projectileSpeed;
            projectile30.GetComponent<EnemyProjectile>().SetDirection(-direction30);

            Vector3 directionMinus30 = rotationMinus30 * direction;
            GameObject projectileMinus30 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileMinus30.GetComponent<EnemyProjectile>().projectileSpeed = projectileSpeed;
            projectileMinus30.GetComponent<EnemyProjectile>().SetDirection(-directionMinus30);
        }
    }

    public void SuperAttack() {
        Debug.Log("Boss is performing a super attack!");

        if (cooldown <= 0) {
            cooldown = maxCooldown;
            Vector3[] directions = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            (Vector3.up + Vector3.left).normalized,
            (Vector3.up + Vector3.right).normalized,
            (Vector3.down + Vector3.left).normalized,
            (Vector3.down + Vector3.right).normalized
            };

            foreach (Vector3 direction in directions) {
                GameObject spawnedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                spawnedProjectile.GetComponent<EnemyProjectile>().projectileSpeed = projectileSpeed;
                spawnedProjectile.GetComponent<EnemyProjectile>().SetDirection(direction);
            }
        }
    }

    public void TeleportRandomly()
    {
        Debug.Log("Boss is teleporting randomly!");

        Vector3 randomPosition = new Vector3(
            Random.Range(transform.position.x - 10, transform.position.x + 10),
            Random.Range(transform.position.y - 10, transform.position.y + 10),
            transform.position.z
        );

        transform.position = randomPosition;
    }
}
