namespace Kernel2D.Physics
{
    /// <summary>
    /// Basic physics values for a 2D platformer game.
    /// </summary>
    public struct Platformer2DPhysics
    {
        /// <summary>
        /// Running speed of the player character.
        /// </summary>
        public float RunSpeed;
        /// <summary>
        /// Dashing speed of the player character.
        /// </summary>
        public float DashSpeed;
        /// <summary>
        /// Jumping velocity of the player character.
        /// </summary>
        public float JumpVelocity;
        /// <summary>
        /// Maximum jump height of the player character.
        /// </summary>
        public float JumpHeight;
        /// <summary>
        /// Base gravity to apply to the player character when falling.
        /// </summary>
        public float Gravity;
        /// <summary>
        /// Rate at which gravity decays up to a maximum value.
        /// </summary>
        public float GravityDecay;
        /// <summary>
        /// Gets default physics values for a 2D player character
        /// in a platforming game.
        /// </summary>
        /// <returns>
        /// A set of default physics values.
        /// </returns>
        public static Platformer2DPhysics Default() => new()
        {
            RunSpeed = 200f,
            DashSpeed = 350f,
            JumpVelocity = 7.0f,
            JumpHeight = 2.2f,
            Gravity = 22f,
            GravityDecay = 0.2f
        };
    }
}
