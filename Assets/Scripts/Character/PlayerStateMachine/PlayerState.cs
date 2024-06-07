using StateMachine;

namespace Character.PlayerStateMachine
{
    public abstract class PlayerState : BaseState<PlayerStateManager.EPlayerState>
    {
        protected PlayerContext context;

        protected PlayerState(PlayerContext context, PlayerStateManager.EPlayerState stateKey) : base(stateKey)
        {
            this.context = context;
        }
    }
}

    
