using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame.Kernel2D.Drawing
{
    /// <summary>
    /// Represents a queue for drawing commands in a 2D game.
    /// </summary>
    public class DrawQueue
    {
        /// <summary>
        /// A list of draw commands that are queued for execution.
        /// </summary>
        private readonly List<IDrawCommand> _queue = [];

        /// <summary>
        /// Enqueues a draw command to be executed later.
        /// </summary>
        /// <param name="call">
        /// The draw command to be added to the queue.
        /// </param>
        public void Enqueue(IDrawCommand call) => _queue.Add(call);

        /// <summary>
        /// Flushes the draw queue by executing all queued drawing commands in order of
        /// their layer depth, from the highest layer (back) to the lowest layer (front).
        /// </summary>
        /// <param name="sb">
        /// The <see cref="SpriteBatch"/> used to execute the draw commands.
        /// </param>
        public void Flush(SpriteBatch sb)
        {
            foreach (var call in _queue.OrderByDescending(c => c.Layer))
            { call.Execute(sb); }
            _queue.Clear();
        }

        /// <summary>
        /// Clears the draw queue, removing all queued drawing commands without executing them.
        /// </summary>
        public void ClearQueue() => _queue.Clear();
    }

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
        /// <summary>
        /// Executes the draw command using the provided <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="batch">
        /// The <see cref="SpriteBatch"/> used to draw the sprite.
        /// </param>
        public void Execute(SpriteBatch batch)
        {
            batch.Draw(Texture, Position, SourceRectangle, Color, Rotation, Origin,
                Scale, Effects, LayerDepth);
        }
    }

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
        /// <summary>
        /// Executes the draw command using the provided <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="batch">
        /// The <see cref="SpriteBatch"/> used to draw the text.
        /// </param>
        public void Execute(SpriteBatch batch)
        {
            batch.DrawString(SpriteFont, Text, Position, Color, Rotation, Origin,
                Scale, Effects, LayerDepth);
        }
    }
}
