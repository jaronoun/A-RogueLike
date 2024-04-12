using UnityEngine;

public class PlayerStateManager : StateManager<PlayerStateManager.EPlayerState>
{
    private PlayerContext playerContext;
    
    public enum EPlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Falling,
        Landing,
        Climbing,
    }

    void Awake() {
        PlayerAnimation playerAnimation = GetComponent<PlayerAnimation>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerContext = new PlayerContext(playerAnimation, playerMovement);
        InitializePlayerStates();
    }

    private void InitializePlayerStates()
    {
        states.Add(EPlayerState.Idle, new PlayerIdleState(playerContext));
        states.Add(EPlayerState.Walking, new PlayerWalkingState(playerContext));
        states.Add(EPlayerState.Running, new PlayerRunningState(playerContext));
        states.Add(EPlayerState.Jumping, new PlayerJumpingState(playerContext));
        states.Add(EPlayerState.Falling, new PlayerFallingState(playerContext));
        states.Add(EPlayerState.Landing, new PlayerLandingState(playerContext));
        currentState = states[EPlayerState.Idle];
    }

    void Update()
    {
        currentState.Update();
    }

    public void HandleMovement(Vector2 movement) {
        // Determine whether to change to Walking or Idle
        if (movement != Vector2.zero) ChangeState(EPlayerState.Walking);
        else ChangeState(EPlayerState.Idle);
    }

}