using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Kernel2D.Input.Bridges.Menu
{
    /// <summary>
    /// A bridge for mouse input in menu screens, allowing for
    /// mouse-based interactions with menu options.
    /// </summary>
    public class MouseMenuInputBridge : IInputBridge
    {
        private MouseState _current, _previous;

        /// <summary>
        /// Updates the mouse state by polling the current mouse state
        /// and storing the previous state in the current frame.
        /// </summary>
        public void Update()
        { 
            _previous = _current;
            _current = Mouse.GetState();
        }

        /// <summary>
        /// Gets the input state for a specific action.
        /// </summary>
        /// <param name="action">The action to look up the state for.</param>
        /// <returns>The input state for the specified action.</returns>
        public InputState GetInputState(string action)
        {
            if (action == DefaultInputActionNames.AcceptAction)
            { 
                bool now = _current.LeftButton == ButtonState.Pressed;
                bool prev = _previous.LeftButton == ButtonState.Pressed;
                return ResolveState(now, prev);
            }
            return InputState.None;
        }

        /// <summary>
        /// Resolves the input state based on the current and previous button states.
        /// </summary>
        /// <param name="isDownNow">
        /// Indicates whether the button is currently pressed.
        /// </param>
        /// <param name="wasDownBefore">
        /// Indicates whether the button was pressed in the previous frame.
        /// </param>
        /// <returns>The input state based on the current and previous button states.</returns>
        private static InputState ResolveState(bool isDownNow, bool wasDownBefore)
        { 
            if (isDownNow && !wasDownBefore) return InputState.Pressed;
            if (!isDownNow && wasDownBefore) return InputState.Released;
            if (isDownNow && wasDownBefore) return InputState.Held;
            return InputState.None;
        }

        /// <summary>
        /// This bridge does not define any specific triggerable actions,
        /// so it returns an empty collection.
        /// </summary>
        public IEnumerable<string> GetDefinedActions() => [];

        /// <summary>
        /// Gets the current position of the mouse pointer.
        /// </summary>
        /// <returns>The current position of the mouse pointer as a Vector2.</returns>
        public Vector2? GetPointerPosition() =>
            Mouse.GetState().Position.ToVector2();
    }
}