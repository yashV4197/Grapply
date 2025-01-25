using UnityEngine;

public class EnemyView: MonoBehaviour
{
    private EnemyController enemyController;
    [SerializeField] Rigidbody2D rb2D;
    public void SetController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    private void Update()
    {
        enemyController?.Update();
    }

    public void ReturnToPool()
    {
        enemyController?.ReturnToPool();
    }

    public Rigidbody2D GetRigidbody2D() => rb2D;


}