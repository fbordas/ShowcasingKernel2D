using Kernel2D.Drawing;
using Kernel2D.Helpers;
using Kernel2D.Screens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using PlatformingProject.Core.GameEntities;

namespace PlatformingProject.Core.Screens
{
    internal class PlatformerGameTestScreen : GameScreen
    {
        public PlatformerGameTestScreen(ContentManager content) : base(content) => _content = content;

        public override string ID => "GameplayScreen";

        private PlatformerPlayerCharacter _player = null;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            var texture = _content.Load<Texture2D>("Player/zero_base");
            var sheet = EntitySpritesheetLoader.LoadEntitySpritesheet
                <PlatformerGameTestScreen>("Player.zero_base", _content);
            var font = _content.Load<SpriteFont>("Fonts/Hud");

            _player = new(new(100f, 400f), null!, sheet, texture, font);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Input.Update();
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