using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Kernel2D.Drawing
{
    /// <summary>
    /// Represents a command to draw a sprite using a texture, position, source rectangle,
    /// tint color, rotation, origin, scale, sprite effects, and layer depth.
    /// </summary>
    /// <param name="Texture">
    /// The <see cref="Texture2D"/> to draw as a sprite.
    /// </param>
    /// <param name="Position">
    /// The position in world space where the sprite will be drawn represented by a
    /// <see cref="Vector2"/>.
    /// </param>
    /// <param name="SourceRectangle">
    /// An optional <see cref="Rectangle"/> that defines the source rectangle of the texture
    /// to be drawn. If null, the entire texture will be used.
    /// </param>
    /// <param name="Color">
    /// The <see cref="Color"/> tint to apply to the sprite. This can be used to change the
    /// color of the sprite when drawn.
    /// </param>
    /// <param name="Rotation">
    /// The rotation angle in radians to apply to the sprite when drawn. A value of 0 means
    /// no rotation.
    /// </param>
    /// <param name="Origin">
    /// The origin point of the sprite, represented by a <see cref="Vector2"/>. This is the
    /// point around which the sprite will be rotated and scaled. It is typically set to
    /// the center of the sprite for proper rotation and scaling effects.
    /// </param>
    /// <param name="Scale">
    /// The scale factor for the sprite, represented by a <see cref="Vector2"/>. This
    /// determines how much the sprite will be scaled in the X and Y directions.
    /// </param>
    /// <param name="Effects">
    /// The <see cref="SpriteEffects"/> to apply to the sprite. This can be used to flip
    /// the sprite horizontally or vertically.
    /// </param>
    /// <param name="LayerDepth">
    /// The depth at which the sprite will be drawn in the layer order. A value of 0 is
    /// the front layer, and a value of 1 is the back layer. This allows for proper
    /// layering of sprites when drawn together.
    /// </param>
    public record SpriteDrawCommand(Texture2D Texture, Vector2 Position,
        Rectangle? SourceRectangle, Color Color, float Rotation, Vector2 Origin,
        Vector2 Scale, SpriteEffects Effects, float LayerDepth) : IDrawCommand
    {
        /// <summary>
        /// Gets the layer depth at which the sprite will be drawn.
        /// </summary>
        public float Layer => LayerDepth;
    }
}
