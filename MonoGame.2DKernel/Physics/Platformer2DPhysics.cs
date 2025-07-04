namespace MonoGame.Kernel2D.Physics
{
    public struct Platformer2DPhysics
    {
        public float RunSpeed;
        public float DashSpeed;
        public float JumpVelocity;
        public float JumpHeight;
        public float Gravity;
        public float GravityDecay;

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
