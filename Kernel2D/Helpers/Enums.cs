namespace Kernel2D.Helpers
{
    [Flags]
    public enum BasicAnimationTypes
    {
        None = 0,
        Idle = 1 << 0,
        Interactable = 1 << 1,
        Looping = 1 << 2,
        Interruptible = 1 << 3,
        FacingRight = 1 << 4,
        Unskippable = 1 << 5,
        Grounded = 1 << 6,
        Airborne = 1 << 7
    }
}
