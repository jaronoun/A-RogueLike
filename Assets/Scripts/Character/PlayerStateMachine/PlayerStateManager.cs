using UnityEngine;

public class PlayerStateManager : StateManager<PlayerStateManager.EPlayerState>
{
    private PlayerAnimation playerAnimation;
    
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
    }

    // When changing states, ensure the appropriate animation is triggered
    

    void TriggerAnimationForState(EPlayerState state)
    {
        switch (state)
        {
            case EPlayerState.Idle:
                playerAnimation.SetWalking(false);
                playerAnimation.SetRunning(false);
                break;
            case EPlayerState.Walking:
                playerAnimation.SetWalking(true);
                break;
            case EPlayerState.Running:
                playerAnimation.SetRunning(true);
                break;
            // Handle other states similarly
        }
    }

}