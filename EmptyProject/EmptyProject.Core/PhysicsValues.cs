namespace EmptyProject.Core
{
    internal struct PhysicsValues
    {
        public float RunSpeed;
        public float DashSpeed;
        public float JumpVelocity;
        public float Gravity;

        public static PhysicsValues Default() => new()
        {
            RunSpeed = 4.0f,
            DashSpeed = 9.0f,
            JumpVelocity = 7.0f,
            Gravity = -9.81f
        };
    }
}
