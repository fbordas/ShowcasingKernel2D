namespace Kernel2D.Animation.Import
{
    /// <summary>
    /// Represents a spritesheet, which is a collection of animations
    /// that can be used in a game. Each spritesheet contains a texture
    /// representing the image of the spritesheet and a collection of
    /// animations that define how the sprites are animated.
    /// </summary>
    internal class SpritesheetDTO
    {
        /// <summary>
        /// The name of the spritesheet, typically the filename without extension.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The path to the texture file that contains the spritesheet image
        /// inside the MGCB content file.
        /// </summary>
        public string Texture { get; set; } = string.Empty;
        /// <summary>
        /// The collection of animations associated with this spritesheet.
        /// </summary>
        public List<SpriteAnimationDTO> Animations { get; set; } = [];
    }

    /// <summary>
    /// Represents a sprite animation, which consists of multiple frames.
    /// </summary>
    internal class SpriteAnimationDTO
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
    internal class AnimationFrameDTO
    {
        /// <summary>
        /// The name of the frame, typically used to identify it in code.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The rectangle area in the spritesheet that this frame occupies.
        /// </summary>
        public FrameDTO? Frame { get; set; }
        /// <summary>
        /// The duration of the frame in milliseconds. This determines how long the
        /// frame is displayed before moving to the next one.
        /// </summary>
        public float Duration { get; set; } = 0f;
    }

    /// <summary>
    /// Represents the rectangle containing a single sprite frame, measured by its
    /// geographical coordinates in the source image, width and height.
    /// </summary>
    internal class FrameDTO
    {
        /// <summary>
        /// X coordinate of the sprite frame location.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y coordinate of the sprite frame location.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Width of the sprite frame.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of the sprite frame.
        /// </summary>
        public int Height { get; set; }
    }

}