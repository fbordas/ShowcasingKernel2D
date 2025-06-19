namespace EmptyProject.Core
{
    internal struct PhysicsValues
    {
        public float RunSpeed;
        public float DashSpeed;
        public float JumpVelocity;
        public float JumpHeight;
        public float Gravity;

        public static PhysicsValues Default() => new()
        {
            RunSpeed = 4.0f,
            DashSpeed = 6.0f,
            JumpVelocity = 7.0f,
            JumpHeight = 2.2f,
            Gravity = -9.81f
        };
    }
}
