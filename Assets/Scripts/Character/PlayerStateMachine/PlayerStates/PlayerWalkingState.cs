using UnityEngine;

public class PlayerWalkingState : PlayerState
{

    public PlayerWalkingState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Walking)
    {
    }

    public override void Enter() 
    {
        Debug.Log("Player Walking");
        context.playerAnim.SetGrounded(true);
        context.playerAnim.SetWalking(true);
        context.playerAnim.SetRunning(false);
        context.playerAnim.SetMidJump(false);
    }

    public override void Update() 
    {
        Vector2 movement = context.playerMove.currentMove;
        context.playerAnim.SetMovement(movement.x, movement.y);
        if (!context.playerJump.isPlayerGrounded) context.playerState.ChangeState(PlayerStateManager.EPlayerState.MidJump);
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