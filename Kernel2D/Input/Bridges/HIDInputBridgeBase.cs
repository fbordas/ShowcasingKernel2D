using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Debugger = Kernel2D.Helpers.DebugHelpers;

namespace Kernel2D.Input.Bridges
{
    /// <summary>
    /// An intermediate inheritable class to create user input polling entities
    /// processing signals from gamepads or keyboards.
    /// </summary>
    public abstract class HIDInputBridgeBase : IInputBridge
    {
        /// <summary>
        /// The current state of the keyboard inputs.
        /// </summary>
        protected KeyboardState _kb;

        /// <summary>
        /// The current state of the gamepad inputs.
        /// </summary>
        protected GamePadState _gp;

        /// <summary>
        /// The previous state of the keyboard inputs, used to
        /// determine if an action was pressed or released in the
        /// previous frame.
        /// </summary>
        protected KeyboardState _prevKb;

        /// <summary>
        /// The previous state of the gamepad inputs, used to
        /// determine if an action was pressed or released in the
        /// previous frame.
        /// </summary>
        protected GamePadState _prevGp;

        /// <summary>
        /// The player index for the gamepad input. This is used to
        /// determine which gamepad to poll for input.
        /// </summary>
        protected PlayerIndex _player = PlayerIndex.One;

        /// <summary>
        /// Reserved action identifiers. Triple underscores before and after prevent
        /// collisions with user-defined actions. Do NOT rename without refactoring
        /// all bridge logic.
        /// </summary>
        private const string
            UpDirection = DefaultInputActionNames.UpDirection,
            DownDirection = DefaultInputActionNames.DownDirection,
            LeftDirection = DefaultInputActionNames.LeftDirection,
            RightDirection = DefaultInputActionNames.RightDirection,
            AcceptAction = DefaultInputActionNames.AcceptAction,
            CancelAction = DefaultInputActionNames.CancelAction;

        /// <summary>
        /// Gets the default action identifier for the up action.
        /// </summary>
        public string DefaultUpAction => UpDirection;

        /// <summary>
        /// Gets the default action identifier for the down action.
        /// </summary>
        public string DefaultDownAction => DownDirection;

        /// <summary>
        /// Gets the default action identifiers for the left action.
        /// </summary>
        public string DefaultLeftAction => LeftDirection;

        /// <summary>
        /// Gets the default action identifier for the right action.
        /// </summary>
        public string DefaultRightAction => RightDirection;

        /// <summary>
        /// Gets the default action identifier for the accept action.
        /// </summary>
        public string DefaultAcceptAction => AcceptAction;

        /// <summary>
        /// Gets the default action identifier for the cancel action.
        /// </summary>
        public string DefaultCancelAction => CancelAction;

        /// <summary>
        /// A dictionary mapping action identifiers to keyboard keys.
        /// </summary>
        protected readonly Dictionary<string, Keys[]> _keyMappings = new()
        {
            { UpDirection, [Keys.Up, Keys.W] },
            { LeftDirection, [Keys.Left, Keys.A] },
            { DownDirection, [Keys.Down, Keys.S] },
            { RightDirection, [Keys.Right, Keys.D] },
            { AcceptAction, [Keys.Enter] },
            { CancelAction, [Keys.Back] }
        };

        /// <summary>
        /// A dictionary mapping action identifiers to gamepad buttons.
        /// </summary>
        protected readonly Dictionary<string, Buttons[]> _padMappings = new()
        {
            { UpDirection, [Buttons.DPadUp, Buttons.LeftThumbstickUp] },
            { LeftDirection, [Buttons.DPadLeft, Buttons.LeftThumbstickLeft] },
            { DownDirection, [Buttons.DPadDown, Buttons.LeftThumbstickDown] },
            { RightDirection, [Buttons.DPadRight, Buttons.LeftThumbstickRight] },
            { AcceptAction, [Buttons.Start, Buttons.A] },
            { CancelAction, [Buttons.Back, Buttons.B] }
        };

        /// <summary>
        /// Determines if the button/key for the specified action is currently
        /// considered as "pressed" at the time of input polling. The action
        /// must exist in the actions dictionary of the current concrete
        /// input bridge.
        /// </summary>
        /// <param name="action">The action to look up.</param>
        /// <returns>True if the action was "pressed" (key/button hit now, not
        /// in previous frame); False otherwise.</returns>
        protected bool IsActionDownNow(string action) =>
            _keyMappings.TryGetValue(action, out var keys) &&
                keys.Any(k => _kb.IsKeyDown(k)) ||
            _padMappings.TryGetValue(action, out var buttons) &&
                buttons.Any(b => _gp.IsButtonDown(b));

        /// <summary>
        /// Determines if the button/key for the specified action was already
        /// being pressed during the previous frame at the time of input polling.
        /// The action must exist in the actions dictionary of the current concrete
        /// input bridge.
        /// </summary>
        /// <param name="action">The action to look up.</param>
        /// <returns>True if the action was "pressed" during the previous frame;
        /// False otherwise.</returns>
        protected bool WasActionDownLastFrame(string action) =>
            _keyMappings.TryGetValue(action, out var keys) &&
                keys.Any(k => _prevKb.IsKeyDown(k)) ||
            _padMappings.TryGetValue(action, out var buttons) &&
                buttons.Any(b => _prevGp.IsButtonDown(b));

        /// <summary>
        /// Registers a key mapping for a specific action. The action must be
        /// a valid string and the keys array must not be null or empty. If the
        /// action already exists, it will be overwritten with the new keys
        /// mapping.
        /// </summary>
        /// <param name="action">
        /// The action to register the key mapping for. Must be a valid string.
        /// </param>
        /// <param name="keys">
        /// The array of keys to map to the action. Must not be null or empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action is null, empty, or the keys array is null or empty.
        /// </exception>
        public void RegisterKeyMapping(string action, Keys[] keys)
        {
            if (string.IsNullOrEmpty(action))
            { 
                throw new ArgumentException("Action must be a valid string.",
                    nameof(action));
            }
            if (keys == null || keys.Length == 0)
            {
                throw new ArgumentException("Keys array must not be null or empty.",
                    nameof(keys));
            }
            _keyMappings[action] = keys;
        }

        /// <summary>
        /// Registers a new gamepad button mapping for a given action. The action must
        /// be a valid string and the buttons array must not be null or empty. If the
        /// action /// already exists, it will be overwritten with the new buttons mapping.
        /// </summary>
        /// <param name="action">
        /// The action to register the button mapping for. Must be a valid string.
        /// </param>
        /// <param name="buttons">
        /// The array of gamepad buttons to map to the action. Must not be null or empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action is null, empty, or the buttons array is null or empty.
        /// </exception>
        public void RegisterButtonMapping(string action, Buttons[] buttons)
        {
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentException("Action must be a valid string.",
                    nameof(action));
            }
            if (buttons == null || buttons.Length == 0)
            {
                throw new ArgumentException("Buttons array must not be null or empty.",
                    nameof(buttons));
            }
            _padMappings[action] = buttons;
        }

        /// <summary>
        /// Gets the input state of a given mapped action. The action to look up
        /// must exist in the actions dictionary of the current concrete input bridge.
        /// </summary>
        /// <param name="action">The action to look up the state for.</param>
        /// <returns>The current input state of the submitted action.</returns>
        public InputState GetInputState(string action)
        {
            if (!_keyMappings.ContainsKey(action) && !_padMappings.ContainsKey(action))
            {
                var ae = 
                    new ArgumentException($"Action '{action}' not found in input mappings.",
                    nameof(action));
                Debugger.WriteLine(ae.ToString());
                throw ae;
            }
            bool isDownNow = IsActionDownNow(action);
            bool wasDownBefore = WasActionDownLastFrame(action);
            if (isDownNow && !wasDownBefore) return InputState.Pressed;
            if (isDownNow && wasDownBefore) return InputState.Held;
            if (!isDownNow && wasDownBefore) return InputState.Released;
            return InputState.None;
        }

        /// <summary>
        /// Updates the current state of the Input Bridge based on
        /// previous and current inputs.
        /// </summary>
        public virtual void Update()
        {
            _prevKb = _kb;
            _prevGp = _gp;

            _kb = Keyboard.GetState();
            _gp = GamePad.GetState(_player);
        }

        /// <summary>
        /// Returns a collection of action identifiers that are defined
        /// in this input bridge. This is used to determine what actions can be
        /// triggered by the input bridge.
        /// </summary>
        /// <returns>
        /// An enumerable collection of action identifiers defined in this input bridge.
        /// </returns>
        public virtual IEnumerable<string> GetDefinedActions() =>
            _keyMappings.Keys.Concat(_padMappings.Keys).Distinct();

        /// <summary>
        /// Not needed for this bridge, as it does not handle pointer inputs.
        /// </summary>
        public Vector2? GetPointerPosition() => null;
    }
}