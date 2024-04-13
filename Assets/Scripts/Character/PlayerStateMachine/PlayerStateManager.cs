using UnityEngine;

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
        Falling,
        Landing,
        Climbing,
    }

    void Awake() {
        PlayerAnimation playerAnimation = GetComponent<PlayerAnimation>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        PlayerJump playerJump = GetComponent<PlayerJump>();
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
        states.Add(EPlayerState.Falling, new PlayerFallingState(playerContext));
        states.Add(EPlayerState.Landing, new PlayerLandingState(playerContext));
        currentState = states[EPlayerState.Idle];
    }

    void Update()
    {
        currentState.Update();
    }

    public void HandleMovement(Vector2 movement, bool isRunning, bool isGrounded) 
    {
        if (!isGrounded) return;
        if (isRunning) {
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