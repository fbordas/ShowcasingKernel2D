using Kernel2D.Drawing;
using Kernel2D.Helpers;
using Kernel2D.Screens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using PlatformingProject.Core.GameEntities;

namespace PlatformingProject.Core.Screens
{
    internal class PlatformerGameTestScreen : GameScreen
    {
        public PlatformerGameTestScreen(ContentManager content) : base(content)
        {
            _content = content;
            Input.RegisterButtonMapping(Input.MoveLeftActionName, [Buttons.LeftThumbstickLeft, Buttons.DPadLeft]);
            Input.RegisterButtonMapping(Input.MoveRightActionName, [Buttons.LeftThumbstickRight, Buttons.DPadRight]);
            Input.RegisterButtonMapping(Input.JumpActionName, [Buttons.A]);
            Input.RegisterButtonMapping(Input.DashActionName, [Buttons.RightShoulder]);
        }

        public override string ID => "GameplayScreen";

        private PlatformerPlayerCharacter _player = null;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            var sheet = EntitySpritesheetLoader.LoadEntitySpritesheet
                <PlatformingGame>("Player.zero_base", _content);

            _player = new(new(100, 400), null!, sheet);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Input.Update();
            _player.ProcessPlayerActions(Input);
            _player.Update(gameTime, Input);
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            _player.SetDrawContextIfUnset(context);
            _player.Draw(context.GameTime);
        }
    }
}