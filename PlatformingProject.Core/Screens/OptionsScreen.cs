using Kernel2D.Drawing;
using Kernel2D.Input;
using Kernel2D.Screens;
using Kernel2D.Screens.ScreenTransitions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformingProject.Core.Screens
{
    internal class OptionsScreen : SettingsScreen
    {
        private readonly SpriteFont _font;

        public OptionsScreen(ContentManager content, SpriteFont font) : base(content)
        {
            _content = content;
            _font = font;
        }

        public override string ID => "OptionsScreen";

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Graphics.Clear(Color.DarkRed);
            string optionsText = "This should be an options screen\nbut there's nothing here quite yet";
            float textScaling = 3f;
            var location = context.CenterTextHorizontally(_font, optionsText, textScaling, 100);    
            context.DrawingQueue.Enqueue(new TextDrawCommand(_font, optionsText, location,
                Color.White, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Input.Update();
            var optionsTransOut = new FadeTransition(0.15f, false, Color.Black);
            var optionsTransIn = new FadeTransition(0.15f, true, Color.Black);
            var optionsTrans = new ScreenTransitionPair(optionsTransOut, optionsTransIn);
            if (Input.GetInputState(Input.CancelOrReturnActionName) == InputState.Pressed)
            {
                ScreenManager.Instance.ChangeScreen("TitleScreen", _content, optionsTrans);
            }
        }
    }
}