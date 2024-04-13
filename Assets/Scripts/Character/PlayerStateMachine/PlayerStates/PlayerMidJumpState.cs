using UnityEngine;

public class PlayerMidJumpState : PlayerState
{
    public PlayerMidJumpState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.MidJump)
    {
        PlayerContext playerContext = context;
    }

    public override void Enter() 
    {
        Debug.Log("Player Mid Jump");
        context.playerAnim.SetMidJump(true);
        context.playerAnim.SetGrounded(false);
        context.playerAnim.SetWalking(false);
        context.playerAnim.SetRunning(false);
    }
    public override void Update() 
    {
        if (context.playerJump.isPlayerGrounded)
        {
            context.playerState.ChangeState(PlayerStateManager.EPlayerState.Landing);
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