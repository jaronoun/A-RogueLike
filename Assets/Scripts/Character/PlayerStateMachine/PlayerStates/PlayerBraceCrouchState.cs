using UnityEngine;

namespace Character.PlayerStateMachine.PlayerStates
{
    public class PlayerBraceCrouchState : PlayerState
    {
        public PlayerBraceCrouchState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.BraceCrouch)
        {
        }

        public override void Enter()
        {
            context.playerAnim.StartBraceCrouch();
        }

        public override void Update()
        {
            AnimatorStateInfo stateInfo = context.playerAnim.GetAnimationStateInfo();
            if (stateInfo.IsName("Brace Crouch") && stateInfo.normalizedTime >= 0.1f) { 
                context.playerState.ChangeState(PlayerStateManager.EPlayerState.Crouch); 
                return; 
            }
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