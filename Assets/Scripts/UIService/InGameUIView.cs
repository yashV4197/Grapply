using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIView:MonoBehaviour
{
    private InGameUIController inGameUIController;
    [SerializeField] TextMeshProUGUI timerSecondsText;
    [SerializeField] TextMeshProUGUI balloonsCollectedText;
    [SerializeField] TextMeshProUGUI gameWonLostText;

    [SerializeField] GameObject gamePausedPopUpMenu;
    [SerializeField] GameObject gameWonLostPopUpMenu;

    [SerializeField] Button resumeGamePausedButton;
    [SerializeField] Button restartButtonGamePaused;
    [SerializeField] Button restartButtonGameWonLost;

    [SerializeField] Button exitToLobbyButtonGamePaused;
    [SerializeField] Button exitToLobbyButtonGameWonLost;

    [SerializeField] GameObject timerSecondsTextParent;
    private void Start()
    {
        restartButtonGamePaused.onClick.AddListener(RestartGame);
        restartButtonGameWonLost.onClick.AddListener(RestartGame);
        exitToLobbyButtonGamePaused.onClick.AddListener(ExitToLobby);
        exitToLobbyButtonGameWonLost.onClick.AddListener(ExitToLobby);
        resumeGamePausedButton.onClick.AddListener(ResumeGame);
    }

    private void ResumeGame()
    {
        inGameUIController?.TogglePause(false);
    }

    private void ExitToLobby()
    {
        inGameUIController?.ExitToLobby();
    }

    private void RestartGame()
    {
        inGameUIController?.RestartGame();
    }

    public void SetController(InGameUIController inGameUIController)
    {
        this.inGameUIController = inGameUIController;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            inGameUIController?.TogglePause();
        }


        inGameUIController?.Update();
    }

    public TextMeshProUGUI GetTimerSecondsText()=>timerSecondsText;
    public TextMeshProUGUI GetBaloonsCollectedText() => balloonsCollectedText;
    public TextMeshProUGUI GetGameWonLostText() => gameWonLostText;
    public GameObject GetGamePausedMenu() => gamePausedPopUpMenu;
    public GameObject GetGameWonLostMenu() => gameWonLostPopUpMenu;

    public GameObject GetTimerSecondsTextParent() => timerSecondsTextParent;

}