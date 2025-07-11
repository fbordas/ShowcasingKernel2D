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

        public Vector2 CenterHorizontally(string text, float scale, float y)
        {
            float textWidth = Font.MeasureString(text).X * scale;
            float screenWidth = Graphics.Viewport.Width;
            return new Vector2((screenWidth - textWidth) / 2f, y);
        }

        public Vector2 CenterVertically(string text, float scale, float x)
        {
            float textHeight = Font.MeasureString(text).Y * scale;
            float screenHeight = Graphics.Viewport.Height;
            return new Vector2(x, (screenHeight - textHeight) / 2f);
        }

        public Vector2 CenterScreen(string text, float scale)
        {
            Vector2 size = Font.MeasureString(text) * scale;
            Vector2 screen = new(Graphics.Viewport.Width, Graphics.Viewport.Height);
            return (screen - size) / 2f;
        }

    }
}