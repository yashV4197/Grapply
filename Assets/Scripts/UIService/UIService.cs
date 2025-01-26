
public class UIService 
{
    private LobbyUIController lobbyUIController;

    public UIService(LobbyUIView lobbyUIView)
    {
        lobbyUIController=new LobbyUIController(lobbyUIView);
    }

    public LobbyUIController GetLobbyUIController() => lobbyUIController;


}
