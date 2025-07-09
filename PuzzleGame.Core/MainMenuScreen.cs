using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Drawing;
using K2D = MonoGame.Kernel2D;
using XnaVector = Microsoft.Xna.Framework.Vector2;

namespace PuzzleGameProject.Core
{
    internal class MainMenuScreen : K2D.Screens.GameScreen
    {
        public MainMenuScreen(ContentManager content) : base(content) => _content = content;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            //_font = content.Load<SpriteFont>("Fonts/Hud");
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            //if (_font != null)
            //{
            //    batch.Begin();
            //    batch.DrawString(_font, "Main Menu", new XnaVector(100, 100), Color.Black, 0,
            //        new XnaVector(0,0), 3f, SpriteEffects.None, 0);
            //    batch.End();
            //}
        }
    }
}
