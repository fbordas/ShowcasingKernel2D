using Microsoft.Xna.Framework.Content;
using Kernel2D.Input;

namespace Kernel2D.Screens
{
    public abstract class MenuScreen : ScreenBase
    {
        public MenuInputBridge Input { get; protected set; } = new();

        public MenuScreen(ContentManager content) => _content = content;
    }
}