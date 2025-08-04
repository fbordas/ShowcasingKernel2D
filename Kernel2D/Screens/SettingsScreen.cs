using Microsoft.Xna.Framework.Content;
using Kernel2D.Input.Bridges.Menu;
using Kernel2D.Input;

namespace Kernel2D.Screens
{
    /// <summary>
    /// A screen where global game-related settings are shown to the user to
    /// view and modify.
    /// </summary>
    public abstract class SettingsScreen : ScreenBase
    {
        /// <summary>
        /// The <see cref="IInputBridge"/> to use in the current screen.
        /// </summary>
        public IInputBridge Input { get; protected set; }

        /// <summary>
        /// Creates a new global game settings screen. This screen type should
        /// not be used for anything but global game settings, in order to
        /// keep a level of separation between in-game menus for in-game world
        /// data and execution data.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager"/> to load assets from.
        /// </param>
        /// <param name="input">
        /// The <see cref="IInputBridge"/> to use for processing user input
        /// in the settings screen.
        /// </param>
        public SettingsScreen(ContentManager content, IInputBridge input) 
        {
            _content = content;
            Input = input;
        }
    }
}