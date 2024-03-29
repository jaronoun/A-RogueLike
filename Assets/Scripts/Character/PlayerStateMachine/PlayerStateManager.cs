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
        //Crouching,
        //Sliding,
        Climbing,
        //Swimming,
        //Diving,
        //Drowning,
        //Dead
    }

    void Awake() {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerContext = new PlayerContext();
        InitializePlayerStates();

    }

    // When changing states, ensure the appropriate animation is triggered

    // void TriggerAnimationForState(EPlayerState state)
    // {
    //     switch (state)
    //     {
    //         case EPlayerState.Idle:
    //             playerAnimation.SetWalking(false);
    //             playerAnimation.SetRunning(false);
    //             break;
    //         case EPlayerState.Walking:
    //             playerAnimation.SetWalking(true);
    //             break;
    //         case EPlayerState.Running:
    //             playerAnimation.SetRunning(true);
    //             break;
    //         // Handle other states similarly
    //     }
    // }

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

}