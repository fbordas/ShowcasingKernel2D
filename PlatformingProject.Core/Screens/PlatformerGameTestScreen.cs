using System;

using Kernel2D.Drawing;
using Kernel2D.Helpers;
using Kernel2D.Input.Bridges;
using Kernel2D.Screens;
using Kernel2D.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using PlatformingProject.Core.GameEntities;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformingProject.Core.Screens
{
    internal class PlatformerGameTestScreen : GameScreen
    {
        public PlatformerGameTestScreen(ContentManager content, PlatformerInputBridge input)
            : base(content, input)
        {
            _content = content;
            Input.RegisterButtonMapping(Input.MoveLeftActionName, [Buttons.LeftThumbstickLeft, Buttons.DPadLeft]);
            Input.RegisterButtonMapping(Input.MoveRightActionName, [Buttons.LeftThumbstickRight, Buttons.DPadRight]);
            Input.RegisterButtonMapping(Input.JumpActionName, [Buttons.A]);
            Input.RegisterButtonMapping(Input.DashActionName, [Buttons.RightShoulder]);
            Input.RegisterButtonMapping(Input.PauseActionName, [Buttons.Start]);
            Input.RegisterKeyMapping(input.MoveLeftActionName, [Keys.A]);
            Input.RegisterKeyMapping(input.MoveRightActionName, [Keys.D]);
            Input.RegisterKeyMapping(input.JumpActionName, [Keys.Space]);
            Input.RegisterKeyMapping(input.DashActionName, [Keys.LeftShift, Keys.RightShift]);
            Input.RegisterKeyMapping(input.PauseActionName, [Keys.P]);
        }

        public override string ID => "GameplayScreen";

        private PlatformerPlayerCharacter _player = null;
        private Texture2D _bg = null;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            var sheet = EntitySpritesheetLoader.LoadEntitySpritesheet
                <PlatformingGame>("Player.zero_base", _content);

            _bg = _content.Load<Texture2D>("GlobalAssets/lab");

            _player = new(new(100, 400), null!, sheet);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Input.Update();
            if (Input.GetInputState(Input.PauseActionName) == InputState.Pressed)
            {
                ScreenManager.Instance.PushScreen(
                    ScreenManager.Instance.GetScreen("PauseScreen"), _content);
                return;
            }
            _player.ProcessPlayerActions(Input);
            _player.Update(gameTime, Input);
        }

        public override void Draw(DrawContext context)
        {
            base.Draw(context);
            var sprcmd = new SpriteDrawCommand(_bg, Vector2.Zero, null,
                Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
            context.DrawingQueue.Enqueue(sprcmd);
            _player.SetDrawContextIfUnset(context);
            _player.Draw(context.GameTime);
        }
    }
}