using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Kernel2D.Drawing;
using Kernel2D.Screens;
using Kernel2D.Screens.ScreenTransitions;
using Kernel2D.Menus;

namespace PlatformingProject.Core.Screens
{
    internal class TitleScreen : MenuScreen
    {
        private SpriteFont _font;
        SpriteFont _menuFont;
        SpriteFont _titleFont;

        private int _selectedIndex = 0;
        private readonly string titleText = "   Test\nMain Menu\n  Screen";

        VerticalMenuList _menuList;
        LabelOption _start;
        LabelOption _options;
        LabelOption _exit;

        FadeTransition gameTransIn = new FadeTransition(2f, true, Color.White);
        FadeTransition gameTransOut = new FadeTransition(2f, false, Color.White);
        ScreenTransitionPair gameTrans;

        FadeTransition optionsTransOut = new FadeTransition(0.15f, false, Color.Black);
        FadeTransition optionsTransIn = new FadeTransition(0.15f, true, Color.Black);
        ScreenTransitionPair optionsTrans;


        public TitleScreen(ContentManager content, SpriteFont font) : base(content)
        {
            _content = content;
            gameTrans = new ScreenTransitionPair(gameTransOut, gameTransIn);
            optionsTrans = new ScreenTransitionPair(optionsTransOut, optionsTransIn);
            _font = font;
            _titleFont = _content.Load<SpriteFont>(@"Fonts/TitleText");
            _menuFont = _content.Load<SpriteFont>(@"Fonts/MenuOption");

            _menuList = new VerticalMenuList(Vector2.Zero, 0f, _menuFont, true);
            _start = new LabelOption("Start Game", () => {
                ScreenManager.Instance.ChangeScreen("GameplayScreen", _content, gameTrans);
            });
            _options = new LabelOption("Options Menu", () => {
                ScreenManager.Instance.ChangeScreen("OptionsScreen", _content, optionsTrans);
            });
            _exit = new LabelOption("Exit Game", () => {
                OnExitRequested();
            });
            _menuList.AddOption(_start);
            _menuList.AddOption(_options);
            _menuList.AddOption(_exit);
        }

        public override string ID => "TitleScreen";

        public override void Update(GameTime gameTime)
        {
            Input.Update();
            _menuList.Update(Input);
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Graphics.Clear(Color.Navy);
            var bg = _content.Load<Texture2D>("GlobalAssets/TitleScreen/titlebg");
            float titleScaling = 1f;
            float menuScaling = 1f;
            float bgScaling = 1.2f;
            var bgLocation = context.CenterImageOnScreen(bg, bgScaling);
            var titleLocation = context.CenterTextHorizontally(_titleFont, titleText, titleScaling, 100);
            var menuLocation = context.CenterTextHorizontally(_menuFont, _menuList.SelectedItem, menuScaling, 800);
            context.DrawingQueue.Enqueue(new SpriteDrawCommand(bg, bgLocation, null, Color.White,
                0, Vector2.Zero, new(bgScaling, bgScaling), SpriteEffects.None, 1f));
            context.DrawingQueue.Enqueue(new TextDrawCommand(_titleFont, titleText, titleLocation,
                Color.White, 0, Vector2.Zero, titleScaling, SpriteEffects.None, 0));
            context.DrawingQueue.Enqueue(new TextDrawCommand(_menuFont, _menuList.SelectedItem, menuLocation,
                Color.Yellow, 0, Vector2.Zero, menuScaling, SpriteEffects.None, 0));
        }
    }
}