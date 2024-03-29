using UnityEngine;

public abstract class PlayerState : BaseState<PlayerStateManager.EPlayerState>
{
    protected PlayerContext context;

    public PlayerState(PlayerContext context, PlayerStateManager.EPlayerState stateKey) : base(stateKey)
    {
        this.context = context;
    }
}

    
