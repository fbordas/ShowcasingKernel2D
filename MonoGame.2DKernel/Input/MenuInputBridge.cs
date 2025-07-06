using Microsoft.Xna.Framework.Input;

namespace MonoGame.Kernel2D.Input
{
    /// <summary>
    /// A bridge or translator for input handling of menu-type screens.
    /// </summary>
    public class MenuInputBridge : InputBridge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuInputBridge"/> class.
        /// </summary>
        public MenuInputBridge() { }

        /// <summary>
        /// The action string to identify the select input.
        /// </summary>
        public string SelectActionName { get; protected set; } = "select";

        /// <summary>
        /// The action string to identify the cancel input.
        /// </summary>
        public string CancelActionName { get; protected set; } = "cancel";

        /// <summary>
        /// Sets the action string to identify the select input.
        /// </summary>
        /// <param name="action">
        /// The action string to set for select input.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action string is null or empty.
        /// </exception>
        public void SetSelectActionName(string action)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException("Action must be a valid string.",
                    nameof(action));
            SelectActionName = action;
        }

        /// <summary>
        /// Sets the action string to identify the cancel input.
        /// </summary>
        /// <param name="action">
        /// The action string to set for cancel input.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action string is null or empty.
        /// </exception>
        public void SetCancelActionName(string action)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException("Action must be a valid string.",
                    nameof(action));
            CancelActionName = action;
        }

        /// <summary>
        /// Registers a key mapping for a specific action.
        /// </summary>
        /// <param name="action">
        /// The action to register the key mapping for.
        /// </param>
        /// <param name="keys">
        /// The array of keys to map to the action.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action is not a valid action name or if the keys
        /// array is null or empty.
        /// </exception>
        public void RegisterKeyMapping(string action, Keys[] keys)
        {
            if (string.IsNullOrEmpty(action)
                || action != SelectActionName || action != CancelActionName)
                throw new ArgumentException("Action must be a valid action name.",
                    nameof(action));
            if (keys == null || keys.Length == 0)
                throw new ArgumentException("Keys must not be null or empty.",
                    nameof(keys));
            _keyMappings[action] = keys;
        }

        /// <summary>
        /// Registers a pad mapping for a specific action.
        /// </summary>
        /// <param name="action">
        /// The action to register the pad mapping for.
        /// </param>
        /// <param name="buttons">
        /// The array of buttons to map to the action.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action is not a valid action name or if the buttons
        /// array is null or empty.
        /// </exception>
        public void RegisterPadMapping(string action, Buttons[] buttons)
        {
            if (string.IsNullOrEmpty(action)
                || action != SelectActionName || action != CancelActionName)
                throw new ArgumentException("Action must be a valid action name.",
                    nameof(action));
            if (buttons == null || buttons.Length == 0)
                throw new ArgumentException("Buttons must not be null or empty.",
                    nameof(buttons));
            _padMappings[action] = buttons;
        }
    }
}
