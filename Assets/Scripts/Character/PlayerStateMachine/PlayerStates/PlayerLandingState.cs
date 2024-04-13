using UnityEngine;

public class PlayerLandingState : PlayerState
{
    public PlayerLandingState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Landing)
    {
        PlayerContext playerContext = context;
    }

    public override void Enter() 
    {
        Debug.Log("Player Landing");
        context.playerAnim.SetMidJump(false);
        context.playerAnim.SetGrounded(true);
        context.playerAnim.SetWalking(false);
        context.playerAnim.SetRunning(false);
    }
    public override void Update() {}
    public override void Exit() {}

    public override PlayerStateManager.EPlayerState GetNextState() 
    {
        return stateKey;
    }
    
    public override void OnTriggerEnter(Collider other) {}
    public override void OnTriggerExit(Collider other) {}
    public override void OnCollisionEnter(Collision other) {}
    
}