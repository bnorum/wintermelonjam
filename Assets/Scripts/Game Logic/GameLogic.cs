using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameLogic : MonoBehaviour
{
    [Header("Enemy Spawning Settings")]
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public int enemyLimit = 10;
    private Camera mainCamera;
    public List<GameObject> activeEnemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
        Debug.Log($"Movement Ability Selected: {LoadingParameters.movementAbility}");
        Debug.Log($"Weapon Selected: {LoadingParameters.movementAbility}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (activeEnemies.Count < enemyLimit) SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
        
    }

    private void SpawnEnemy()
    {
        //bounds
        Vector3 screenMin = mainCamera.ScreenToWorldPoint(new Vector3(0,0, mainCamera.nearClipPlane));
        Vector3 screenMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        float xMin = screenMin.x - spawnDistance;
        float xMax = screenMax.x + spawnDistance;
        float yMin = screenMin.y - spawnDistance;
        float yMax = screenMax.y + spawnDistance;

        int side = UnityEngine.Random.Range(0,4);
        Vector2 spawnPosition = Vector2.zero;

        switch(side)
        {
            case 0: //top
                spawnPosition = new Vector2(UnityEngine.Random.Range(xMin, xMax), yMax);
                break;
            case 1: //bottom
                spawnPosition = new Vector2(UnityEngine.Random.Range(xMin, xMax), yMin);
                break;
            case 2: //left
                spawnPosition = new Vector2(xMin, UnityEngine.Random.Range(yMin, yMax));
                break;
            case 3: //right
                spawnPosition = new Vector2(xMax, UnityEngine.Random.Range(yMin, yMax));
                break;
        }
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().Initialize(player);
        activeEnemies.Add(enemy);
    }
}
