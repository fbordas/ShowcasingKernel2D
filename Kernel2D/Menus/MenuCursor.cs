using Kernel2D.Animation;
using Kernel2D.Drawing;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Menus
{
    /// <summary>
    /// Represents a cursor for navigating through menu options in a 2D game.
    /// </summary>
    public class MenuCursor
    {
        /// <summary>
        /// The texture used for the cursor.
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        /// Optional animation for the cursor.
        /// </summary>
        public SpriteAnimation? Animation { get; set; } = null;

        /// <summary>
        /// The offset from the option position where the cursor will be drawn.
        /// </summary>
        public Vector2 Offset { get; set; } = new(-32, 0); // Default: left of text

        /// <summary>
        /// The layer depth at which the cursor will be drawn.
        /// </summary>
        public float LayerDepth { get; set; } = 0f;

        /// <summary>
        /// The current position of the cursor in the menu.
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuCursor"/> class with
        /// the specified texture.
        /// </summary>
        /// <param name="texture">
        /// The texture to use for the cursor.
        /// </param>
        public MenuCursor(Texture2D texture) => Texture = texture;

        /// <summary>
        /// Updates the cursor's position based on the provided option position.
        /// </summary>
        /// <param name="optionPosition">
        /// The position of the menu option that the cursor should point to.
        /// </param>
        public void Update(Vector2 optionPosition) =>
            Position = optionPosition + Offset;

        /// <summary>
        /// Draws the cursor at its current position using the provided
        /// <see cref="DrawContext"/>.
        /// </summary>
        /// <param name="context">
        /// The drawing context to use for rendering the cursor.
        /// </param>
        public void Draw(DrawContext context)
        {
            context.DrawingQueue.Enqueue(new SpriteDrawCommand
                (Texture, Position, null, Color.White, 0f, Vector2.Zero, 
                Vector2.One, SpriteEffects.None, LayerDepth));
        }
    }
}