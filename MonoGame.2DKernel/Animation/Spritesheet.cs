using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Kernel2D.Animation
{
    public class Spritesheet
    {
        public Texture2D? Texture { get; set; } = null;
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, SpriteAnimation> Animations = [];
    }
}
