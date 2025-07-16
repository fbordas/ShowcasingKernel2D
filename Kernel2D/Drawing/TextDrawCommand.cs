using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Kernel2D.Drawing
{
    /// <summary>
    /// Represents a command to draw text using a sprite font, text string, position,
    /// tint color, rotation, origin, scale, sprite effects, and layer depth.
    /// </summary>
    /// <param name="SpriteFont">
    /// The <see cref="SpriteFont"/> used to draw the text. This defines the font
    /// style and size.
    /// </param>
    /// <param name="Text">
    /// The text string to be drawn. This is the actual content that will be rendered
    /// on the screen.
    /// </param>
    /// <param name="Position">
    /// The position in world space where the text will be drawn, represented by a
    /// <see cref="Vector2"/>.
    /// </param>
    /// <param name="Color">
    /// The <see cref="Microsoft.Xna.Framework.Color"/> tint to apply to the text.
    /// This can be used to change the color of the text when drawn.
    /// </param>
    /// <param name="Rotation">
    /// The rotation angle in radians to apply to the text when drawn. A value of 0
    /// means no rotation.
    /// </param>
    /// <param name="Origin">
    /// The origin point of the text, represented by a <see cref="Vector2"/>.
    /// </param>
    /// <param name="Scale">
    /// The scale factor for the text, represented by a <see cref="float"/>.
    /// </param>
    /// <param name="Effects">
    /// The <see cref="SpriteEffects"/> to apply to the text. This can be used
    /// to flip the text horizontally or vertically.
    /// </param>
    /// <param name="LayerDepth">
    /// The depth at which the text will be drawn in the layer order.
    /// </param>
    public record TextDrawCommand(SpriteFont SpriteFont, string Text, Vector2 Position,
            Color Color, float Rotation, Vector2 Origin, float Scale, SpriteEffects Effects,
            float LayerDepth) : IDrawCommand
    {
        /// <summary>
        /// Gets the layer depth at which the text will be drawn.
        /// </summary>
        public float Layer => LayerDepth;
    }
}
