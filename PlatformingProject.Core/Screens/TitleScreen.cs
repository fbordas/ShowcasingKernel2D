using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Kernel2D.Drawing;
using MonoGame.Kernel2D.Input;
using MonoGame.Kernel2D.Screens;

namespace PlatformingProject.Core.Screens
{
    internal class TitleScreen : MenuScreen
    {
        private readonly List<string> MenuItems = ["Start Game", "Options", "Exit"];

        private int _selectedIndex = 0;
        private readonly string titleText = "THIS IS A TITLE SCREEN";

        public override string ID => "TitleScreen";

        public override void Update(GameTime gameTime)
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
                    case 0: 
                        ScreenManager.Instance.ChangeScreen("GameplayScreen", _content); break;
                    case 1: 
                        ScreenManager.Instance.ChangeScreen("OptionsScreen", _content); break;
                    case 2: OnExitRequested(); break;
                }
            }
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Graphics.Clear(Color.Navy);
            float titleScaling = 3f;
            float menuScaling = 1.5f;
            var titleLocation = context.CenterHorizontally(titleText, titleScaling, 100);
            var menuLocation = context.CenterHorizontally(MenuItems[_selectedIndex], menuScaling, 400);


            context.SpriteBatch.DrawString(context.Font, titleText, titleLocation, Color.White,
                0, new Vector2(0,0), titleScaling, SpriteEffects.None, 0);
            context.SpriteBatch.DrawString(context.Font, MenuItems[_selectedIndex],
                menuLocation, Color.Yellow, 0, new Vector2(0, 0), menuScaling, SpriteEffects.None, 0);
        }

        public TitleScreen(ContentManager content) : base(content) => _content = content;
    }
}