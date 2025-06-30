using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Kernel2D
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

        protected KeyboardState _previousKb;
        protected GamePadState _previousGp;

        protected PlayerIndex _player = PlayerIndex.One;

        protected readonly Dictionary<string, Keys[]> _keyMappings = [];
        protected readonly Dictionary<string, Buttons[]> _padMappings = [];

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
                keys.Any(k => _previousKb.IsKeyDown(k)) ||
            _padMappings.TryGetValue(action, out var buttons) &&
                buttons.Any(b => _previousGp.IsButtonDown(b));

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
                System.Diagnostics.Debug.WriteLine(ae.ToString());
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
            _previousKb = _kb;
            _previousGp = _gp;

            _kb = Keyboard.GetState();
            _gp = GamePad.GetState(_player);
        }
    }
}