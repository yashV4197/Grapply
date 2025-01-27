
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService: MonoBehaviour
{
    private EnemyPool enemyPool;
    private List<Transform> currentlySpawnedEnemies;
    private bool isGameRunning;
    private float enemyRadius;
    private GameMode currentGameMode;
    private float timer;
    private List<SpawnRates> spawnRates;
    private float currentSpawnRate;
    public void OnGameStart()
    {
        currentlySpawnedEnemies.Clear();
        isGameRunning = true;
        timer = 0f;
        
    }

    public void Init(EnemyView enemyPrefab, EnemyDataSO enemyDataSO, float enemyRadius, List<SpawnRates> spawnRates)
    {
        this.enemyRadius = enemyRadius;
        this.spawnRates=spawnRates;
        enemyPool = new EnemyPool(enemyPrefab, enemyDataSO);
        currentlySpawnedEnemies = new List<Transform>();
        isGameRunning = false;
        GameService.Instance.startGame += OnGameStart;
    }


    private void Update()
    {
        if(isGameRunning)
        {
            timer += Time.deltaTime;
            if(timer>currentSpawnRate)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }
    }

    public void SetGameMode(GameMode gameMode)
    {
        currentGameMode = gameMode;
        SpawnRates item=spawnRates.Find(i=>i.GameMode == gameMode);
        if (item!=null)
        {
            currentSpawnRate = item.spawnRate;
        }
    }

    public void SpawnEnemy()
    {
        EnemyController newEnemyController= enemyPool.GetPooledItem();
        currentlySpawnedEnemies.Add(newEnemyController.GetEnemyTransform());
        newEnemyController.GetEnemyTransform().position = CheckValidPosition();
        newEnemyController.SetCurrentEnemyData(currentGameMode);
        newEnemyController.ActivateEnemy();
    }

    private Vector2 CheckValidPosition()
    {
        bool check;
        Vector2 scrrenPos= new Vector2(UnityEngine.Random.Range(0.5f,Screen.width-1f), UnityEngine.Random.Range(0.5f,Screen.height-1f));
        Vector2 newPos=Camera.main.ScreenToWorldPoint(scrrenPos);
        int attempts = 100;
        do
        {
            check = true;
            foreach (Transform enemyPos in currentlySpawnedEnemies)
            {
                if (Vector2.Distance(newPos, enemyPos.position) <= enemyRadius)
                {
                    check = false;
                }
            }
            attempts--;
        }
        while(check&&attempts>0);
        return newPos;
    }


    public void ReturnToPool(EnemyController enemyController)
    {
        if(currentlySpawnedEnemies.Contains(enemyController.GetEnemyTransform()))
        {
            currentlySpawnedEnemies.Remove(enemyController.GetEnemyTransform());
        }
        enemyPool.ReturnToPool(enemyController);
        
    }

}

[Serializable]
public class SpawnRates
{
    public GameMode GameMode;
    public float spawnRate;
}