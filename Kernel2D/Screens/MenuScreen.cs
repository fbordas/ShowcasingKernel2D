using Microsoft.Xna.Framework.Content;
using Kernel2D.Input;

namespace Kernel2D.Screens
{
    /// <summary>
    /// A screen where menu options are shown for the user to interact with.
    /// </summary>
    public abstract class MenuScreen : ScreenBase
    {
        /// <summary>
        /// The <see cref="MenuInputBridge"/> to use in the current screen.
        /// </summary>
        public MenuInputBridge Input { get; protected set; } = new();

        /// <summary>
        /// Creates a new user-interactable menu screen.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager"/> to load assets from.
        /// </param>
        public MenuScreen(ContentManager content) => _content = content;
    }
}