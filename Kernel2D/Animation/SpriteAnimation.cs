namespace Kernel2D.Animation
{
    /// <summary>
    /// Represents a sprite animation, which consists of multiple frames.
    /// </summary>
    public class SpriteAnimation
    {
        /// <summary>
        /// The name of the animation, which can be used for identification
        /// or debugging purposes.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// A collection of <see cref="AnimationFrame"/>s that make up the
        /// animation.
        /// </summary>
        public List<AnimationFrame> Frames = [];
        /// <summary>
        /// Whether the animation should loop when it reaches the end.
        /// </summary>
        public bool Loop { get; set; }
        /// <summary>
        /// A set of tags associated with the animation, which can be used
        /// to determine its qualities and properties in a finite state
        /// machine.
        /// </summary>
        public HashSet<string> Tags { get; set; } = [];
    }
}
