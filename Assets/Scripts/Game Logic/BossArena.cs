using UnityEngine;

public class BossArena : MonoBehaviour
{

    public GameObject boss;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerStats.Singleton.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize() {
        player = PlayerStats.Singleton.gameObject;
        GameObject spawnedBoss = Instantiate(boss, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
         
        spawnedBoss.GetComponent<EnemyMovement>().Initialize(player.transform);
    }

    public bool IsPositionInsideArena(Vector3 position) {
        if (position.x > transform.position.x - 10 && position.x < transform.position.x + 10 && position.y > transform.position.y - 10 && position.y < transform.position.y + 10) {
            return true;
        }
        return false;
    }
}
