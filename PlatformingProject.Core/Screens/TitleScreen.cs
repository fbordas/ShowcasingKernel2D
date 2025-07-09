using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Drawing;
using MonoGame.Kernel2D.Input;
using MonoGame.Kernel2D.Screens;

namespace PlatformerGameProject.Core.Screens
{
    internal class TitleScreen : MenuScreen
    {
        private readonly List<string> MenuItems = ["Start Game", "Options", "Exit"];

        private int _selectedIndex = 0;
        private readonly string titleText = "this is a title screen";

        public override string ID => "TitleScreen";

        public override void Update(GameTime gameTime, ContentManager content)
        {
            Input.Update();
            if (Input.GetInputState(Input.DefaultDownAction) == InputState.Pressed)
            { _selectedIndex = (_selectedIndex + 1) % MenuItems.Count; }
            if (Input.GetInputState(Input.DefaultUpAction) == InputState.Pressed)
            { _selectedIndex = (_selectedIndex - 1 + MenuItems.Count) % MenuItems.Count; }
            if (Input.GetInputState(Input.DefaultAcceptAction) == InputState.Pressed)
            {
                switch (_selectedIndex)
                {
                    case 0: ScreenManager.Instance.ChangeScreen("Game", content); break;
                    case 1: ScreenManager.Instance.ChangeScreen("Options", content); break;
                    case 2: OnExitRequested(); break;
                }
            }
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Graphics.Clear(Color.Navy);
            context.SpriteBatch.Begin(transformMatrix: context.TransformMatrix);
            context.SpriteBatch.DrawString(context.Font, titleText, new Vector2(500, 100), Color.White,
                0, new Vector2(0,0), 6f, SpriteEffects.None, 0);
            context.SpriteBatch.DrawString(context.Font, MenuItems[_selectedIndex],
                new Vector2(100, 400), Color.Yellow, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            context.SpriteBatch.End();
        }

        public TitleScreen(ContentManager content) : base(content) => _content = content;
    }
}