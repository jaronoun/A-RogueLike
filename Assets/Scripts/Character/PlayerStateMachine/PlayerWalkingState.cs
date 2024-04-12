using UnityEngine;

public class PlayerWalkingState : PlayerState
{

    public PlayerWalkingState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Walking)
    {
    }

    public override void Enter() 
    {
        context.playerAnim.SetGrounded(true);
        context.playerAnim.SetWalking(true);
        context.playerAnim.SetRunning(false);
        context.playerAnim.SetFalling(false);
    }

    public override void Update() 
    {
        Vector2 movement = context.playerMove.currentMove;
        context.playerAnim.SetMovement(movement.x, movement.y);
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