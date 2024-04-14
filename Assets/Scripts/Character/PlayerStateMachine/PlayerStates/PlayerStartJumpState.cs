using UnityEngine;

public class PlayerStartJumpState : PlayerState
{
    public PlayerStartJumpState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.StartJump)
    {
        PlayerContext playerContext = context;
    }

    public override void Enter() 
    {
        Debug.Log("Player Start Jump");
        context.playerAnim.StartJump();
    }

    public override void Update() 
    {
        AnimatorStateInfo stateInfo = context.playerAnim.GetAnimationStateInfo();
        if (stateInfo.IsName("StartJump") && stateInfo.normalizedTime >= 0.8f) { 
            context.playerState.ChangeState(PlayerStateManager.EPlayerState.MidJump); 
            return; 
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