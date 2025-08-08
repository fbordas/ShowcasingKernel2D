using Kernel2D.Drawing;
using Kernel2D.Input;
using Kernel2D.Input.Bridges.Menu;
using Kernel2D.Menus;
using Kernel2D.Screens;
using Kernel2D.Screens.ScreenTransitions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformingProject.Core.Screens
{
    internal class PauseScreen : MenuScreen
    {
        SpriteFont _menuFont;
        string _titleText = "Pause Menu";
        SpriteFont _titleFont;
        VerticalMenuList _menuList;
        LabelOption _resume;
        LabelOption _exit;
        FadeTransition quitTransIn = new(0.15f, false, Color.Black);
        FadeTransition quitTransOut = new(0.15f, true, Color.Black);

        public PauseScreen(ContentManager content, IInputBridge input)
            : base(content, input)
        {
            var quitTrans = new ScreenTransitionPair(quitTransOut, quitTransIn);
            _content = content;
            _menuFont = _content.Load<SpriteFont>(@"Fonts/MenuOption");
            _titleFont = _content.Load<SpriteFont>(@"Fonts/TitleText");
            _menuList = new VerticalMenuList(175f, 45f, _menuFont, false);
            _resume = new LabelOption("Resume Game", () => {
                ScreenManager.Instance.PopScreen();
            });


            _exit = new LabelOption("Quit game", () => {
                ScreenManager.Instance.ChangeScreen("GameOverScreen", content, quitTrans);
            });
            _menuList.AddOption(_resume);;
            _menuList.AddOption(_exit);
        }
        public override string ID => "PauseScreen";

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            var overlay = new SpriteDrawCommand(context.WhitePixel, Vector2.Zero, null, Color.Black * 0.6f,
                0f, Vector2.Zero, context.Graphics.Viewport.Bounds.Size.ToVector2(), SpriteEffects.None, 0.01f);
            context.DrawingQueue.Enqueue(overlay);
            var txcmd = new TextDrawCommand(_titleFont, _titleText,
                context.CenterTextHorizontally(_titleFont, _titleText, 0.4f, 50f),
                Color.LightBlue, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0.001f);
            context.DrawingQueue.Enqueue(txcmd);
            _menuList.Draw(context);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Input.Update();
            _menuList.Update((IMenuInputBridge)Input);
        }
    }
}