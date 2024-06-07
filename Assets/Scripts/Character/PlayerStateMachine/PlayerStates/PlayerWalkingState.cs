using UnityEngine;

namespace Character.PlayerStateMachine.PlayerStates
{
    public class PlayerWalkingState : PlayerState
    {

        public PlayerWalkingState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Walking)
        {
        }

        public override void Enter() 
        {
            Debug.Log("Player Walking");
            context.playerAnim.StartWalking();
        }

        public override void Update() 
        {
            if (context.playerJmp.isPlayerJumping) return; 
            if (!context.playerJump.isPlayerGrounded) context.playerState.ChangeState(PlayerStateManager.EPlayerState.MidJump);
            Vector2 movement = context.playerMove.currentMove;
            context.playerAnim.SetMovement(movement.x, movement.y);
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