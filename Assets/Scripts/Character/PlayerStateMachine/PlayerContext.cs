namespace Character.PlayerStateMachine
{
    public class PlayerContext
    {
        public PlayerStateManager playerStateManager;
        public PlayerAnimation playerAnimation;
        public PlayerMovement playerMovement;
        public PlayerClimb playerClimb;
        public PlayerJump playerJump;
    
        public PlayerContext(
            PlayerStateManager playerStateManager,
            PlayerAnimation playerAnimation,
            PlayerMovement playerMovement,
            PlayerClimb playerClimb,
            PlayerJump playerJump
        ) 
        {
            this.playerStateManager = playerStateManager;
            this.playerAnimation = playerAnimation;
            this.playerMovement = playerMovement;
            this.playerClimb = playerClimb;
            this.playerJump = playerJump;
        }

        public PlayerStateManager playerState => playerStateManager;
        public PlayerAnimation playerAnim => playerAnimation;
        public PlayerMovement playerMove => playerMovement;
        public PlayerClimb playerClim => playerClimb;
        public PlayerJump playerJmp => playerJump;
    
    }
}