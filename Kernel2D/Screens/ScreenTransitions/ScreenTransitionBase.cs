using Kernel2D.Drawing;
using Microsoft.Xna.Framework;

namespace Kernel2D.Screens.ScreenTransitions
{
    /// <summary>
    /// Base class for screen transitions in a 2D game.
    /// </summary>
    public abstract class ScreenTransitionBase
    {
        /// <summary>
        /// The elapsed time since the transition started, in seconds.
        /// </summary>
        protected float Elapsed { get; private set; }

        /// <summary>
        /// The total duration of the transition, in seconds.
        /// </summary>
        protected float Duration { get; }
        
        /// <summary>
        /// Indicates whether the transition has finished.
        /// </summary>
        public bool IsFinished => Elapsed >= Duration;
        
        /// <summary>
        /// Indicates whether the transition is currently active (not finished).
        /// </summary>
        public bool IsActive => !IsFinished;
        
        /// <summary>
        /// Base constructor for a screen transition instance.
        /// </summary>
        /// <param name="duration">
        /// The duration of the transition in seconds. This defines how long the
        /// transition will take to complete from start to finish.
        /// </param>
        protected ScreenTransitionBase(float duration) => Duration = duration;
        
        /// <summary>
        /// Updates the transition's elapsed time based on the game time.
        /// </summary>
        /// <param name="gameTime">
        /// The <see cref="GameTime"/> that provides timing information for the game,
        /// including elapsed time since the last update and total game time.
        /// </param>
        public virtual void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                Elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Elapsed > Duration) Elapsed = Duration; // clamp to duration
            }
        }
        
        /// <summary>
        /// Draws the transition effect using the provided <see cref="DrawContext"/>.
        /// </summary>
        /// <param name="context">
        /// The <see cref="DrawContext"/> that contains the drawing parameters
        /// such as the sprite batch, transform matrix, graphics device, and
        /// game time.
        /// </param>
        public abstract void Draw(DrawContext context);

        /// <summary>
        /// Resets the transition's progress so it can be reused.
        /// </summary>
        public virtual void Reset() => Elapsed = 0f;
    }
}