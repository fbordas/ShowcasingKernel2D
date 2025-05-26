using XnaRectangle = Microsoft.Xna.Framework.Rectangle;

namespace MonoGame.Kernel2D.Animation
{
    public class AnimationFrame
    {
        public string Name { get; set; } = string.Empty;
        public XnaRectangle SourceRectangle { get; set; }
        public float Duration { get; set; }
    }
}
