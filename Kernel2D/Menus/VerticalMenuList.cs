using Kernel2D.Drawing;
using Kernel2D.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Menus
{
    /// <summary>
    /// Creates a vertical menu list in a game screen.
    /// </summary>
    public class VerticalMenuList
    {
        /// <summary>
        /// The list of options to show in the menu.
        /// </summary>
        public readonly List<MenuOption> Options = [];

        /// <summary>
        /// The starting location at which the menu should begin rendering.
        /// </summary>
        public readonly Vector2 Start;
        private readonly float Spacing;
        private readonly SpriteFont _font;
        private readonly bool _singleLine = false;
        private readonly bool _carousel = false;

        /// <summary>
        /// An optional cursor to display alongside the menu options.
        /// </summary>
        public MenuCursor? Cursor = null;

        /// <summary>
        /// The currently selected item in the menu.
        /// </summary>
        public int SelectedIndex = 0;

        /// <summary>
        /// Gets the text of the currently selected menu item.
        /// </summary>
        public string SelectedItem => Options[SelectedIndex].LabelText;

        /// <summary>
        /// Creates a new vertical menu at the specified position, with the
        /// specified spacing between items using the provided font.
        /// </summary>
        /// <param name="start">The location to start drawing the menu to.</param>
        /// <param name="spacing">The spacing between menu items.</param>
        /// <param name="font">The font to use to render the menu items.</param>
        /// <param name="singleLine">Whether to render the menu as a single line.</param>
        /// <param name="carousel">If rendering as a single line, whether the menu should
        /// be displayed as a scrolling carousel.</param>
        public VerticalMenuList(Vector2 start, float spacing, SpriteFont font,
            bool singleLine = false, bool carousel = false)
        {
            Start = start;
            Spacing = spacing;
            _font = font;
            _singleLine = singleLine;
            _carousel = carousel;
        }

        /// <summary>
        /// Adds an option to the menu list.
        /// </summary>
        /// <param name="option">The option to add.</param>
        public void AddOption(MenuOption option) => Options.Add(option);

        /// <summary>
        /// Removes an option from the menu list.
        /// </summary>
        /// <param name="id">The ID of the option to remove.</param>
        public void RemoveOption(string id)
        {
            var op = Options.Find(o => o.ID == id);
            if (op != null) Options.Remove(op);
        }

        /// <summary>
        /// Renders the current menu to the screen.
        /// </summary>
        /// <param name="context">The <see cref="DrawContext"/> to use
        /// to draw elements onscreen.</param>
        public void Draw(DrawContext context)
        {
            if (_singleLine)
            {
                if (_carousel)
                {
                    // Carousel mode: draw a few wrapped-around items centered on SelectedIndex
                    int visibleCount = 3;
                    int half = visibleCount / 2;

                    for (int offset = -half; offset <= half; offset++)
                    {
                        int wrappedIndex = (SelectedIndex + offset + Options.Count) % Options.Count;
                        var option = Options[wrappedIndex];

                        float yOffset = offset * Spacing;
                        option.Position = Start + new Vector2(0, yOffset);
                        bool isSelected = (offset == 0);
                        option.Draw(context, _font, isSelected);
                    }
                }
                else
                {
                    // Simple single-line display
                    var option = Options[SelectedIndex];
                    option.Position = Start;
                    option.Draw(context, _font, true);
                }
                return;
            }

            Vector2? selectedPos = null; // Track selected option position for cursor
            // Classic full list mode
            for (int i = 0; i < Options.Count; i++)
            {
                var option = Options[i];
                float yOffset = i * Spacing;
                option.Position = Start + new Vector2(0, yOffset);
                bool isSelected = (i == SelectedIndex);
                option.Draw(context, _font, isSelected);
                if (isSelected) { selectedPos = option.Position; }
            }

            if (Cursor != null && selectedPos.HasValue)
            { 
                Cursor.Update(selectedPos.Value);
                Cursor.Draw(context);
            }
        }


        /// <summary>
        /// Updates the current state of the menu option based on
        /// the user input received.
        /// </summary>
        /// <param name="input">
        /// The <see cref="IMenuInputBridge"/> to process inputs from.
        /// </param>
        public void Update(IMenuInputBridge input)
        {
            if (input.Up == InputState.Pressed)
            {
                SelectedIndex = (SelectedIndex - 1 + Options.Count) % Options.Count;
            }
            else if (input.Down == InputState.Pressed)
            {
                SelectedIndex = (SelectedIndex + 1) % Options.Count;
            }
            else if (input.Left == InputState.Pressed)
            {
                Options[SelectedIndex].OnLeft();
            }
            else if (input.Right == InputState.Pressed)
            {
                Options[SelectedIndex].OnRight();
            }
            else if (input.Accept == InputState.Pressed)
            {
                Options[SelectedIndex].OnAccept();
            }
        }
    }
}