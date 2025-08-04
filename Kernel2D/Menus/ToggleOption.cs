using Kernel2D.Drawing;
using Kernel2D.Input;

using Microsoft.Xna.Framework.Graphics;

using XVector = Microsoft.Xna.Framework.Vector2;

namespace Kernel2D.Menus
{
    /// <summary>
    /// Creates a new menu option with a togglable value (true or false).
    /// </summary>
    public class ToggleOption : MenuOption
    {
        private bool _value = false;
        private Action<bool>? _onToggleCallback = null;

        /// <summary>
        /// The current state of the toggle option.
        /// </summary>
        public bool Value => _value;

        /// <summary>
        /// Creates a new togglable menu option.
        /// </summary>
        /// <param name="labelText">The label text to display.</param>
        /// <param name="defaultValue">The default value of the toggle.</param>
        /// <param name="onToggle">The action to execute when the toggle state is changed</param>
        public ToggleOption(string labelText, bool defaultValue = false,
            Action<bool>? onToggle = null) : base(labelText)
        {
            _value = defaultValue;
            _onToggleCallback = onToggle;
            // TODO: ToggleOption() does this need anything special/extra? IDFK, no extra constructor setup needed atm
        }

        /// <summary>
        /// Draws the current menu option onscreen.
        /// </summary>
        /// <param name="context">The <see cref="DrawContext"/> to use to draw.</param>
        /// <param name="font">The <see cref="SpriteFont"/> to render the text with.</param>
        /// <param name="selected">Whether the current menu option is selected or not.</param>
        public override void Draw(DrawContext context, SpriteFont font, bool selected)
        {
            var color = selected ? SelectedColor : UnselectedColor;
            string toggleText = _value ? "[On]" : "[Off]";
            var textScaling = 0.6f;
            var labelCmd = new TextDrawCommand(
                font, LabelText, Position, color, 0, XVector.Zero, textScaling, SpriteEffects.None, 0);
            context.DrawingQueue.Enqueue(labelCmd);

            var toggleCmd = new TextDrawCommand(font, toggleText,
                new XVector(context.Graphics.Viewport.Width - 200, Position.Y),
                color, 0, XVector.Zero, textScaling, SpriteEffects.None, 0);
            context.DrawingQueue.Enqueue(toggleCmd);
        }

        /// <summary>
        /// Changes the state of the toggle.
        /// </summary>
        public override void OnAccept()
        {
            _value = !_value;
            _onToggleCallback?.Invoke(_value);
        }

        /// <summary>
        /// Does nothing for this particular menu option type.
        /// </summary>
        public override void OnLeft() { return; }

        /// <summary>
        /// Does nothing for this particular menu option type.
        /// </summary>
        public override void OnRight() { return; }

        /// <summary>
        /// Does nothing for this particular menu option type.
        /// </summary>
        /// <param name="input">Not applicable.</param>
        public override void Update(IMenuInputBridge input) { return; }
    }
}