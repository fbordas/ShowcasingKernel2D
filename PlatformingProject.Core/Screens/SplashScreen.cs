using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Drawing;
using MonoGame.Kernel2D.Screens;

namespace PlatformingProject.Core.Screens
{
    internal class SplashScreen : ScreenBase
    {
        private double _elapsedTime = 0;
        private const double _displayDuration = 3000; // Display for 3 seconds

        public override string ID => "SplashScreen";
        public override void Update(GameTime gameTime, ContentManager _content)
        {
            base.Update(gameTime);
            _elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTime >= _displayDuration)
            {
                ScreenManager.Instance.ChangeScreen("MainMenu", _content);
            }
            // Logic to transition to the next screen after a delay or user input
        }
        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            // Draw the splash screen content here
            context.Graphics.Clear(Color.LightGray);

            string splashText = "this is a splash screen";

            context.SpriteBatch.Begin(transformMatrix: context.TransformMatrix);
            context.SpriteBatch.DrawString(context.Font, splashText, new Vector2(200, 200), Color.Black,
                0, new Vector2(0,0), 10f, SpriteEffects.None, 0);
            context.SpriteBatch.End();
        }
    }
}