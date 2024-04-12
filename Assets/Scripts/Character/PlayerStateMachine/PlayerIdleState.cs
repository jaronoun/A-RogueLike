using UnityEngine;

public class PlayerIdleState : PlayerState
{

    public PlayerIdleState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Idle)
    {
    }

    public override void Enter() 
    {
        context.playerAnim.SetGrounded(true);
        context.playerAnim.SetWalking(false);
        context.playerAnim.SetRunning(false);
        context.playerAnim.SetFalling(false);
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