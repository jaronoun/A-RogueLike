using UnityEngine;

public class PlayerContext
{

    public PlayerAnimation playerAnimation;
    public PlayerMovement playerMovement;
    
    public PlayerContext(
        PlayerAnimation playerAnimation,
        PlayerMovement playerMovement
        ) 
    {
        this.playerAnimation = playerAnimation;
        this.playerMovement = playerMovement;
    }

    public PlayerAnimation playerAnim => playerAnimation;
    public PlayerMovement playerMove => playerMovement;
    
}