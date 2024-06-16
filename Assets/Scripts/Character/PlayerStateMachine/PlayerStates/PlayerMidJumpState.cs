using UnityEngine;

namespace Character.PlayerStateMachine.PlayerStates
{
    public class PlayerMidJumpState : PlayerState
    {
        public PlayerMidJumpState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.MidJump)
        {
            PlayerContext playerContext = context;
        }

        public override void Enter() 
        {
            context.playerAnim.StartMidJump();
        }
        public override void Update() 
        {
            if (context.playerJump.isPlayerGrounded) context.playerState.ChangeState(PlayerStateManager.EPlayerState.EndJump);
        }
        public override void Exit() {}

        public override PlayerStateManager.EPlayerState GetNextState() 
        {
            return stateKey;
        }
    
        public override void OnTriggerEnter(Collider other) {}
        public override void OnTriggerExit(Collider other) {}
        public override void OnCollisionEnter(Collision other) {}
    }
}