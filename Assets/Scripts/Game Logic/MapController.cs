using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;

    public GameObject currentChunk;
    PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDistance;
    float opDistance;
    float opCooldown;
    float opCooldownDuration;
    void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement>();
                noTerrainPosition =currentChunk.transform.Find("Top Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("TopRight Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("TopLeft Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Bottom Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("BottomRight Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("BottomLeft Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Left Chunk").position;
                SpawnChunk();
                noTerrainPosition = currentChunk.transform.Find("Right Chunk").position;
                SpawnChunk();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;
        }
        if(pm.movement.x > 0 && pm.movement.y == 0) // right
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("BottomRight Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("TopLeft Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x < 0 && pm.movement.y == 0) // left
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("Left Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Bottom Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("TopLeft Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x == 0 && pm.movement.y > 0) // up
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Top Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("Top Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("TopRight Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("TopLeft Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x == 0 && pm.movement.y < 0) // down
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Bottom Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("Bottom Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("BottomRight Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("BottomLeft Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x > 0 && pm.movement.y > 0) // up right
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("TopRight Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("TopRight Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Top Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Right Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x < 0 && pm.movement.y > 0) // up left
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("TopLeft Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("TopLeft Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Top Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Left Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x > 0 && pm.movement.y < 0) // down right
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("BottomRight Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("BottomRight Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Bottom Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Right Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x < 0 && pm.movement.y < 0) // down left
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("BottomLeft Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("BottomLeft Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Bottom Chunk").position;
                SpawnChunk();
                noTerrainPosition =currentChunk.transform.Find("Left Chunk").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        // Check if there's already a chunk at this position
        Collider2D existingChunk = Physics2D.OverlapCircle(noTerrainPosition, 0.1f, terrainMask);
        if (existingChunk == null)
        {
            int rand = Random.Range(0,terrainChunks.Count);
            latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
            spawnedChunks.Add(latestChunk);   
        }
    }

    void ChunkOptimizer()
    {
        opCooldown -= Time.deltaTime;

        if(opCooldown <= 0f)
        {
            opCooldown = opCooldownDuration;

        }
        else
            return;
        foreach(GameObject chunk in spawnedChunks)
        {
            opDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDistance > maxOpDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
