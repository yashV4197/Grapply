
public class PlayerService
{
    private PlayerController playerController;

    public PlayerService(PlayerView playerView,float grappleSpeed)
    {
        playerController = new PlayerController(playerView,grappleSpeed);
    }

    public PlayerController GetPlayerController() => playerController;

}
