
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameService: MonoBehaviour
{
    private static GameService instance;
    public static GameService Instance {  get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Init();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //DATA
    [SerializeField] PlayerView playerView;
    [SerializeField] EnemyView enemyPrefab;
    [SerializeField] EnemyDataSO enemyDataSO;
    [SerializeField] float enemyRadius;
    [SerializeField] List<SpawnRates> spawnRates;
    //Services
    private PlayerService playerService;
    [SerializeField] EnemyService enemyService;

    public PlayerService PlayerService { get {  return playerService; } }
    public EnemyService EnemyService { get { return enemyService; } }

    //ACTIONS
    public UnityAction startGame;

    private void Init()
    {
        playerService = new PlayerService(playerView);
        enemyService.Init(enemyPrefab,enemyDataSO,enemyRadius,spawnRates);
    }



}