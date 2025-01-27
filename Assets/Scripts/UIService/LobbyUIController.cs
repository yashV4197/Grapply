using System;
using Unity.VisualScripting;
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

    public void ToggleLobbyStatus(bool status)
    {
        if (status)
        {
            lobbyUIView.gameObject.SetActive(true);
            lobbyUIView.GetGameModeSelectionMenu().SetActive(false);
            GameService.Instance.UIService.GetInGameUIController().ToggleInGameUIStatus(false);
        }
        else
        {
            lobbyUIView.gameObject.SetActive(false);
            lobbyUIView.GetGameModeSelectionMenu().SetActive(false);
            GameService.Instance.UIService.GetInGameUIController().ToggleInGameUIStatus(true);
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
        GameService.Instance.UIService.GetInGameUIController().SetCurrentGameModeDataUI(mode);
        GameService.Instance.UIService.GetInGameUIController().SetGameModeEndless(false);
        StartGame();
    }

    private void StartGame()
    {
        GameService.Instance.startGame?.Invoke();
    }

    public void OnEndlessModeSelected()
    {
        GameService.Instance.EnemyService.SetGameMode(GameMode.MEDIUM);
        GameService.Instance.UIService.GetInGameUIController().SetCurrentGameModeDataUI(GameMode.MEDIUM);
        GameService.Instance.UIService.GetInGameUIController().SetGameModeEndless(true);
        StartGame();
    }

}