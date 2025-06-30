using Microsoft.Xna.Framework.Input;
using K2D = MonoGame.Kernel2D;

namespace EmptyProject.Core
{
    public class PlatformerInputBridge : K2D.InputBridge
    {
        #region constructor w/ lookup dictionaries
        public PlatformerInputBridge()
        {
            _keyMappings.Add("dash", [Keys.LeftShift]);
            _keyMappings.Add("jump", [Keys.Space]);
            _keyMappings.Add("move_left", [Keys.A, Keys.Left]);
            _keyMappings.Add("move_right", [Keys.D, Keys.Right]);

            _padMappings.Add("dash", [Buttons.RightShoulder]);
            _padMappings.Add("jump", [Buttons.A]);
            _padMappings.Add("move_left", [Buttons.DPadLeft]);
            _padMappings.Add("move_right", [Buttons.DPadRight]);
        }
        #endregion

        #region shortcuts/shorthands
        public bool MoveLeft() => GetInputState("move_left") == K2D.InputState.Held;
 
        public bool MoveRight() => GetInputState("move_right") == K2D.InputState.Held;

        public bool IsIdle() => !MoveLeft() && !MoveRight() &&
            (GetInputState("dash") == K2D.InputState.None) &&
            (GetInputState("jump") == K2D.InputState.None);

        public bool InputPressed(string action) =>
            GetInputState(action) == K2D.InputState.Pressed;

        public bool InputHeld(string action) =>
            GetInputState(action) == K2D.InputState.Held;
        #endregion

        public override void Update()
        {
            base.Update();
        }
    }
}
