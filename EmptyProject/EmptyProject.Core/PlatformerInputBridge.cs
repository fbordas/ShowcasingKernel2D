using System.Collections.Generic;
using System.Linq;
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

        public bool MoveLeft() => InputHeld("move_left");
 
        public bool MoveRight() => InputHeld("move_right");

        public bool IsIdle() => !MoveLeft() && !MoveRight() && !InputPressed("dash") && !InputHeld("jump");

        public bool InputPressed(string action)
        {
            return (_keyMappings.TryGetValue(action, out var keys) &&
                keys.Any(k => _kb.IsKeyDown(k) && !_previousKb.IsKeyDown(k)))
                || (_gp.IsConnected && _padMappings.TryGetValue(action, out var buttons) &&
                buttons.Any(b => _gp.IsButtonDown(b) && !_previousGp.IsButtonDown(b)));
        }

        public bool InputHeld(string action)
        {
            return (_keyMappings.TryGetValue(action, out var keys) && 
                keys.Any(k => _kb.IsKeyDown(k))) || (_gp.IsConnected && 
                _padMappings.TryGetValue(action, out var buttons) 
                && buttons.Any(b => _gp.IsButtonDown(b)));
        }
    }
}
