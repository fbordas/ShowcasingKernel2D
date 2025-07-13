using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Drawing
{
    /// <summary>
    /// Represents a command to draw a sprite or text in a 2D game.
    /// </summary>
    public interface IDrawCommand
    {
        /// <summary>
        /// Gets the layer depth at which the sprite or text will be drawn.
        /// </summary>
        float Layer { get; }
        /// <summary>
        /// Executes the draw command using the provided <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch">
        /// The <see cref="SpriteBatch"/> used to execute the draw command.
        /// </param>
        void Execute(SpriteBatch spriteBatch);
    }
}
