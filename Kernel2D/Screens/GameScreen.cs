using Microsoft.Xna.Framework.Content;
using Kernel2D.Drawing;
using Kernel2D.Input;

namespace Kernel2D.Screens
{
    public abstract class GameScreen : ScreenBase
    {
        public PlatformerInputBridge Input { get; protected set; } = new();

        public GameScreen(ContentManager content) => _content = content;
    }
}