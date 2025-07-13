using Microsoft.Xna.Framework.Content;
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
        /// The <see cref="MenuInputBridge"/> to use in the current screen.
        /// </summary>
        public MenuInputBridge Input { get; protected set; } = new();

        /// <summary>
        /// Creates a new global game settings screen. This screen type should
        /// not be used for anything but global game settings, in order to
        /// keep a level of separation between in-game menus for in-game world
        /// data and execution data.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager"/> to load assets from.
        /// </param>
        public SettingsScreen(ContentManager content) => _content = content;
    }
}