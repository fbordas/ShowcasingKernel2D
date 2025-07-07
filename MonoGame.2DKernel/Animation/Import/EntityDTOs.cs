namespace MonoGame.Kernel2D.Animation.Import
{
    /// <summary>
    /// Data Transfer Objects (DTOs) for importing spritesheets and animations.
    /// </summary>
    public class SpritesheetDTO
    {
        /// <summary>
        /// The name of the spritesheet, typically the filename without extension.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The collection of animations associated with this spritesheet.
        /// </summary>
        public List<SpriteAnimationDTO> Animations { get; set; } = [];
    }

    /// <summary>
    /// Represents a sprite animation, which consists of multiple frames.
    /// </summary>
    public class SpriteAnimationDTO
    {
        /// <summary>
        /// The name of the animation, typically used to identify it in code.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The collection of frames that make up the animation.
        /// </summary>
        public List<AnimationFrameDTO> Frames { get; set; } = [];
        /// <summary>
        /// Whether the animation should loop when it reaches the end.
        /// </summary>
        public bool Loop { get; set; }
    }

    /// <summary>
    /// Represents a single frame in an animation, including its name, rectangle area
    /// in the spritesheet, and duration.
    /// </summary>
    public class AnimationFrameDTO
    {
        /// <summary>
        /// The name of the frame, typically used to identify it in code.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The rectangle area in the spritesheet that this frame occupies.
        /// </summary>
        public Microsoft.Xna.Framework.Rectangle Frame { get; set; } = new();
        /// <summary>
        /// The duration of the frame in milliseconds. This determines how long the
        /// frame is displayed before moving to the next one.
        /// </summary>
        public float Duration { get; set; } = 0f;
    }
}