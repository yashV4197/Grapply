using System;
using UnityEngine;

public class LobbyUIController
{
    private LobbyUIView lobbyUIView;

    public LobbyUIController(LobbyUIView lobbyUIView)
    {
        this.lobbyUIView = lobbyUIView;
        GameService.Instance.startGame += OnGameStart;
        lobbyUIView.SetController(this);
    }

    public void OnGameStart()
    {
        ToggleLobbyStatus(false);
    }

    //remember this
    public void ToggleLobbyStatus(bool status)
    {
        if (status)
        {
            lobbyUIView.gameObject.SetActive(true);
            lobbyUIView.GetGameModeSelectionMenu().SetActive(false);
        }
        else
        {
            lobbyUIView.gameObject.SetActive(false);
            lobbyUIView.GetGameModeSelectionMenu().SetActive(false);
        }
    }

    public void ToggleGameModeSelectionMenu(bool toggle)
    {
        if(toggle)
        {
            lobbyUIView.GetGameModeSelectionMenu().gameObject.SetActive(true);
        }
        else
        {
            lobbyUIView.GetGameModeSelectionMenu().gameObject.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetGameMode(GameMode mode)
    {
        GameService.Instance.EnemyService.SetGameMode(mode);
        StartGame();
    }

    private void StartGame()
    {
        GameService.Instance.startGame?.Invoke();
    }



}