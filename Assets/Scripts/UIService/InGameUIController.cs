using System;
using UnityEngine;

public class InGameUIController
{
    private InGameUIView inGameUIView;
    private InGameModeUIDataSO InGameModeUIData;
    private InGameModeUIDataCollection currentInGameUIDataCollection;
    private bool isGameRunning;
    private float currentTimer;
    private int currentBaloonsCollected;
    private bool isPaused;
    public int CurrentBaloonsCollected { get { return currentBaloonsCollected; } }
    private bool isEndless;

    public InGameUIController(InGameUIView inGameUIView,InGameModeUIDataSO inGameModeUIData)
    {
        this.inGameUIView = inGameUIView;
        this.InGameModeUIData = inGameModeUIData;
        inGameUIView.SetController(this);
        GameService.Instance.startGame += OnGameStart;
    }

    public void OnGameStart()
    {
        ToggleGameRunningStatus(true);
        SetTimer(currentInGameUIDataCollection.Timer);
        UpdateBalloonsCollected(0);
        TogglePause(false);
        ToggleGameWonLostMenu(false);
    }

    public void UpdateBalloonsCollected(int balloonsCollected)
    {
        currentBaloonsCollected=balloonsCollected;
        if (isEndless)
        {
            inGameUIView.GetBaloonsCollectedText().text = currentBaloonsCollected.ToString();
        }
        else
        {
            inGameUIView.GetBaloonsCollectedText().text = currentBaloonsCollected.ToString() + "/" + currentInGameUIDataCollection.BalloonsRequired.ToString();
            CheckIfGameWon();
        }
        
    }

    public void SetTimer(float timer)
    {
        currentTimer = timer;
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if(isEndless==true)
        {
            inGameUIView.GetTimerSecondsTextParent().SetActive(false);
        }
        else
        {
            inGameUIView.GetTimerSecondsTextParent().SetActive(true);
            int temp = (int)currentTimer;
            inGameUIView.GetTimerSecondsText().text = temp.ToString();
        }

    }

    private void ToggleGameRunningStatus(bool isRunning)
    {
        isGameRunning=isRunning;
    }


    public void SetCurrentGameModeDataUI(GameMode gameMode)
    {
        foreach(var item in InGameModeUIData.InGameModeUIDataCollections)
        {
            if(item.GameMode == gameMode)
            {
                currentInGameUIDataCollection=item;
                break;
            }
        }
    }

    public void Update()
    {
        if(isGameRunning)
        {
            if (!isEndless)
            {
                currentTimer -= Time.deltaTime;
                SetTimer(currentTimer);
                if (currentTimer <= 0)
                {
                    OnGameLost();
                }
            }
        }
    }

    public void ExitToLobby()
    {
        ToggleInGameUIStatus(false);
        GameService.Instance.UIService.GetLobbyUIController().ToggleLobbyStatus(true);
    }

    public void RestartGame()
    {
        GameService.Instance.startGame?.Invoke();
    }

    public void TogglePause()
    {
        if(isPaused==true)
        {
            Time.timeScale = 1f;
            inGameUIView.GetGamePausedMenu().SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            inGameUIView.GetGamePausedMenu().SetActive(true);
            isPaused=true;
        }
    }

    public void TogglePause(bool toggle)
    {
        if(toggle==true)
        {
            Time.timeScale = 0f;
            inGameUIView.GetGamePausedMenu().SetActive(true);
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            inGameUIView.GetGamePausedMenu().SetActive(false);
            isPaused=false;
        }
    }
    
    private void OnGameLost()
    {
        inGameUIView.GetGameWonLostText().text = "GAME LOST";
        inGameUIView.GetGameWonLostText().color = Color.red;
        ToggleGameWonLostMenu(true);
    }

    private void OnGameWon()
    {
        inGameUIView.GetGameWonLostText().text = "GAME WON";
        inGameUIView.GetGameWonLostText().color = Color.green;
        ToggleGameWonLostMenu(true);
    }

    private void ToggleGameWonLostMenu(bool toggle)
    {
        if(toggle==true) 
        {
            Time.timeScale = 0f;
            inGameUIView.GetGameWonLostMenu().SetActive(true);
        }
        else
        {
            Time .timeScale = 1f;
            inGameUIView.GetGameWonLostMenu().SetActive(false);
        }
    }

    public void CheckIfGameWon()
    {
        if(currentBaloonsCollected>=currentInGameUIDataCollection.BalloonsRequired)
        {
            OnGameWon();
        }
    }

    public void ToggleInGameUIStatus(bool toggle)
    {
        if(toggle==true)
        {
            inGameUIView.gameObject.SetActive(true);
        }
        else
        {
            inGameUIView.gameObject.SetActive(false);
        }
    }

    public void SetGameModeEndless(bool isEndless)
    {
        this.isEndless = isEndless;
    }
}