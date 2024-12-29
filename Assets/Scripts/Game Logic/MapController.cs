using System.Collections.Generic;
using UnityEngine;
public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;

    public GameObject currentChunk;
    PlayerMovement pm;
    void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
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
            }
        }
        else if(pm.movement.x < 0 && pm.movement.y == 0) // left
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("Left Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x == 0 && pm.movement.y > 0) // up
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Top Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("Top Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x == 0 && pm.movement.y < 0) // down
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Bottom Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("Bottom Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x > 0 && pm.movement.y > 0) // up right
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("TopRight Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("TopRight Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x < 0 && pm.movement.y > 0) // up left
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("TopLeft Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("TopLeft Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x > 0 && pm.movement.y < 0) // down right
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("BottomRight Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("BottomRight Chunk").position;
                SpawnChunk();
            }
        }
        else if(pm.movement.x < 0 && pm.movement.y < 0) // down left
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("BottomLeft Chunk").position, checkerRadius, terrainMask))
            {
                noTerrainPosition =currentChunk.transform.Find("BottomLeft Chunk").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0,terrainChunks.Count);
        Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
    }
}
