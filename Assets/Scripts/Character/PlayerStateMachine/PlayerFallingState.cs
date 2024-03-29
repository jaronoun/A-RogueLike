using UnityEngine;

public class PlayerFallingState : PlayerState
{
    public PlayerFallingState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Falling)
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