using UnityEngine;

public class PlayerStateManager : StateManager<PlayerStateManager.EPlayerState>
{
    private PlayerAnimation playerAnimation;
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
        playerAnimation = GetComponent<PlayerAnimation>();
        playerContext = new PlayerContext(playerAnimation);
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

}