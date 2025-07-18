using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Kernel2D.Drawing;
using Debugger = Kernel2D.Helpers.DebugHelpers;

namespace Kernel2D.Screens
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
        /// Event that is raised when the screen requests to exit the game.
        /// </summary>
        public event Action? ExitRequested = null;

        /// <summary>
        /// Invokes the <see cref="ExitRequested"/> event to signal that the
        /// screen wants to exit the game.
        /// </summary>
        protected void OnExitRequested() => ExitRequested?.Invoke();

        /// <summary>
        /// The content manager used to load and manage game assets.
        /// </summary>
        protected ContentManager? _content = null;

        /// <summary>
        /// Gets the currently loaded <see cref="ContentManager"/>.
        /// </summary>
        /// <returns>The currently loaded <see cref="ContentManager"/>.</returns>
        public ContentManager? GetCurrentContent() => _content;

        /// <summary>
        /// Loads the content for the screen using the provided
        /// <see cref="ContentManager"/>.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager"/> to use for loading content.
        /// </param>
        public virtual void LoadContent(ContentManager content) => _content = content;

        /// <summary>
        /// Unloads the content for the screen.
        /// </summary>
        public virtual void UnloadContent() => _content = null;

        /// <summary>
        /// Updates the screen with the given <see cref="GameTime"/>.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values used for game updates.
        /// </param>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Draws the screen using the provided <see cref="DrawContext"/>.
        /// </summary>
        /// <param name="context">
        /// The <see cref="DrawContext"/> that contains the drawing parameters
        /// such as the <see cref="DrawQueue"/>, transform matrix, graphics device,
        /// and game time.
        /// </param>
        public virtual void Draw(DrawContext context) => 
            Debugger.WriteLine($"Screen drawing: {this.ID}");
    }
}