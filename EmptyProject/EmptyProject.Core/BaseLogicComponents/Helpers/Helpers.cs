using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyProject.Core.BaseLogicComponents
{
    public static class Helpers
    {
        [Flags]
        public enum AnimationTypes
        {
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
    }
}
