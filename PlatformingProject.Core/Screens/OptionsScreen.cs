using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Drawing;
using MonoGame.Kernel2D.Input;
using MonoGame.Kernel2D.Screens;

namespace PlatformingProject.Core.Screens
{
    internal class OptionsScreen : SettingsScreen
    {
        public OptionsScreen(ContentManager content) : base(content) => _content = content;

        public override string ID => "OptionsScreen";

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Graphics.Clear(Color.DarkGray);
            string optionsText = "this is an options screen";
            context.SpriteBatch.Begin(transformMatrix: context.TransformMatrix);
            context.SpriteBatch.DrawString(context.Font, optionsText, new Vector2(100, 100), Color.White,
                0, new Vector2(0,0), 5f, SpriteEffects.None, 0);
            context.SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Input.Update();
            if (Input.GetInputState(Input.CancelOrReturnActionName) == InputState.Pressed)
            {
                ScreenManager.Instance.ChangeScreen("TitleScreen", _content);
            }
        }
    }
}