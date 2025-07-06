using XVector = Microsoft.Xna.Framework.Vector2;
using PlayerState = MonoGame.Kernel2D.Entities.PlatformerPlayerState;

namespace MonoGame.Kernel2D.Entities
{
    /// <summary>
    /// Part of the entity FSM to determine what it's currently "doing".
    /// </summary>
    [Flags]
    public enum PlatformerPlayerState
    {
        /// <summary>
        /// Represents the neutral/default state with no active actions.
        None = 0,
        /// <summary>
        /// Represents the state when the player is not performing any action.
        /// </summary>
        Idle = 1 << 0,  // 1
        /// <summary>
        /// Represents the state when the player is actively running in either
        /// direction.
        /// </summary>
        Running = 1 << 1,  // 2
        /// <summary>
        /// Represents the state when the player is dashing, which is a quick
        /// movement in either direction for a short time.
        /// </summary>
        Dashing = 1 << 2,  // 4
        /// <summary>
        /// Represents the state when the player is jumping.
        /// </summary>
        Jumping = 1 << 3,  // 8
        /// <summary>
        /// Represents the state when the player is falling after a jump or
        /// other airborne or semi-airborne state.
        /// </summary>
        Falling = 1 << 4,  // 16
        /// <summary>
        /// Represents the state when the player is landing after a jump or
        /// other airborne or semi-airborne state.
        /// </summary>
        Landing = 1 << 5,  // 32
        /// <summary>
        /// Represents the state when the player is shooting a long-ranged weapon.
        /// </summary>
        Shooting = 1 << 6,  // 64
        /// <summary>
        /// Represents the state when the player is slashing with a melee weapon.
        /// </summary>
        Slashing = 1 << 7,  // 128
        /// <summary>
        /// Represents the state when the player is taking damage from an enemy or
        /// environmental hazard.
        /// </summary>
        TakingDamage = 1 << 8,  // 256
        /// <summary>
        /// Represents the state when the player is climbing a ladder or other
        /// environmental element that allows vertical movement.
        /// </summary>
        Climbing = 1 << 9,  // 512
        /// <summary>
        /// Represents the state when the player is sliding down a wall or other
        /// environmental element that allows the player to stick to it.
        /// </summary>
        WallSliding = 1 << 10, // 1024
        /// <summary>
        /// Represents the state when the player is entering a door.
        /// </summary>
        EnteringDoor = 1 << 11, // 2048
    }

    /// <summary>
    /// Base class for a 2D platformer player entity in a game.
    /// </summary>
    public abstract class Platformer2DCharacter
    {
        /// <summary>
        /// Unique identifier for the entity in the game world.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the entity is facing right in the game world.
        /// </summary>
        public bool FacingRight { get; set; } = true;

        /// <summary>
        /// Current position of the entity in the game world.
        /// </summary>
        public XVector CurrentPosition { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is currently grounded.
        /// </summary>
        public bool IsGrounded { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the entity is currently airborne.
        /// </summary>
        public bool IsAirborne => HasState(PlayerState.Jumping) || HasState(PlayerState.Falling);

        /// <summary>
        /// Gets or sets a value indicating whether a jump was cut short.
        /// </summary>
        public bool JumpCut { get; set; } = false;

        /// <summary>
        /// Gets or sets the current velocity of the entity in the game world.
        /// </summary>
        public float DeltaTime { get; set; } = 0f;

        /// <summary>
        /// Current state of the entity. Provides maneuverability for
        /// the entity FSM.
        /// </summary>
        public PlayerState CurrentState { get; set; } = PlayerState.None;

        /// <summary>
        /// Gets the current state of the entity.
        /// </summary>
        /// <returns>The current state of the entity.</returns>
        public PlayerState GetState() => CurrentState;
        /// <summary>
        /// Checks if a state is currently active in the FSM.
        /// </summary>
        /// <param name="state">The state to check for.</param>
        /// <returns>True if the state is active, False otherwise.</returns>
        public bool HasState(PlayerState state) => (CurrentState & state) != 0;
        /// <summary>
        /// Adds a state to the current state in the FSM.
        /// </summary>
        /// <param name="state">The state to add.</param>
        public void AddState(PlayerState state) => CurrentState |= state;
        /// <summary>
        /// Removes a state from the current state collection.
        /// </summary>
        /// <param name="state">The state to remove.</param>
        public void RemoveState(PlayerState state) => CurrentState &= ~state;
        /// <summary>
        /// Resets all states and sets a specific state.
        /// </summary>
        /// <param name="state">The state to set.</param>
        public void SetState(PlayerState state) => CurrentState = state;

        /// <summary>
        /// Removes all grounded states from the FSM.
        /// </summary>
        protected virtual void ClearGroundedStates()
        {
            RemoveState(PlayerState.Idle);
            RemoveState(PlayerState.Running);
        }
    }
}
