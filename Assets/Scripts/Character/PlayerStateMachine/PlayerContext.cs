using UnityEngine;

public class PlayerContext
{
    public PlayerStateManager playerStateManager;
    public PlayerAnimation playerAnimation;
    public PlayerMovement playerMovement;
    public PlayerJump playerJump;
    
    public PlayerContext(
        PlayerStateManager playerStateManager,
        PlayerAnimation playerAnimation,
        PlayerMovement playerMovement,
        PlayerJump playerJump
        ) 
    {
        this.playerStateManager = playerStateManager;
        this.playerAnimation = playerAnimation;
        this.playerMovement = playerMovement;
        this.playerJump = playerJump;
    }

    public PlayerStateManager playerState => playerStateManager;
    public PlayerAnimation playerAnim => playerAnimation;
    public PlayerMovement playerMove => playerMovement;
    public PlayerJump playerJmp => playerJump;
    
}