using Kernel2D.Drawing;
using Kernel2D.Input;
using Kernel2D.Menus;
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
        private readonly string _titleText = "Main Options Screen (Not working)";

        public OptionsScreen(ContentManager content, SpriteFont font, IInputBridge input) 
            : base(content, input)
        {
            _content = content;
            _font = font;
            optionsTrans = new ScreenTransitionPair(optionsTransOut, optionsTransIn);
            _fullScreen = new ToggleOption("Use full screen", false, null);
            _enableSound = new ToggleOption("Enable sound", true, null);
            _resolutions = new ChoiceOption("Resolution/Viewport size",
                ["800x600", "1024x768", "1280x960"], false, null){ SelectedIndex = 2 };
            _submenu = new SubMenuOption("Go into sub-menu", 
                ScreenManager.Instance.GetScreen("OptionsSubMenu"), optionsTransOut, optionsTransIn);
            _exit = new LabelOption("Exit", () => {
                ScreenManager.Instance.ChangeScreen("TitleScreen", content, optionsTrans);
            });

            _menuItems = new VerticalMenuList(new Vector2(40f, 180f), 45f, _font);
            _menuItems.AddOption(_fullScreen);
            _menuItems.AddOption(_enableSound);
            _menuItems.AddOption(_resolutions);
            _menuItems.AddOption(_submenu);
            _menuItems.AddOption(_exit);
        }

        public override string ID => "OptionsScreen";

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Graphics.Clear(Color.DarkRed);
            var txcmd = new TextDrawCommand(_font, _titleText, context.CenterTextHorizontally(_font, _titleText, 1f, 50f),
                Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
            context.DrawingQueue.Enqueue(txcmd);
            _menuItems.Draw(context);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Input.Update();
            _menuItems.Update((IMenuInputBridge)Input);
            if (((IMenuInputBridge)Input).Cancel == InputState.Pressed)
            {
                ScreenManager.Instance.ChangeScreen("TitleScreen", _content, optionsTrans);
            }
        }

        VerticalMenuList _menuItems;
        ToggleOption _fullScreen;
        ToggleOption _enableSound;
        ChoiceOption _resolutions;
        SubMenuOption _submenu;
        LabelOption _exit;
        FadeTransition optionsTransOut = new(0.15f, false, Color.Black);
        FadeTransition optionsTransIn = new(0.15f, true, Color.Black);
        ScreenTransitionPair optionsTrans;
    }
}