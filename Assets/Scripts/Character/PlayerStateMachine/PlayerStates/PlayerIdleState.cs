using UnityEngine;

namespace Character.PlayerStateMachine.PlayerStates
{
    public class PlayerIdleState : PlayerState
    {

        public PlayerIdleState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Idle)
        {
        }

        public override void Enter() 
        {
            Debug.Log("Player Idle");
            context.playerAnim.StartIdle();
        }

        public override void Update() 
        {
        
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