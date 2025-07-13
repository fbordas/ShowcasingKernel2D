using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaVector = Microsoft.Xna.Framework.Vector2;

namespace Kernel2D.Animation
{
    /// <summary>
    /// Represents a player for sprite animations.
    /// </summary>
    public class AnimationPlayer
    {
        private SpriteAnimation? _currentAnim;
        private int _currentAnimIndex;
        private float _elapsedTime;
        /// <summary>
        /// Indicates whether the animation being drawn is facing right. This is used to determine
        /// the <see cref="SpriteEffects"/> to apply.
        /// </summary>
        public bool FacingRight = true;
        /// <summary>
        /// The name of the current animation being played by this player.
        /// </summary>
        public string CurrentAnimationName = string.Empty;
        /// <summary>
        /// A callback invoked once a non-looping animation reaches its final frame.
        /// </summary>
        private Action? _currentAnimFinishedCallback = null;

        /// <summary>
        /// Gets a value indicating whether the current animation has finished playing.
        /// </summary>
        public bool HasFinishedPlaying =>
            _currentAnim != null &&
            !_currentAnim.Loop &&
            _currentAnimIndex >= _currentAnim.Frames.Count;

        /// <summary>
        /// Gets a value indicating whether the current animation is still playing.
        /// </summary>
        public bool IsPlaying => !HasFinishedPlaying;

        /// <summary>
        /// Draws the current animation frame using the specified sprite batch, texture,
        /// position, and sprite effects. Tint is assumed to be <see cref="Color.White"/>
        /// to render without any special coloring.
        /// </summary>
        /// <param name="batch">The <see cref="SpriteBatch"/> to render through.</param>
        /// <param name="tex">The <see cref="Texture2D"/> to display.</param>
        /// <param name="position">The <see cref="XnaVector"/> representing the coordinates
        /// to draw at.</param>
        /// <param name="fx">Any <see cref="SpriteEffects"/> to apply to the current
        /// render.</param>
        public void Draw(SpriteBatch batch, Texture2D tex, XnaVector position, SpriteEffects fx) =>
            Draw(batch, tex, position, fx, Color.White);

        /// <summary>
        /// Gets the current animation being played by this player.
        /// </summary>
        /// <returns>The current animation being played by the current instance.</returns>
        public SpriteAnimation? GetCurrentAnimation() => _currentAnim;

        /// <summary>
        /// Draws the current animation frame using the specified sprite batch, texture,
        /// position, sprite effects and tint.
        /// </summary>
        /// <param name="batch">The <see cref="SpriteBatch"/> to render through.</param>
        /// <param name="tex">The <see cref="Texture2D"/> to display.</param>
        /// <param name="position">The <see cref="XnaVector"/> representing the coordinates
        /// to draw at.</param>
        /// <param name="fx">Any <see cref="SpriteEffects"/> to apply to the current
        /// render.</param>
        /// <param name="tint">Any <see cref="Color"/> to apply as a tint to the current
        /// render. Defaults to <see cref="Color.White"/>.</param>
        /// <remarks>The origin point is set to the bottom center of the sprite frame
        /// to support consistent ground aligning during vertical actions and states like
        /// jumping and falling.</remarks>
        public void Draw(SpriteBatch batch, Texture2D tex, XnaVector position, SpriteEffects fx, Color tint)
        {
            if (_currentAnim == null || _currentAnimIndex >= _currentAnim.Frames.Count) { return; }
            var frame = _currentAnim.Frames[_currentAnimIndex];
            var origin = new XnaVector(frame.SourceRectangle.Width / 2f, frame.SourceRectangle.Height);
            batch.Draw(tex, position, frame.SourceRectangle, tint, 0f, origin, 2, fx, 0);
        }

        /// <summary>
        /// Draws the current animation frame using the specified sprite batch, texture,
        /// and position. No <see cref="SpriteEffects"/> are applied, and the tint
        /// is assumed to be <see cref="Color.White"/> to render without any special coloring.
        /// </summary>
        /// <param name="batch">The <see cref="SpriteBatch"/> to render through.</param>
        /// <param name="tex">The <see cref="Texture2D"/> to display.</param>
        /// <param name="position">The <see cref="XnaVector"/> representing the coordinates
        /// to draw at.</param>
        public void Draw(SpriteBatch batch, Texture2D tex, XnaVector position) =>
            Draw(batch, tex, position, SpriteEffects.None);

        /// <summary>
        /// Starts playback of the specified animation, resetting its progress and optionally
        /// invoking a callback when it completes.
        /// </summary>
        /// <param name="anim">The internal name of the animation to play.</param>
        /// <param name="onComplete">An action or function call to execute upon
        /// the animation finishing playing.</param>
        public void Play(SpriteAnimation anim, Action? onComplete = null)
        {
            if (_currentAnim != anim || _currentAnimIndex >= _currentAnim.Frames.Count)
            {
                _currentAnim = anim;
                _currentAnimIndex = 0;
                _elapsedTime = 0f;
            }
            CurrentAnimationName = anim.Name;
            _currentAnimFinishedCallback = onComplete;
        }

        /// <summary>
        /// Updates the current animation player based on the game time.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values used
        /// for game updates.</param>
        public void Update(GameTime gameTime)
        {
            if (_currentAnim == null) return;
            if (_currentAnimIndex >= _currentAnim.Frames.Count)
            { return; } // ✅ safety check before accessing any frame
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTime >= _currentAnim.Frames[_currentAnimIndex].Duration)
            {
                _elapsedTime = 0f;
                _currentAnimIndex++;
                if (_currentAnimIndex >= _currentAnim.Frames.Count)
                {
                    if (_currentAnim.Loop)
                        _currentAnimIndex = 0;
                    else
                    {
                        _currentAnimIndex = _currentAnim
                            .Frames.Count - 1; // ✅ cap at final valid frame
                        _currentAnimFinishedCallback?.Invoke();
                        _currentAnimFinishedCallback = null; // ✅ reset callback after use
                    }
                }
            }
        }
    }
}
