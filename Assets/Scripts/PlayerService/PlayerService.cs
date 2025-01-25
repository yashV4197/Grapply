using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    private PlayerController playerController;

    public PlayerService(PlayerView playerView)
    {
        playerController = new PlayerController(playerView);
    }

    public PlayerController GetPlayerController() => playerController;

}
