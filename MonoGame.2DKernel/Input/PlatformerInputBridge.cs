using Microsoft.Xna.Framework.Input;

namespace MonoGame.Kernel2D.Input
{
    /// <summary>
    /// A bridge or translator for platformer-style input handling,
    /// allowing for managing keyboard and gamepad inputs.
    /// </summary>
    public class PlatformerInputBridge : InputBridge
    {
        #region properties
        /// <summary>
        /// The action string to identify the dash input.
        /// Defaults to "dash", but can be set to a custom string.
        /// </summary>
        public string DashAction { get; protected set; } = "dash";

        /// <summary>
        /// The action string to identify the jump input.
        /// Defaults to "jump", but can be set to a custom string.
        /// </summary>
        public string JumpAction { get; protected set; } = "jump";
        #endregion

        #region init and setup
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformerInputBridge"/> class.
        /// </summary>
        public PlatformerInputBridge() { }

        /// <summary>
        /// Sets the action string to identify the dash input.
        /// </summary>
        /// <param name="action">
        /// The action string to set for dash input.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action string is null or empty.
        /// </exception>
        public void SetDashActionName(string action)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException("Action must be a valid string.",
                    nameof(action));
            DashAction = action;
        }

        /// <summary>
        /// Sets the action string to identify the jump input.
        /// </summary>
        /// <param name="action">
        /// The action string to set for jump input.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action string is null or empty.
        /// </exception>
        public void SetJumpActionName(string action)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException("Action must be a valid string.",
                    nameof(action));
            JumpAction = action;
        }

        /// <summary>
        /// Registers a new key mapping for a given action.
        /// </summary>
        /// <param name="action">
        /// The action to register the key mapping for. Must be a valid ID.
        /// </param>
        /// <param name="keys">
        /// The array of keys to map to the action. Must not be null or empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action is null, empty, or the keys array is null or empty.
        /// </exception>
        public void RegisterKeyMapping(string action, Keys[] keys)
        {
            if (string.IsNullOrEmpty(action) || keys == null || keys.Length == 0)
                throw new ArgumentException("Action and keys must be valid.");
            _keyMappings[action] = keys;
        }

        /// <summary>
        /// Registers a new gamepad button mapping for a given action.
        /// </summary>
        /// <param name="action">
        /// The action to register the button mapping for. Must be a valid ID.
        /// </param>
        /// <param name="buttons">
        /// The array of gamepad buttons to map to the action. Must not be null or empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action is null, empty, or the buttons array is null or empty.
        /// </exception>
        public void RegisterPadMapping(string action, Buttons[] buttons)
        {
            if (string.IsNullOrEmpty(action) || buttons == null || buttons.Length == 0)
                throw new ArgumentException("Action and buttons must be valid.");
            _padMappings[action] = buttons;
        }
        #endregion

        #region shortcuts/shorthands
        /// <summary>
        /// Moves the entity left.
        /// </summary>
        /// <returns>True if the input state triggers left movement, False otherwise.</returns>
        public bool MoveLeft() => GetInputState("move_left") == InputState.Held;

        /// <summary>
        /// Moves the entity right.
        /// </summary>
        /// <returns>True if the input state triggers right movement, False otherwise.</returns>
        public bool MoveRight() => GetInputState("move_right") == InputState.Held;

        /// <summary>
        /// Whether the entity is currently immobile and no inputs are being processed.
        /// </summary>
        /// <returns>True if the input state is currently fully released, False otherwise.</returns>
        public bool IsIdle() => !MoveLeft() && !MoveRight() &&
            (GetInputState(DashAction) == InputState.None) &&
            (GetInputState(JumpAction) == InputState.None);

        /// <summary>
        /// Whether an input has been pressed in the current frame.
        /// </summary>
        /// <param name="action">The action to look up in the input dictionaries.</param>
        /// <returns>True if the lookup produces a valid result; False otherwise.</returns>
        public bool InputPressed(string action) => GetInputState(action) == InputState.Pressed;

        /// <summary>
        /// Whether an input has been held for more than the current frame.
        /// </summary>
        /// <param name="action">The action to look up in the input dictionaries.</param>
        /// <returns>True if the lookup produces a valid result; False otherwise.</returns>
        public bool InputHeld(string action) => GetInputState(action) == InputState.Held;
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

            if (GetInputState("jump") == InputState.Pressed)
            { _jumpTriggered = true; }

            if (GetInputState("dash") == InputState.Pressed)
            { _dashTriggered = true; }
        }
    }
}
