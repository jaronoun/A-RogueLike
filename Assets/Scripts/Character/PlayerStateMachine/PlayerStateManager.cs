using Character.PlayerStateMachine.PlayerStates;
using StateMachine;
using UnityEngine;

namespace Character.PlayerStateMachine
{
    public class PlayerStateManager : StateManager<PlayerStateManager.EPlayerState>
    {
        private PlayerContext playerContext;
    
        public enum EPlayerState
        {
            Idle,
            Walking,
            Running,
            StartJump,
            MidJump,
            EndJump,
            Falling,
            Climbing,
        }

        void Awake() {
            var playerAnimation = GetComponent<PlayerAnimation>();
            var playerMovement = GetComponent<PlayerMovement>();
            var playerJump = GetComponent<PlayerJump>();
            playerContext = new PlayerContext(this, playerAnimation, playerMovement, playerJump);
            InitializePlayerStates();
        }

        private void InitializePlayerStates()
        {
            states.Add(EPlayerState.Idle, new PlayerIdleState(playerContext));
            states.Add(EPlayerState.Walking, new PlayerWalkingState(playerContext));
            states.Add(EPlayerState.Running, new PlayerRunningState(playerContext));
            states.Add(EPlayerState.StartJump, new PlayerStartJumpState(playerContext));
            states.Add(EPlayerState.MidJump, new PlayerMidJumpState(playerContext));
            states.Add(EPlayerState.EndJump, new PlayerEndJumpState(playerContext));
            states.Add(EPlayerState.Falling, new PlayerFallingState(playerContext));
            currentState = states[EPlayerState.Idle];
        }

        private void Update()
        {
            currentState.Update();
        }

        public void HandleMovement(Vector2 movement, bool isRunning) 
        {
            if (currentState.stateKey == EPlayerState.StartJump ||
                currentState.stateKey == EPlayerState.MidJump || 
                currentState.stateKey == EPlayerState.EndJump) return;
            if (movement != Vector2.zero && isRunning) {
                ChangeState(EPlayerState.Running);
                return;
            }
            if (movement != Vector2.zero) {
                ChangeState(EPlayerState.Walking);
            } else {
                ChangeState(EPlayerState.Idle);
            }
        }

        public void HandleJump(bool isGrounded) 
        {
            if (!isGrounded) return;
            ChangeState(EPlayerState.StartJump);
        }

    }
}