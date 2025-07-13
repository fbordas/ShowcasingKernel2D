using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Debugger = Kernel2D.Helpers.DebugHelpers;

#pragma warning disable
namespace Kernel2D.Input
{
    /// <summary>
    /// Describes the current state of a given input.
    /// </summary>
    public enum InputState
    {
        None,
        Pressed,
        Held,
        Released
    }

    /// <summary>
    /// An intermediate inheritable class to create user input polling entities.
    /// </summary>
    public abstract class InputBridge
    {
        protected KeyboardState _kb;
        protected GamePadState _gp;

        protected KeyboardState _prevKb;
        protected GamePadState _prevGp;

        protected PlayerIndex _player = PlayerIndex.One;

        /// <summary>
        /// Reserved action identifiers. Triple underscores before and after prevent
        /// collisions with user-defined actions. Do NOT rename without refactoring
        /// all bridge logic.
        /// </summary>
        private const string
            UpDirection = "___up___",
            DownDirection = "___down___",
            LeftDirection = "___left___",
            RightDirection = "___right___",
            AcceptAction = "___ok___";

        public string DefaultUpAction => UpDirection;
        public string DefaultDownAction => DownDirection;
        public string DefaultLeftAction => LeftDirection;
        public string DefaultRightAction => RightDirection;
        public string DefaultAcceptAction => AcceptAction;

        protected readonly Dictionary<string, Keys[]> _keyMappings = new()
        {
            { UpDirection, [Keys.Up, Keys.W] },
            { LeftDirection, [Keys.Left, Keys.A] },
            { DownDirection, [Keys.Down, Keys.S] },
            { RightDirection, [Keys.Right, Keys.D] },
            { AcceptAction, [Keys.Enter] }

        };
        protected readonly Dictionary<string, Buttons[]> _padMappings = new()
        {
            { UpDirection, [Buttons.DPadUp, Buttons.LeftThumbstickUp] },
            { LeftDirection, [Buttons.DPadLeft, Buttons.LeftThumbstickLeft] },
            { DownDirection, [Buttons.DPadDown, Buttons.LeftThumbstickDown] },
            { RightDirection, [Buttons.DPadRight, Buttons.LeftThumbstickRight] },
            { AcceptAction, [Buttons.Start] }
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
    }
}