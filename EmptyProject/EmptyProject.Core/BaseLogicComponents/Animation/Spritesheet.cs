using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace EmptyProject.Core.BaseLogicComponents.Animation
{
    internal class Spritesheet
    {
        public Texture2D Texture { get; set; }
        public string Name { get; set; }
        public Dictionary<string, SpriteAnimation> Animations;
    }
}
