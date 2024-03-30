using UnityEngine;

public class PlayerJumpingState : PlayerState
{
    public PlayerJumpingState(PlayerContext context) : base(context, PlayerStateManager.EPlayerState.Jumping)
    {
        PlayerContext playerContext = context;
    }

    public override void Enter() {}
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