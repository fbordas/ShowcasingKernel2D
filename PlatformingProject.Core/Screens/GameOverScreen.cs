using System.Linq;

using Kernel2D.Drawing;
using Kernel2D.Screens;
using Kernel2D.Screens.ScreenTransitions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformingProject.Core.Screens
{
    internal class GameOverScreen : ScreenBase
    {
        private double _elapsedTime = 0;
        private const double _displayDuration = 6000; // Display for 6 seconds
        private readonly SpriteFont _font;

        public GameOverScreen(SpriteFont font) => _font = font;

        public override string ID => "GameOverScreen";
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
            context.Graphics.Clear(Color.DarkSlateGray);
            var pan = _content.Load<Texture2D>("GlobalAssets/TitleScreen/bread");
            var sfont = _content.Load<SpriteFont>("Fonts/TitleText");
            var imgLocation = context.CenterImageOnScreen(pan, 0.6f);
            var imgScale = new Vector2(0.6f, 0.6f);
            context.DrawingQueue.Enqueue(new SpriteDrawCommand(pan, imgLocation, null,
                Color.White * 0.4f, 0f, Vector2.Zero, imgScale, SpriteEffects.None, 1f));
            string[] text = ["Thanks for t rying", "my proof of concept"];
            float textScaling = 0.6f;
            var textHeight = _font.MeasureString(string.Join(" ", text)).Y * textScaling;
            var topTextLocation = context.CenterTextOnscreen(sfont, text[0], textScaling) - new Vector2(0, textHeight * 5);
            var bottomTextLocation = context.CenterTextOnscreen(sfont, text[1], textScaling) + new Vector2(0, textHeight * 5);
            context.DrawingQueue.Enqueue(new TextDrawCommand(sfont, text[0], topTextLocation,
                Color.Black, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0));
            context.DrawingQueue.Enqueue(new TextDrawCommand(sfont, text[1], bottomTextLocation,
                Color.Black, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0));
        }
    }
}