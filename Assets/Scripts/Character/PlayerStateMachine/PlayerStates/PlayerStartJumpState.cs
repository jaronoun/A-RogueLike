using UnityEngine;

public class PlayerStartJumpState : PlayerState
{
    public PlayerStartJumpState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.StartJump)
    {
        PlayerContext playerContext = context;
    }

    public override void Enter() 
    {
        Debug.Log("Player Jumping");
        context.playerAnim.SetGrounded(false);
        context.playerAnim.SetWalking(false);
        context.playerAnim.SetRunning(false);
        context.playerAnim.SetMidJump(false);
        context.playerAnim.StartJump();
    }
    public override void Update() 
    {
        if (!context.playerJump.isPlayerGrounded)
        {
            context.playerState.ChangeState(PlayerStateManager.EPlayerState.MidJump);
        }
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