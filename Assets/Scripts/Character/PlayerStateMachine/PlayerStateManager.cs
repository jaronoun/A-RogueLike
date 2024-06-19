using Character.PlayerStateMachine.PlayerStates;
using StateMachine;
using UnityEngine;

namespace Character.PlayerStateMachine
{
    public class PlayerStateManager : StateManager<PlayerStateManager.EPlayerState>
    {
        private PlayerContext playerContext;
        private string prevState;
    
        public enum EPlayerState
        {
            Idle,
            Walking,
            Running,
            StartJump,
            MidJump,
            EndJump,
            Falling,
            MidJumpBrace,
            Brace,
            Crouch,
            BraceCrouch,
        }

        void Awake() {
            var playerAnimation = GetComponent<PlayerAnimation>();
            var playerMovement = GetComponent<PlayerMovement>();
            var playerClimb = GetComponent<PlayerClimb>();
            var playerJump = GetComponent<PlayerJump>();
            playerContext = new PlayerContext(this, playerAnimation, playerMovement, playerClimb, playerJump);
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
            states.Add(EPlayerState.MidJumpBrace, new PlayerMidJumpBraceState(playerContext));
            states.Add(EPlayerState.Brace, new PlayerBraceState(playerContext));
            states.Add(EPlayerState.Crouch, new PlayerCrouchState(playerContext));
            states.Add(EPlayerState.BraceCrouch, new PlayerBraceCrouchState(playerContext));
            currentState = states[EPlayerState.Idle];
        }

        private void Update()
        {
            currentState.Update();
        }

        public void HandleMovement(Vector2 movement, bool isRunning) 
        {
            if (playerContext.playerClimb.isHanging) return;
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
        public void HandleLedgeClimb(bool isHanging) 
        {
            if (isHanging) ChangeState(EPlayerState.BraceCrouch);
        }
        
        public string GetCurrentState() 
        {
            return currentState.stateKey.ToString();
        }

    }
}