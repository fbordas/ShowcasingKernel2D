using Microsoft.Xna.Framework.Input;
using K2D = MonoGame.Kernel2D;

namespace EmptyProject.Core
{
    public class PlatformerInputBridge : K2D.InputBridge
    {
        #region constructor w/ lookup dictionaries
        /// <summary>
        /// Creates a new instance of the PlatformerInputBridge object. Any
        /// pre-populations of the input action dictionaries should be
        /// implemented here.
        /// </summary>
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
        /// <summary>
        /// Moves the entity left.
        /// </summary>
        /// <returns>True if the input state triggers left movement, False otherwise.</returns>
        public bool MoveLeft() => GetInputState("move_left") == K2D.InputState.Held;
 
        /// <summary>
        /// Moves the entity right.
        /// </summary>
        /// <returns>True if the input state triggers right movement, False otherwise.</returns>
        public bool MoveRight() => GetInputState("move_right") == K2D.InputState.Held;

        /// <summary>
        /// Whether the entity is currently immobile and no inputs are being processed.
        /// </summary>
        /// <returns>True if the input state is currently fully released, False otherwise.</returns>
        public bool IsIdle() => !MoveLeft() && !MoveRight() &&
            (GetInputState("dash") == K2D.InputState.None) &&
            (GetInputState("jump") == K2D.InputState.None);

        /// <summary>
        /// Whether an input has been pressed in the current frame.
        /// </summary>
        /// <param name="action">The action to look up in the input dictionaries.</param>
        /// <returns>True if the lookup produces a valid result; False otherwise.</returns>
        public bool InputPressed(string action) => GetInputState(action) == K2D.InputState.Pressed;

        /// <summary>
        /// Whether an input has been held for more than the current frame.
        /// </summary>
        /// <param name="action">The action to look up in the input dictionaries.</param>
        /// <returns>True if the lookup produces a valid result; False otherwise.</returns>
        public bool InputHeld(string action) => GetInputState(action) == K2D.InputState.Held;
        #endregion

        private bool _jumpTriggered = false;
        private bool _dashTriggered = false;

        /// <summary>
        /// Whether a jump has been queued to trigger in the actions stack. Clamps
        /// inputs so no consecutive jumps happen if holding the action key.
        /// </summary>
        /// <returns>True if the jump was triggered; False otherwise.</returns>
        public bool TriggerJump()
        {
            if (_jumpTriggered)
            {
                _jumpTriggered = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Whether a dash has been queued to trigger in the actions stack. Clamps
        /// inputs so no consecutive dashes happen if holding the action key.
        /// </summary>
        /// <returns>True if the dash was triggered; False otherwise.</returns>
        public bool TriggerDash()
        {
            if (_dashTriggered)
            {
                _dashTriggered = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the current state of the input processing machine and
        /// actions stack.
        /// </summary>
        public override void Update()
        {
            base.Update();

            if (GetInputState("jump") == K2D.InputState.Pressed)
            { _jumpTriggered = true; }

            if (GetInputState("dash") == K2D.InputState.Pressed)
            { _dashTriggered = true; }
        }
    }
}
