
public class UIService 
{
    private LobbyUIController lobbyUIController;
    private InGameUIController inGameUIController;
    public UIService(LobbyUIView lobbyUIView,InGameUIView inGameUIView,InGameModeUIDataSO inGameModeUIDataSO)
    {
        lobbyUIController=new LobbyUIController(lobbyUIView);
        inGameUIController=new InGameUIController(inGameUIView,inGameModeUIDataSO);
    }

    public LobbyUIController GetLobbyUIController() => lobbyUIController;
    public InGameUIController GetInGameUIController() => inGameUIController;

}
