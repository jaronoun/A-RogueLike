using UnityEngine;

namespace Character.PlayerStateMachine.PlayerStates
{
    public class PlayerCrouchState : PlayerState
    {
        public PlayerCrouchState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Crouch)
        {
        }

        public override void Enter()
        {
            context.playerAnim.StartCrouch();
        }

        public override void Update()
        {
            if (!context.playerClimb.isHanging) context.playerState.ChangeState(PlayerStateManager.EPlayerState.MidJump);
        }

        public override void Exit() { }

        public override PlayerStateManager.EPlayerState GetNextState()
        {
            return stateKey;
        }

        public override void OnTriggerEnter(Collider other) { }
        public override void OnTriggerExit(Collider other) { }
        public override void OnCollisionEnter(Collision other) { }
    }
}