using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Kernel2D.Input.Bridges.Menu
{
    /// <summary>
    /// A bridge for touch input in menu screens, allowing for
    /// touch-based interactions with menu options.
    /// </summary>
    public class TouchMenuInputBridge : IInputBridge
    {
        private TouchCollection _touchState, _previousTouchState;

        /// <summary>
        /// Updates the touch state by polling the current touch state
        /// and storing the previous state in the current frame.
        /// </summary>
        public void Update()
        {
            _previousTouchState = _touchState;
            _touchState = TouchPanel.GetState();
        }

        /// <summary>
        /// Returns a collection of action identifiers that are defined in this
        /// input bridge. This is used to determine what actions can be
        /// triggered by touch inputs in the menu system.
        /// </summary>
        /// <returns>An enumerable collection of action names.</returns>
        public IEnumerable<string> GetDefinedActions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the input state for a specific action.
        /// </summary>
        /// <param name="action">
        /// The action to look up the state for.
        /// </param>
        /// <returns>
        /// The input state for the specified action.
        /// </returns>
        public InputState GetInputState(string action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not needed for this bridge, as it does not handle pointer inputs.
        /// </summary>
        public Vector2? GetPointerPosition() => null;
    }
}