using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Kernel2D.Input.Bridges.Menu
{
    /// <summary>
    /// A bridge or translator for input handling of menu-type screens with
    /// either keyboard strokes or gamepad button presses.
    /// </summary>
    public class HIDMenuInputBridge : HIDInputBridgeBase, IMenuInputBridge
    {
        private readonly string _accept = "accept";
        private readonly string _cancel = "cancel";
        private readonly string _up = "up";
        private readonly string _down = "down";
        private readonly string _left = "left";
        private readonly string _right = "right";

        /// <summary>
        /// Gets the state of the Up directional input in a menu screen.
        /// </summary>
        public InputState Up => GetInputState(_up);

        /// <summary>
        /// Gets the state of the Down directional input in a menu screen.
        /// </summary>
        public InputState Down => GetInputState(_down);

        /// <summary>
        /// Gets the state of the Left directional input in a menu screen.
        /// </summary>
        public InputState Left => GetInputState(_left);

        /// <summary>
        /// Gets the state of the Right directional input in a menu screen.
        /// </summary>
        public InputState Right => GetInputState(_right);

        /// <summary>
        /// Gets the state of the Accept action input in a menu screen.
        /// </summary>
        public InputState Accept => GetInputState(_accept);

        /// <summary>
        /// Gets the state of the Cancel action input in a menu screen.
        /// </summary>
        public InputState Cancel => GetInputState(_cancel);

        /// <summary>
        /// Not needed for this bridge, as it does not handle pointer inputs.
        /// </summary>
        public bool PointerPressed => false;

        /// <summary>
        /// Not needed for this bridge, as it does not handle pointer inputs.
        /// </summary>
        public bool PointerReleased => false;

        /// <summary>
        /// Not needed for this bridge, as it does not handle pointer inputs.
        /// </summary>
        public Vector2? PointerPosition => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="HIDMenuInputBridge"/>
        /// class with default key and gamepad button mappings.
        /// </summary>
        public HIDMenuInputBridge()
        {
            _keyMappings[_up] = [Keys.W, Keys.Up, Keys.NumPad8];
            _keyMappings[_down] = [Keys.S, Keys.Down, Keys.NumPad2];
            _keyMappings[_left] = [Keys.A, Keys.Left, Keys.NumPad4];
            _keyMappings[_right] = [Keys.D, Keys.Right, Keys.NumPad6];
            _keyMappings[_accept] = [Keys.Enter];
            _keyMappings[_cancel] = [Keys.Back];

            _padMappings[_up] = [Buttons.DPadUp, Buttons.LeftThumbstickUp];
            _padMappings[_down] = [Buttons.DPadDown, Buttons.LeftThumbstickDown];
            _padMappings[_left] = [Buttons.DPadLeft, Buttons.LeftThumbstickLeft];
            _padMappings[_right] = [Buttons.DPadRight, Buttons.LeftThumbstickRight];
            _padMappings[_accept] = [Buttons.A, Buttons.Start];
            _padMappings[_cancel] = [Buttons.B, Buttons.Back];
        }

        ///// <summary>
        ///// Updates the state of the input bridge.
        ///// </summary>
        ///// <exception cref="NotImplementedException"></exception>
        //public override void Update()
        //{
        //    base.Update();
        //}

        /// <summary>
        /// Returns a collection of action identifiers that are defined
        /// in this input bridge.
        /// </summary>
        /// <returns>
        /// An enumerable collection of action identifiers.
        /// </returns>
        public override IEnumerable<string> GetDefinedActions() =>
            _keyMappings.Keys.Concat(_padMappings.Keys);
    }
}