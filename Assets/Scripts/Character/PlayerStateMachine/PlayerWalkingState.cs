using UnityEngine;

public class PlayerWalkingState : PlayerState
{

    public PlayerWalkingState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Walking)
    {
    }

    public override void Enter() 
    {
        context.player.SetGrounded(true);
        context.player.SetWalking(true);
        context.player.SetRunning(false);
        context.player.SetFalling(false);
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