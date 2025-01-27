
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

    //VIEWS
    [SerializeField] LobbyUIView lobbyUIView;
    [SerializeField] InGameUIView inGameUIView;

    //DATA
    [SerializeField] PlayerView playerView;
    [SerializeField] EnemyView enemyPrefab;
    [SerializeField] EnemyDataSO enemyDataSO;
    [SerializeField] float enemyRadius;
    [SerializeField] float grappleSpeed;
    [SerializeField] List<SpawnRates> spawnRates;
    [SerializeField] InGameModeUIDataSO inGameModeUIDataSO;
    //Services
    private PlayerService playerService;
    private UIService uIService;
    [SerializeField] EnemyService enemyService;

    public PlayerService PlayerService { get {  return playerService; } }
    public EnemyService EnemyService { get { return enemyService; } }
    public UIService UIService { get { return uIService; } }

    //ACTIONS
    public UnityAction startGame;

    private void Init()
    {
        playerService = new PlayerService(playerView,grappleSpeed);
        enemyService.Init(enemyPrefab,enemyDataSO,enemyRadius,spawnRates);
        uIService = new UIService(lobbyUIView,inGameUIView,inGameModeUIDataSO);
        uIService.GetLobbyUIController().ToggleLobbyStatus(true);
    }



}