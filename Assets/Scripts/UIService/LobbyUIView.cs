using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIView: MonoBehaviour
{
    private LobbyUIController lobbyUIController;
    [SerializeField] Button startGameButton;
    [SerializeField] Button exitGameButton;
    [SerializeField] GameObject gameModeSelectionMenu;
    [SerializeField] Button gameModeGoBackButton;
    [SerializeField] Button easyModeButton;
    [SerializeField] Button mediumModeButton;
    [SerializeField] Button hardModeButton;


    private void Start()
    {
        startGameButton.onClick.AddListener(OnStartButtonClicked);
        exitGameButton.onClick.AddListener(OnExitButtonClicked);
        easyModeButton.onClick.AddListener(OnEasyModeSelected);
        mediumModeButton.onClick.AddListener(OnMediumModeSelected);
        hardModeButton.onClick.AddListener(OnHardModeSelected);
        gameModeGoBackButton.onClick.AddListener(GoBackGameModeSelection);
    }

    private void GoBackGameModeSelection()
    {
        lobbyUIController?.ToggleGameModeSelectionMenu(false);
    }

    private void OnHardModeSelected()
    {
        lobbyUIController?.SetGameMode(GameMode.HARD);
    }

    private void OnMediumModeSelected()
    {
        lobbyUIController?.SetGameMode(GameMode.MEDIUM);
    }

    private void OnEasyModeSelected()
    {
        lobbyUIController?.SetGameMode(GameMode.EASY);
    }

    private void OnExitButtonClicked()
    {
        lobbyUIController?.ExitGame();
    }

    private void OnStartButtonClicked()
    {
        lobbyUIController?.ToggleGameModeSelectionMenu(true);
    }

    public void SetController(LobbyUIController lobbyUIController)
    {
        this.lobbyUIController = lobbyUIController;
    }

    public GameObject GetGameModeSelectionMenu() => gameModeSelectionMenu;


}