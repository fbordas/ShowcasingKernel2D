using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using K2D = MonoGame.Kernel2D;

namespace EmptyProject.Core
{
    public class PlatformerInputBridge : K2D.InputBridge
    {
        private readonly Dictionary<string, Keys[]> _keyMappings = new()
        {
            { "dash", new[] { Keys.LeftShift } },
            { "jump", new[] { Keys.Space } },
            { "move_left", new[] { Keys.A, Keys.Left } },
            { "move_right", new[] { Keys.D, Keys.Right } }
        };

        private readonly Dictionary<string, Buttons[]> _padMappings = new()
        {
            { "dash", new[] { Buttons.RightShoulder } },
            { "jump", new[] { Buttons.A } },
            { "move_left", new[] { Buttons.DPadLeft } },
            { "move_right", new[] { Buttons.DPadRight } }
        };

        public override void Update()
        {
            _previousKb = _kb;
            _previousGp = _gp;

            _kb = Keyboard.GetState();
            _gp = GamePad.GetState(PlayerIndex.One);
        }

        public bool JumpPressed()
        { 
            bool kbDownNow = _kb.IsKeyDown(Keys.Space);
            bool kbDownBefore = _previousKb.IsKeyDown(Keys.Space);

            bool gpDownNow = _gp.IsButtonDown(Buttons.A);
            bool gpDownBefore = _previousGp.IsButtonDown(Buttons.A);

            return (kbDownNow && !kbDownBefore) || (gpDownNow && !gpDownBefore);
        }

        public bool MoveLeft()
            => _kb.IsKeyDown(Keys.Left) || _kb.IsKeyDown(Keys.A) || 
                _gp.IsButtonDown(Buttons.DPadLeft) || _gp.IsButtonDown(Buttons.LeftThumbstickLeft);

        public bool MoveRight()
            => _kb.IsKeyDown(Keys.Right) || _kb.IsKeyDown(Keys.D) || 
                _gp.IsButtonDown(Buttons.DPadRight) || _gp.IsButtonDown(Buttons.LeftThumbstickRight);

        public bool DashPressed()
        { 
            bool kbDownNow = _kb.IsKeyDown(Keys.LeftShift) || _kb.IsKeyDown(Keys.RightShift);
            bool kbDownBefore = _previousKb.IsKeyDown(Keys.LeftShift) || _previousKb.IsKeyDown(Keys.RightShift);

            bool gpDownNow = _gp.IsButtonDown(Buttons.RightShoulder);
            bool gpDownBefore = _previousGp.IsButtonDown(Buttons.RightShoulder);

            return (kbDownNow && !kbDownBefore) || (gpDownNow && !gpDownBefore);
        }

        public bool IsIdle() => !MoveLeft() && !MoveRight() && !JumpPressed() && !DashPressed();
    }
}
