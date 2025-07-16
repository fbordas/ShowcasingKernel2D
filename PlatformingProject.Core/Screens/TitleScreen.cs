using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Kernel2D.Drawing;
using Kernel2D.Input;
using Kernel2D.Screens;
using Kernel2D.Screens.ScreenTransitions;

namespace PlatformingProject.Core.Screens
{
    internal class TitleScreen : MenuScreen
    {
        private readonly List<string> MenuItems = ["Start Game", "Options", "Exit"];
        private SpriteFont _font;

        private int _selectedIndex = 0;
        private readonly string titleText = "THIS IS A TITLE SCREEN";

        public TitleScreen(ContentManager content, SpriteFont font) : base(content)
        {
            _content = content;
            _font = font;
        }

        public override string ID => "TitleScreen";

        public override void Update(GameTime gameTime)
        {
            Input.Update();
            var optionsTransOut = new FadeTransition(0.15f, false, Color.Black);
            var optionsTransIn = new FadeTransition(0.15f, true, Color.Black);
            var optionsTrans = new ScreenTransitionPair(optionsTransOut, optionsTransIn);

            var gameTransIn = new FadeTransition(2f, true, Color.White);
            var gameTransOut = new FadeTransition(2f, false, Color.White);
            var gameTrans = new ScreenTransitionPair(gameTransOut, gameTransIn);
            if (Input.GetInputState(Input.DefaultDownAction) == InputState.Pressed)
            { _selectedIndex = (_selectedIndex + 1) % MenuItems.Count; }
            if (Input.GetInputState(Input.DefaultUpAction) == InputState.Pressed)
            { _selectedIndex = (_selectedIndex - 1 + MenuItems.Count) % MenuItems.Count; }
            if (Input.GetInputState(Input.DefaultAcceptAction) == InputState.Pressed)
            {
                switch (_selectedIndex)
                {
                    case 0: 
                        ScreenManager.Instance.ChangeScreen("GameplayScreen", _content, gameTrans); break;
                    case 1: 
                        ScreenManager.Instance.ChangeScreen("OptionsScreen", _content, optionsTrans); break;
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
            var titleLocation = context.CenterTextHorizontally(_font, titleText, titleScaling, 100);
            var menuLocation = context.CenterTextHorizontally(_font, MenuItems[_selectedIndex], menuScaling, 400);
            context.DrawingQueue.Enqueue(new TextDrawCommand(_font, titleText, titleLocation,
                Color.White, 0, Vector2.Zero, titleScaling, SpriteEffects.None, 0));
            context.DrawingQueue.Enqueue(new TextDrawCommand(_font, MenuItems[_selectedIndex], menuLocation,
                Color.Yellow, 0, Vector2.Zero, menuScaling, SpriteEffects.None, 0));
        }
    }
}