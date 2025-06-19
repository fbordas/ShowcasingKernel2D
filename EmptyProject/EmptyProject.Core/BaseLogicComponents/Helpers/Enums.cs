using System;

#pragma warning disable IDE0130 // i don't care if namespace doesn't match folder structure, STFU VS
namespace EmptyProject.Core.BaseLogicComponents
{
    [Flags]
    public enum AnimationTypes
    {
        Idle,
        Interactable,
        Looping,
        Interruptible,
        Unskippable,
        Grounded,
        Airborne,
        Climbing,
        WallSliding,
        Dashing
    }

    [Flags]
    public enum PlayerState
    { 
        Idle,
        Dashing,
        Running,
        Jumping,
        Falling,
        Shooting,
        Slashing,
        TakingDamage,
        Climbing,
        WallSliding,
        EnteringDoor
    }
}
