using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Animation
{
    /// <summary>
    /// Represents a spritesheet containing a texture and a
    /// collection of animations.
    /// </summary>
    public class Spritesheet
    {
        /// <summary>
        /// The <see cref="Texture2D"/> associated with the spritesheet,
        /// which contains the sprite images.
        /// </summary>
        public Texture2D? Texture { get; set; } = null;
        /// <summary>
        /// The name of the spritesheet, which can be used for
        /// identification or debugging purposes.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// A collection of <see cref="SpriteAnimation"/>s defined within
        /// the spritesheet, where each animation is identified by a unique
        /// name.
        /// </summary>
        public Dictionary<string, SpriteAnimation> Animations = [];
    }
}
