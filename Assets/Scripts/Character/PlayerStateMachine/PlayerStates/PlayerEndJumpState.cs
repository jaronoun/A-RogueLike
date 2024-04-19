using UnityEngine;

public class PlayerEndJumpState : PlayerState
{
    
    public PlayerEndJumpState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.EndJump)
    {
        PlayerContext playerContext = context;
    }

    public override void Enter() 
    {
        Debug.Log("Player End Jump");
        context.playerAnim.StartEndJump();
    }
    
    public override void Update() 
    {
        AnimatorStateInfo stateInfo = context.playerAnim.GetAnimationStateInfo();
        if (stateInfo.IsName("End Jump") && stateInfo.normalizedTime >= 0.25f) {
            if (context.playerMove.currentMove != Vector2.zero) context.playerState.ChangeState(PlayerStateManager.EPlayerState.Walking);
            else context.playerState.ChangeState(PlayerStateManager.EPlayerState.Idle);
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