using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Kernel2D.Input;

namespace MonoGame.Kernel2D.Screens
{
    public abstract class GameScreen : ScreenBase
    {
        public PlatformerInputBridge _input { get; protected set; } = new();


    }
}
