using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame.Kernel2D.Drawing
{
    public class DrawContext
    {
        public SpriteBatch SpriteBatch { get; }
        public Matrix TransformMatrix { get; }
        public GraphicsDevice Graphics { get; }
        public GameTime GameTime { get; }
        public SpriteFont Font { get; }

        // Optional:
        public RenderTarget2D? Target { get; set; }
        public Color GlobalTint { get; set; } = Color.White;
        public Color OverlayTint { get; set; } = Color.Black;

        public DrawContext(
            SpriteBatch spriteBatch,
            Matrix transform,
            GraphicsDevice graphics,
            GameTime gameTime,
            SpriteFont font)
        {
            SpriteBatch = spriteBatch;
            TransformMatrix = transform;
            Graphics = graphics;
            GameTime = gameTime;
            Font = font;
        }
    }
}