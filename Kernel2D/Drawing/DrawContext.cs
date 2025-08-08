using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Kernel2D.Helpers;

namespace Kernel2D.Drawing
{
    /// <summary>
    /// Represents the context for drawing operations in a 2D game.
    /// </summary>
    public class DrawContext
    {
        /// <summary>
        /// The <see cref="DrawQueue"/> that holds the queued drawing commands.
        /// </summary>
        public DrawQueue DrawingQueue { get; }
        /// <summary>
        /// The transformation matrix applied to the drawing context, typically
        /// used for scaling, rotating, or translating the drawn elements in the
        /// game world.
        /// </summary>
        public Matrix TransformMatrix { get; private set; }
        /// <summary>
        /// A white pixel used for screen transition overlays or other effects that
        /// require a solid color texture.
        /// </summary>
        public Texture2D WhitePixel { get; }
        /// <summary>
        /// The <see cref="GraphicsDevice"/> used for rendering graphics in the game.
        /// </summary>
        public GraphicsDevice Graphics { get; }
        /// <summary>
        /// The <see cref="GameTime"/> that provides timing information for the game,
        /// including elapsed time since the last update and total game time.
        /// </summary>
        public GameTime GameTime { get; }

        // Optional:
        /// <summary>
        /// The render target for drawing operations, allowing off-screen rendering.
        /// </summary>
        public RenderTarget2D? Target { get; set; }
        /// <summary>
        /// The global tint color applied to all drawn elements, allowing for a
        /// consistent color overlay across the game.
        /// </summary>
        public Color GlobalTint { get; set; } = Color.White;
        /// <summary>
        /// The tint color applied to the overlay, which can be used for effects like
        /// darkening or highlighting the screen.
        /// </summary>
        public Color OverlayTint { get; set; } = Color.Black;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawContext"/> class with the
        /// specified parameters.
        /// </summary>
        /// <param name="drawQueue">
        /// The <see cref="DrawQueue"/> that holds the drawing commands to be executed.
        /// </param>
        /// <param name="transform">
        /// The transformation matrix applied to the drawing context, which can be used
        /// for scaling, rotating, or translating drawn elements in the game world.
        /// </param>
        /// <param name="graphics">
        /// The <see cref="GraphicsDevice"/> used for rendering graphics in the game.
        /// </param>
        /// <param name="gameTime">
        /// The <see cref="GameTime"/> that provides timing information for the game,
        /// including elapsed time since the last update and total game time.
        /// </param>
        public DrawContext(DrawQueue drawQueue, Matrix transform, GraphicsDevice graphics,
            GameTime gameTime)
        {
            DrawingQueue = drawQueue;
            TransformMatrix = transform;
            Graphics = graphics;
            GameTime = gameTime;
            WhitePixel = TextureHelpers.WhitePixel(graphics);
        }

        /// <summary>
        /// Centers text horizontally on the screen at a specified vertical position.
        /// </summary>
        /// <param name="font">
        /// The <see cref="SpriteFont"/> used to measure the text width.
        /// </param>
        /// <param name="text">
        /// The text string to be centered.
        /// </param>
        /// <param name="scale">
        /// The scale factor to apply to the text size when measuring its width.
        /// </param>
        /// <param name="y">
        /// The vertical position on the screen where the text should be centered.
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/> representing the position where the text should be drawn
        /// to center it horizontally on the screen at the specified vertical position.
        /// </returns>
        public Vector2 CenterTextHorizontally(SpriteFont font, string text, float scale, float y)
        {
            float textWidth = font.MeasureString(text).X * scale;
            float screenWidth = Graphics.Viewport.Width;
            return new Vector2((screenWidth - textWidth) / 2f, y);
        }

        /// <summary>
        /// Centers text vertically on the screen at a specified horizontal position.
        /// </summary>
        /// <param name="font">
        /// The <see cref="SpriteFont"/> used to measure the text height.
        /// </param>
        /// <param name="text">
        /// The text string to be centered.
        /// </param>
        /// <param name="scale">
        /// The scale factor to apply to the text size when measuring its height.
        /// </param>
        /// <param name="x">
        /// The horizontal position on the screen where the text should be centered.
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/> representing the position where the text should be drawn
        /// to center it vertically on the screen at the specified horizontal position.
        /// </returns>
        public Vector2 CenterTextVertically(SpriteFont font, string text, float scale, float x)
        {
            float textHeight = font.MeasureString(text).Y * scale;
            float screenHeight = Graphics.Viewport.Height;
            return new Vector2(x, (screenHeight - textHeight) / 2f);
        }

        /// <summary>
        /// Centers text on the screen based on the provided font, text, and scale.
        /// </summary>
        /// <param name="font">
        /// The <see cref="SpriteFont"/> used to measure the text size.
        /// </param>
        /// <param name="text">
        /// The text string to be centered on the screen.
        /// </param>
        /// <param name="scale">
        /// The scale factor to apply to the text size when measuring its dimensions.
        /// </param>
        /// <returns>
        /// A <see cref="Vector2"/> representing the position where the text should be drawn
        /// to center it on the screen.
        /// </returns>
        public Vector2 CenterTextOnscreen(SpriteFont font, string text, float scale)
        {
            Vector2 size = font.MeasureString(text) * scale;
            Vector2 screen = new(Graphics.Viewport.Width, Graphics.Viewport.Height);
            return (screen - size) / 2f;
        }

        /// <summary>
        /// Centers an image horizontally on the screen at a specified vertical position.
        /// </summary>
        /// <param name="texture">The <see cref="Texture2D"/> to center.</param>
        /// <param name="scale">The scale factor to apply to the image size
        /// when measuring its dimensions.</param>
        /// <param name="y">The vertical position on the screen where the image
        /// should be centered.</param>
        /// <returns>A <see cref="Vector2"/> representing the position where the image
        /// should be drawn to center it horizontally on the screen at the specified
        /// vertical position.</returns>
        public Vector2 CenterImageHorizontally(Texture2D texture, float scale, float y)
        {
            float textureWidth = texture.Width * scale;
            float screenWidth = Graphics.Viewport.Width;
            return new Vector2((screenWidth - textureWidth) / 2f, y);
        }

        /// <summary>
        /// Centers an image vertically on the screen at a specified horizontal position.
        /// </summary>
        /// <param name="texture">The <see cref="Texture2D"/> to center.</param>
        /// <param name="scale">The scale factor to apply to the image size
        /// when measuring its dimensions.</param>
        /// <param name="x">The horizontal position on the screen where the image
        /// should be centered.</param>
        /// <returns>A <see cref="Vector2"/> representing the position where the image
        /// should be drawn to center it vertically on the screen at the specified
        /// horizontal position.</returns>
        public Vector2 CenterImageVertically(Texture2D texture, float scale, float x)
        {
            float textureHeight = texture.Height * scale;
            float screenHeight = Graphics.Viewport.Height;
            return new Vector2(x, (screenHeight - textureHeight) / 2f);
        }

        /// <summary>
        /// Centers an image on the screen.
        /// </summary>
        /// <param name="texture">The <see cref="Texture2D"/> to center.</param>
        /// <param name="scale">The scale factor to apply to the image size when
        /// measuring its dimensions.</param>
        /// <returns>A <see cref="Vector2"/> representing the position where the
        /// image should be drawn to center it on the screen.</returns>
        public Vector2 CenterImageOnScreen(Texture2D texture, float scale)
        { 
            Vector2 size = new(texture.Width * scale, texture.Height * scale);
            Vector2 screen = new(Graphics.Viewport.Width, Graphics.Viewport.Height);
            return (screen - size) / 2f;
        }

        /// <summary>
        /// Changes the transformation matrix used for drawing operations.
        /// </summary>
        /// <param name="matrix">
        /// The new transformation <see cref="Matrix"/> to apply.
        /// </param>
        public void SetTransformMatrix(Matrix matrix) => TransformMatrix = matrix;
    }
}