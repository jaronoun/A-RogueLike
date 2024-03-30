using UnityEngine;

public class PlayerContext
{

    private PlayerAnimation playerAnimation;
    
    public PlayerContext(
        PlayerAnimation playerAnimation
        ) 
    {
        this.playerAnimation = playerAnimation;
    }

    public PlayerAnimation player => playerAnimation;
    
}