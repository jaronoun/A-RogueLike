using UnityEngine;

public class PlayerStateFactory : MonoBehaviour
{

    public PlayerContext context;
    
    public PlayerStateFactory(PlayerContext context)
    {
        this.context = context;
    }

    public PlayerState Idle()
    {
        return new PlayerIdleState(context);
    }

    public PlayerState Walking()
    {
        return new PlayerWalkingState(context);
    }

    public PlayerState Running()
    {
        return new PlayerRunningState(context);
    }

    public PlayerState Jumping()
    {
        return new PlayerJumpingState(context);
    }

    public PlayerState Falling()
    {
        return new PlayerFallingState(context);
    }

    public PlayerState Landing()
    {
        return new PlayerLandingState(context);
    }

}