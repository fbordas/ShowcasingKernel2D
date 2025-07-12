using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Drawing;
using MonoGame.Kernel2D.Screens;
using MonoGame.Kernel2D.Screens.ScreenTransitions;
using Debugger = MonoGame.Kernel2D.Helpers.DebugHelpers;

namespace PlatformingProject.Core.Screens
{
    internal class SplashScreen : ScreenBase
    {
        private double _elapsedTime = 0;
        private const double _displayDuration = 3000; // Display for 3 seconds
        private SpriteFont _font;

        public SplashScreen(SpriteFont font) => _font = font;

        public override string ID => "SplashScreen";
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Debugger.WriteLine($"SplashScreen.Update() | {gameTime.TotalGameTime.TotalMilliseconds}");
            _elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTime >= _displayDuration)
            {
                var transIn = new FadeTransition(2f, true, Color.White);
                var transOut = new FadeTransition(2f, false, Color.White);
                ScreenManager.Instance.ChangeScreen("TitleScreen", _content, new ScreenTransitionPair(transIn, transOut));
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
            var location = context.CenterTextOnscreen(_font, splashText, textScaling);

            context.DrawingQueue.Enqueue(new TextDrawCommand(_font, splashText, location,
                Color.Black, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0));
        }
    }
}