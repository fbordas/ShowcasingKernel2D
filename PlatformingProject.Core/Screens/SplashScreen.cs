using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Drawing;
using MonoGame.Kernel2D.Screens;
using Debugger = MonoGame.Kernel2D.Helpers.DebugHelpers;

namespace PlatformingProject.Core.Screens
{
    internal class SplashScreen : ScreenBase
    {
        private double _elapsedTime = 0;
        private const double _displayDuration = 3000; // Display for 3 seconds

        public override string ID => "SplashScreen";
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Debugger.WriteLine($"SplashScreen.Update() | {gameTime.TotalGameTime.TotalMilliseconds}");
            _elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTime >= _displayDuration)
            {
                ScreenManager.Instance.ChangeScreen("TitleScreen", _content);
            }
            // Logic to transition to the next screen after a delay or user input
        }
        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            // Draw the splash screen content here
            context.Graphics.Clear(Color.LightGray);
            string splashText = "SPLASH SCREEN WOOOOOOOO";
            float textScaling = 4.5f;
            var location = context.CenterScreen(splashText, textScaling);

            context.SpriteBatch.DrawString(context.Font, splashText, location, Color.Black,
                0, new Vector2(0,0), textScaling, SpriteEffects.None, 0);
        }
    }
}