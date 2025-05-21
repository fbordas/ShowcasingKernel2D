using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyProject.Core.BaseLogicComponents.Animation
{
    internal class SpriteAnimation
    {
        public string Name { get; set; }
        public List<AnimationFrames> Frames;
        public bool Loop { get; set; }
        public Helpers.AnimationTypes Tags { get; set; }
    }
}
