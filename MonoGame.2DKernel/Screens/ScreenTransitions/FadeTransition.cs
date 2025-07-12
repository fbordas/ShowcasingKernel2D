using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Drawing;

namespace MonoGame.Kernel2D.Screens.ScreenTransitions
{
    /// <summary>
    /// Represents a fade transition effect between screens in a 2D game.
    /// </summary>
    public class FadeTransition : ScreenTransitionBase
    {
        private readonly bool _fadeIn;
        private readonly Color _fadeColor;

        /// <summary>
        /// Creates a new fade transition.
        /// </summary>
        /// <param name="duration">
        /// The duration of the transition in seconds.
        /// </param>
        /// <param name="fadeIn">
        /// If true, the transition will fade in; if false, it will fade out.
        /// </param>
        /// <param name="fadeColor">
        /// The color to use for the fade effect. This will be multiplied by the
        /// alpha value based on the transition progress.
        /// </param>
        public FadeTransition(float duration, bool fadeIn, Color fadeColor)
            : base(duration)
        {
            _fadeIn = fadeIn;
            _fadeColor = fadeColor;
        }

        /// <summary>
        /// Updates the transition's elapsed time based on the game time.
        /// </summary>
        /// <param name="context">
        /// The <see cref="DrawContext"/> containing the game time and other
        /// drawing parameters.
        /// </param>
        public override void Draw(DrawContext context)
        {
            float progress = Math.Clamp(Elapsed / Duration, 0, 1);
            float alpha = _fadeIn ? 1f - progress : progress;
            Color tint = _fadeColor * alpha;

            var viewport = context.Graphics.Viewport;
            var bounds = new Rectangle(0, 0, viewport.Width, viewport.Height);

            context.DrawingQueue.Enqueue(new SpriteDrawCommand(
                Texture: context.WhitePixel, // 1x1 white texture
                Position: new Vector2(bounds.X, bounds.Y),
                SourceRectangle: bounds,
                Color: tint,
                Rotation: 0f,
                Origin: Vector2.Zero,
                Scale: Vector2.One,
                Effects: SpriteEffects.None,
                LayerDepth: 0f
            ));
        }
    }
}