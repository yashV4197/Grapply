using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    private PlayerController playerController;

    public PlayerService(PlayerView playerView,float grappleSpeed)
    {
        playerController = new PlayerController(playerView,grappleSpeed);
    }

    public PlayerController GetPlayerController() => playerController;

}
