namespace Character.PlayerStateMachine.PlayerStates
{
    using UnityEngine;
    
    public class PlayerMidJumpBraceState : PlayerState
    {

        public PlayerMidJumpBraceState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.MidJumpBrace)
        {
        }

        public override void Enter() 
        {
            context.playerAnim.StartMidJumpBrace();
        }

        public override void Update() 
        {
            if (!context.playerClimb.isHanging) context.playerState.ChangeState(PlayerStateManager.EPlayerState.MidJump);
            
            AnimatorStateInfo stateInfo = context.playerAnim.GetAnimationStateInfo();
            if (stateInfo.IsName("Mid Jump Brace") && stateInfo.normalizedTime >= 0.05f) { 
                context.playerState.ChangeState(PlayerStateManager.EPlayerState.Brace); 
                return; 
            }
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