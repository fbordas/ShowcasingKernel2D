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
    internal class OptionsSubScreen : SettingsScreen
    {
        private readonly SpriteFont _font;

        public OptionsSubScreen(ContentManager content, SpriteFont font) : base(content)
        {
            _content = content;
            _font = _content.Load<SpriteFont>(@"Fonts/MenuOption");
            optionsTrans = new ScreenTransitionPair(optionsTransOut, optionsTransIn);
            _fullScreen = new ToggleOption("Test option 1", false, null);
            _enableSound = new ToggleOption("Test option 2", true, null);
            _resolutions = new ChoiceOption("Test option 3",
                ["Value 1", "Value 2", "Value 3", "Value 4", "Value 5"], true, null);
            _exit = new LabelOption("Return", () => {
                ScreenManager.Instance.ChangeScreen("OptionsScreen", content, optionsTrans);
            });

            _menuItems = new(new(80f, 200f), 180, _font);
            _menuItems.AddOption(_fullScreen);
            _menuItems.AddOption(_enableSound);
            _menuItems.AddOption(_resolutions);
            _menuItems.AddOption(_exit);
        }

        public override string ID => "OptionsSubMenu";

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            context.Graphics.Clear(Color.DarkRed);
            _menuItems.Draw(context);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Input.Update();
            _menuItems.Update(Input);
            if (Input.GetInputState(Input.CancelOrReturnActionName) == InputState.Pressed)
            {
                ScreenManager.Instance.ChangeScreen("TitleScreen", _content, optionsTrans);
            }
        }

        VerticalMenuList _menuItems;
        ToggleOption _fullScreen;
        ToggleOption _enableSound;
        ChoiceOption _resolutions;
        LabelOption _exit;
        FadeTransition optionsTransOut = new(0.15f, false, Color.Black);
        FadeTransition optionsTransIn = new(0.15f, true, Color.Black);
        ScreenTransitionPair optionsTrans;
    }
}