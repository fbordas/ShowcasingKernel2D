using Microsoft.Xna.Framework.Content;
using MonoGame.Kernel2D.Drawing;
using MonoGame.Kernel2D.Input;

namespace MonoGame.Kernel2D.Screens
{
    public abstract class GameScreen : ScreenBase
    {
        public PlatformerInputBridge Input { get; protected set; } = new();

        public GameScreen(ContentManager content) => _content = content;
    }
}