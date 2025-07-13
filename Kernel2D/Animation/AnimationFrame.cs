using XRectangle = Microsoft.Xna.Framework.Rectangle;

namespace Kernel2D.Animation
{
    /// <summary>
    /// Represents a single frame in an animation sequence.
    /// </summary>
    public class AnimationFrame
    {
        /// <summary>
        /// The name of the animation frame, which can be used
        /// for identification or debugging purposes.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The source <see cref="XRectangle"> within the texture
        /// that defines the area of the sprite to be drawn.
        /// </summary>
        public XRectangle SourceRectangle { get; set; }
        /// <summary>
        /// The duration for which this frame should be displayed,
        /// in milliseconds.
        /// </summary>
        public float Duration { get; set; }
    }
}
