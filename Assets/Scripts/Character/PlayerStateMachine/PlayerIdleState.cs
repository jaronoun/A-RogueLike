using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Idle)
    {
        PlayerContext playerContext = context;
    }

    public override void EnterState() {}
    public override void UpdateState() {}
    public override void ExitState() {}

    public override PlayerStateManager.EPlayerState GetNextState() 
    {
        return stateKey;
    }
    
    public override void OnTriggerEnter(Collider other) {}
    public override void OnTriggerExit(Collider other) {}
    public override void OnCollisionEnter(Collision other) {}
    
}