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
    private bool canPause;
    public int CurrentBaloonsCollected { get { return currentBaloonsCollected; } }
    private bool isEndless;
    private float endlessTimer;
    private bool firstTime;

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
        endlessTimer = 0;
        CheckCanPauseStatus();
        CheckFirstTimeStatus();
    }

    private void CheckCanPauseStatus()
    {
        if(PlayerPrefs.GetInt("FirstTime",1)==1)
        {
            canPause = false;
            return;
        }
        canPause = true;
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
        if(isEndless==false)
        {
            UpdateTimer();
        }
        
    }

    private void UpdateTimer()
    {
        inGameUIView.GetTimerSecondsTextParent().SetActive(true);
        int temp = (int)currentTimer;
        inGameUIView.GetTimerSecondsText().text = temp.ToString();
    }         

    private void UpdateEndlessTimer(float time)
    {
        endlessTimer += time;
        int temp = (int)endlessTimer;
        inGameUIView.GetTimerSecondsText().text = temp.ToString();
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
            if (firstTime == false)
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
                else
                {
                    UpdateEndlessTimer(Time.deltaTime);
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
        if (canPause == true)
        {
            if (isPaused == true)
            {
                Time.timeScale = 1f;
                inGameUIView.GetGamePausedMenu().SetActive(false);
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0f;
                inGameUIView.GetGamePausedMenu().SetActive(true);
                isPaused = true;
            }
        }
    }

    public void TogglePause(bool toggle)
    {
        if (canPause == true)
        {
            if (toggle == true)
            {
                Time.timeScale = 0f;
                inGameUIView.GetGamePausedMenu().SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                inGameUIView.GetGamePausedMenu().SetActive(false);
                isPaused = false;
            }
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

    public void CheckFirstTimeStatus()
    {
        if(PlayerPrefs.GetInt("FirstSpace",1)==1)
        {
            firstTime = true;
        }
        else
        {
            firstTime = false;
        }
    }
}