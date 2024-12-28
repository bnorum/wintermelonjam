using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameLogic : MonoBehaviour
{
    [Header("Enemy Spawning Settings")]
    public Transform player;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public int enemyLimit = 40;
    private Camera mainCamera;
    public List<GameObject> activeEnemies = new List<GameObject>();
    [Header("Enemy Pools")]
    public List<GameObject> enemyPool1 = new List<GameObject>();
    public List<GameObject> enemyPool2 = new List<GameObject>();
    public List<GameObject> enemyPool3 = new List<GameObject>();
    public List<GameObject> enemyPool4 = new List<GameObject>();
    public List<GameObject> enemyPool5 = new List<GameObject>();

    [Header("Equip Manager")]
    public List<GameObject> playerWeapons  = new List<GameObject>();
    [Header("Game Attributes")]
    public float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
        Debug.Log($"Movement Ability Selected: {LoadingParameters.movementAbility}");
        Debug.Log($"Weapon Selected: {LoadingParameters.weaponAbility}");

        //equip weapon
        for (int i = 0; i < playerWeapons.Count; i++)
        {
            playerWeapons[i].SetActive(false);
        }
        playerWeapons[LoadingParameters.weaponAbility].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
      timer += Time.deltaTime;
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

        //ugly code below
        GameObject enemy = null;
        if (timer is >= 0 and < 60) { //60 sec
            enemy = Instantiate(enemyPool1[UnityEngine.Random.Range(0,enemyPool1.Count)], spawnPosition, Quaternion.identity);
        }
        else if (timer is >= 60 and < 150) { //2 min 30 sec
            enemy = Instantiate(enemyPool2[UnityEngine.Random.Range(0,enemyPool2.Count)], spawnPosition, Quaternion.identity);
        }
        else if (timer is >= 150 and < 300) { //5 min
            enemy = Instantiate(enemyPool3[UnityEngine.Random.Range(0,enemyPool3.Count)], spawnPosition, Quaternion.identity);
        }
        else if (timer is >= 300 and < 450) { //7 min 30 sec
            enemy = Instantiate(enemyPool4[UnityEngine.Random.Range(0,enemyPool4.Count)], spawnPosition, Quaternion.identity);
        }
        else if (timer is >= 450) { //7 min 30 sec+
            enemy = Instantiate(enemyPool5[UnityEngine.Random.Range(0,enemyPool5.Count)], spawnPosition, Quaternion.identity);
        }
        enemy.GetComponent<EnemyMovement>().Initialize(player);
        activeEnemies.Add(enemy);
    }
}
