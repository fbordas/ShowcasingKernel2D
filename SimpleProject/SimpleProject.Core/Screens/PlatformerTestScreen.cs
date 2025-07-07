using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Kernel2D.Animation;
using MonoGame.Kernel2D.Screens;
using Debugger = MonoGame.Kernel2D.Helpers.DebugHelpers;
using Player2D = PlatformerGameProject.Core.PlatformerPlayerCharacter;
using PlayerState = MonoGame.Kernel2D.Entities.PlatformerPlayerState;
using XVector = Microsoft.Xna.Framework.Vector2;

#pragma warning disable
namespace PlatformerGameProject.Core.Screens
{
    internal class PlatformerTestScreen : GameScreen
    {
        #region resources
        PlatformerPlayerCharacter _player;
        SpriteBatch _sb;
        Spritesheet _sheet;
        Texture2D _tex;
        SpriteFont _font;
        #endregion

        #region properties
        private float GroundLevel = 400f;
        #endregion


        internal PlatformerTestScreen(SpriteBatch sb, Texture2D tex, Spritesheet sheet, SpriteFont font)
        {
            _sb = sb;
            _tex = tex;
            _sheet = sheet;
            _font = font;

            _player = new Player2D(new XVector(150, GroundLevel), _sb, _sheet, _tex, _font)
            {
                CurrentState = PlayerState.Idle
            };
            _input.RegisterKeyMapping("dash", [Keys.LeftShift]);
            _input.RegisterKeyMapping("jump", [Keys.Space]);
            _input.RegisterKeyMapping("move_left", [Keys.A, Keys.Left]);
            _input.RegisterKeyMapping("move_right", [Keys.D, Keys.Right]);
            _input.RegisterButtonMapping("dash", [Buttons.RightShoulder]);
            _input.RegisterButtonMapping("jump", [Buttons.A, Buttons.B]);
            _input.RegisterButtonMapping("move_left", [Buttons.DPadLeft, Buttons.LeftThumbstickLeft]);
            _input.RegisterButtonMapping("move_right", [Buttons.DPadRight, Buttons.LeftThumbstickRight]);
            _input.RegisterButtonMapping("pause", [Buttons.Start]);
            Debugger.WriteLine("PlatformerTestScreen initialized with player character.");
        }
    }
}
