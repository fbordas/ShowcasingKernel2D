using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Screens;

namespace PlatformingProject.Core.Screens
{
    internal class PlatformerGameTestScreen : GameScreen
    {
        public PlatformerGameTestScreen(ContentManager content) : base(content) => _content = content;

        public override string ID => "GameplayScreen";
    }
}