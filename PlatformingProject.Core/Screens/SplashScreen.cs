using Kernel2D.Drawing;
using Kernel2D.Screens;
using Kernel2D.Screens.ScreenTransitions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Debugger = Kernel2D.Helpers.DebugHelpers;

namespace PlatformingProject.Core.Screens
{
    internal class SplashScreen : ScreenBase
    {
        private double _elapsedTime = 0;
        private const double _displayDuration = 2500; // Display for 2.5 seconds
        private readonly SpriteFont _font;

        public SplashScreen(SpriteFont font) => _font = font;

        public override string ID => "SplashScreen";
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Debugger.WriteLine($"SplashScreen.Update() | {gameTime.TotalGameTime.TotalMilliseconds}");
            _elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTime >= _displayDuration)
            {
                var transOut = new FadeTransition(0.5f, true, Color.Black);
                var transIn = new FadeTransition(0.5f, false, Color.Black);
                ScreenManager.Instance.ChangeScreen("TitleScreen", _content,
                    new ScreenTransitionPair(transIn, transOut));
            }
            // Logic to transition to the next screen after a delay or user input
        }
        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Graphics.Clear(Color.White);
            // Draw the splash screen content here
            var pan = _content.Load<Texture2D>("GlobalAssets/TitleScreen/bread");
            var sfont = _content.Load<SpriteFont>("Fonts/Splash");
            var imgLocation = context.CenterImageOnScreen(pan, 0.6f);
            var imgScale = new Vector2(0.6f, 0.6f);
            context.DrawingQueue.Enqueue(new SpriteDrawCommand(pan, imgLocation, null,
                Color.White * 0.4f, 0f, Vector2.Zero, imgScale, SpriteEffects.None, 1f));
            string splashText = "   「三咲のパン屋  」\n   Misaki's  Bakery";
            float textScaling = 0.6f;
            var textLocation = context.CenterTextOnscreen(sfont, splashText, textScaling);
            context.DrawingQueue.Enqueue(new TextDrawCommand(sfont, splashText, textLocation,
                Color.Black, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0));
        }
    }
}