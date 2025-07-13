using Microsoft.Xna.Framework.Content;
using Kernel2D.Input;

namespace Kernel2D.Screens
{
    public abstract class SettingsScreen : ScreenBase
    {
        public MenuInputBridge Input { get; protected set; } = new();

        public SettingsScreen(ContentManager content) => _content = content;
    }
}