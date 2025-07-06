using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame.Kernel2D.Screens
{
    /// <summary>
    /// Base class for a screen in a 2D game.
    /// </summary>
    public abstract class ScreenBase
    {
        /// <summary>
        /// Gets the unique identifier for the screen, which is typically
        /// the name of the derived class.
        /// </summary>
        public virtual string ID => GetType().Name;

        /// <summary>
        /// Loads the content for the screen using the provided
        /// <see cref="ContentManager"/>.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager"/> to use for loading content.
        /// </param>
        public virtual void LoadContent(ContentManager content) { }

        /// <summary>
        /// Unloads the content for the screen.
        /// </summary>
        public virtual void UnloadContent() { }

        /// <summary>
        /// Updates the screen with the given <see cref="GameTime"/>.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values used for game updates.
        /// </param>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Draws the screen using the provided <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="batch">
        /// The <see cref="SpriteBatch"/> to use for drawing the screen's
        /// content.
        /// </param>
        public virtual void Draw(SpriteBatch batch) { }
    }
}
