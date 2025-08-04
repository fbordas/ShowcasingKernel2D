using Kernel2D.Drawing;
using Kernel2D.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Menus
{
    /// <summary>
    /// Creates a new menu option with a list of choices for the user
    /// to select one of.
    /// </summary>
    public class ChoiceOption : MenuOption
    {
        private readonly List<string> _options;
        private readonly bool _wraparound;
        private readonly Action<string>? _onChoiche;

        /// <summary>
        /// The currently selected index from the options list.
        /// </summary>
        public int SelectedIndex = 0;

        /// <summary>
        /// Creates a new menu option with which the user can pick a value
        /// from a list of options.
        /// </summary>
        /// <param name="text">The label text to display.</param>
        /// <param name="options">The list of selectable options.</param>
        /// <param name="wraparound">Whether the current selection should wrap around
        /// when reaching the end of the options list.</param>
        /// <param name="onChoiche">(Optional) An action to invoke when selecting
        /// one of the listed options. The name is purposefully misspelled in reference
        /// to an old Super Mario Maker community inside joke.</param>
        public ChoiceOption(string text, List<string> options, bool wraparound,
            Action<string>? onChoiche = null) : base(text)
        { 
            _options = options;
            _wraparound = wraparound;
            _onChoiche = onChoiche;
        }

        /// <summary>
        /// Draws the current menu option onscreen.
        /// </summary>
        /// <param name="context">The <see cref="DrawContext"/> to use to draw.</param>
        /// <param name="font">The <see cref="SpriteFont"/> to render the text with.</param>
        /// <param name="selected">Whether the current menu option is selected or not.</param>
        public override void Draw(DrawContext context, SpriteFont font, bool selected)
        {
            var labelColor = selected ? SelectedColor : UnselectedColor;
            string currentValue = "<< " + (_options.Count > 0 ? _options[SelectedIndex] : "---") + " >>";
            float textScaling = 0.6f;

            // Draw the label
            var labelCmd = new TextDrawCommand(
                font, LabelText, Position, labelColor, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0);
            context.DrawingQueue.Enqueue(labelCmd);

            // Position the value text to the right of the label
            var valueCmd = new TextDrawCommand(
                font, currentValue, new Vector2(context.Graphics.Viewport.Width - 200, Position.Y),
                labelColor, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0);
            context.DrawingQueue.Enqueue(valueCmd);
        }

        /// <summary>
        /// Triggers the logic of the selected option and returns choice selection
        /// to the menu itself.
        /// </summary>
        public override void OnAccept() => _onChoiche?.Invoke(_options[SelectedIndex]);

        /// <summary>
        /// Changes the currently selected value index one position to the "left"
        /// of the list of options.
        /// </summary>
        public override void OnLeft()
        {
            if (_wraparound)
            {
                SelectedIndex = (SelectedIndex - 1 + _options.Count) % _options.Count;
            }
            else
            { 
                SelectedIndex = Math.Max(0, SelectedIndex - 1);
            }
        }

        /// <summary>
        /// Changes the currently selected value index one position to the "right"
        /// of the list of options.
        /// </summary>
        public override void OnRight()
        {
            if (_wraparound)
            {
                SelectedIndex = (SelectedIndex + 1) % _options.Count;
            }
            else
            {
                SelectedIndex = Math.Min(_options.Count - 1, SelectedIndex + 1);
            }
        }

        /// <summary>
        /// Does nothing for this particular menu option type.
        /// </summary>
        /// <param name="input">Not applicable.</param>
        public override void Update(IMenuInputBridge input) { return; }
    }
}