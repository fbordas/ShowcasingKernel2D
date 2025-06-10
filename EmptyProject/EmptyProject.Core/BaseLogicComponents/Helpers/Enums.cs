using System;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace EmptyProject.Core.BaseLogicComponents
{
    [Flags]
    public enum AnimationTypes
    {
        Idle,
        Interactable,
        Looping,
        Interruptible,
        FacingRight,
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
        Shooting,
        Slashing,
        TakingDamage
    }
}
