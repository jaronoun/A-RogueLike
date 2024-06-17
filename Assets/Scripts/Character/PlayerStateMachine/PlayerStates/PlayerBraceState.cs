namespace Character.PlayerStateMachine.PlayerStates
{
    using UnityEngine;
    public class PlayerBraceState : PlayerState
    {
        public PlayerBraceState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Brace)
        {
        }

        public override void Enter() 
        {
            context.playerAnim.StartBrace();
        }

        public override void Update() 
        {
            if (!context.playerClimb.isHanging) context.playerState.ChangeState(PlayerStateManager.EPlayerState.MidJump);
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