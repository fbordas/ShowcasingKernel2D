using Microsoft.Xna.Framework.Content;
using Kernel2D.Input.Bridges;
using Kernel2D.Input;

namespace Kernel2D.Screens
{
    /// <summary>
    /// A screen where gameplay (user interaction and in-game world response)
    /// is actively happening in a 2D game.
    /// </summary>
    public abstract class GameScreen : ScreenBase
    {
        /// <summary>
        /// The <see cref="PlatformerInputBridge"/> to use in the current screen.
        /// </summary>
        public PlatformerInputBridge Input { get; protected set; } = new();

        /// <summary>
        /// Creates a new playable screen.
        /// </summary>
        /// <param name="content">
        /// The <see cref="ContentManager"/> to load assets from.
        /// </param>
        /// <param name="input">
        /// The <see cref="PlatformerInputBridge"/> to use in the current screen.
        /// </param>
        public GameScreen(ContentManager content, PlatformerInputBridge input)
        { 
             _content = content;
            Input = (PlatformerInputBridge)input;
        }
    }
}