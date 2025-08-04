using Microsoft.Xna.Framework.Content;
using Kernel2D.Input.Bridges.Menu;
using Kernel2D.Input;

namespace Kernel2D.Screens
{
    /// <summary>
    /// A screen where menu options are shown for the user to interact with.
    /// </summary>
    public abstract class MenuScreen : ScreenBase
    {
        /// <summary>
        /// The <see cref="IInputBridge"/> to use in the current screen.
        /// </summary>
        public IInputBridge Input { get; }

        /// <summary>
        /// Creates a new user-interactable menu screen.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager"/> to load assets from.
        /// </param>
        /// <param name="input">
        /// The <see cref="IInputBridge"/> to use for processing user input
        /// in the menu screen.
        /// </param>
        public MenuScreen(ContentManager content, IInputBridge input)
        {
            _content = content;
            Input = input;
        }
    }
}