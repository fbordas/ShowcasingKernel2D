using System;

#pragma warning disable IDE0130 // i don't care if namespace doesn't match folder structure, STFU VS
namespace PlatformerGameProject.Core.Helpers
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
}
