using UnityEngine;

public class EnemyController
{
    private EnemyView enemyView;
    private EnemyDataSO enemyData;
    private EnemyData currentEnemyData;
    private float timer;

    public EnemyController(EnemyView enemyPrefab,EnemyDataSO enemyDataSO)
    {
        enemyView = Object.Instantiate(enemyPrefab);
        this.enemyView.SetController(this);
        this.enemyData = enemyDataSO;
    }

    public void ActivateEnemy()
    {
        enemyView.gameObject.SetActive(true);
        enemyView.GetRigidbody2D().velocity = Vector3.zero;
        timer = 0f;
    }
    public void ReturnToPool()
    {
        enemyView.gameObject.SetActive(false);
        GameService.Instance.EnemyService.ReturnToPool(this);
    }

    public Transform GetEnemyTransform()
    {
        return enemyView.transform;
    }

    public void SetCurrentEnemyData(GameMode gameMode)
    {
        foreach(var item in enemyData.EnemyDatas)
        {
            if(item.GameMode == gameMode)
            {
                currentEnemyData = item;
                return;
            }
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer>currentEnemyData.DestroyTime)
        {
            ReturnToPool();
            timer = 0f;
        }
    }
}