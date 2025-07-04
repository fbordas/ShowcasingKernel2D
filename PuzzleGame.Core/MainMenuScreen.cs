using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using K2D = MonoGame.Kernel2D;
using XnaVector = Microsoft.Xna.Framework.Vector2;

namespace PuzzleGameProject.Core
{
    internal class MainMenuScreen : K2D.Screens.GameScreen
    {
        private SpriteFont _font;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _font = content.Load<SpriteFont>("Fonts/Hud");
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            if (_font != null)
            {
                batch.Begin();
                batch.DrawString(_font, "Main Menu", new XnaVector(100, 100), Color.Black, 0,
                    new XnaVector(0,0), 3f, SpriteEffects.None, 0);
                batch.End();
            }
        }
    }
}
