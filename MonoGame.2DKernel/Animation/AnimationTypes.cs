using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Kernel2D.Animation
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
        Airborne = 1 << 7,
        Moving = 1 << 8,
    }

    public static class AnimationTagHelpers
    {
        public static bool HasTag(this SpriteAnimation anim, BasicAnimationTypes tag) => anim.Tags.HasFlag(tag);

        public static bool HasTag(this SpriteAnimation anim, string tag)
        {
            if (string.IsNullOrEmpty(tag)) return false;
            return anim.ExtraTypes.Contains(tag);
        }
    }
}
