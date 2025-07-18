using Kernel2D.Drawing;
using Kernel2D.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Menus
{
    /// <summary>
    /// A representation of a menu option in a menu screen.
    /// </summary>
    public abstract class MenuOption
    {
        /// <summary>
        /// The position at which to draw the menu option in the
        /// viewport.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The color to draw the text for the selected menu option with.
        /// </summary>
        protected Color SelectedColor = Color.Yellow;

        /// <summary>
        /// The color to draw the text for when an option is not selected.
        /// </summary>
        protected Color UnselectedColor = Color.White;

        /// <summary>
        /// The identifier for the current menu option.
        /// </summary>
        public virtual string ID => GetType().Name;

        /// <summary>
        /// The label text for the menu option.
        /// </summary>
        public string LabelText { get; protected set; }

        /// <summary>
        /// Creates a new menu option with the specified label text.
        /// </summary>
        /// <param name="labelText">The label text to use for the menu option.</param>
        public MenuOption(string labelText) => LabelText = labelText;

        /// <summary>
        /// Draws the current menu option using the provided <see cref="DrawContext"/>
        /// and <see cref="SpriteFont"/>, and determines whether the option is currently
        /// selected in the current menu.
        /// </summary>
        /// <param name="context">
        /// The <see cref="DrawContext"/> to draw the menu option with.
        /// </param>
        /// <param name="font">
        /// The <see cref="SpriteFont"/> to render the menu option with.
        /// </param>
        /// <param name="selected">Whether the current menu option is selected
        /// or not.</param>
        public abstract void Draw(DrawContext context, SpriteFont font, bool selected);

        /// <summary>
        /// What to do when registering a left directional input.
        /// </summary>
        public abstract void OnLeft();

        /// <summary>
        /// What to do when registering a right directional input.
        /// </summary>
        public abstract void OnRight();

        /// <summary>
        /// What to do when registering an Accept action input.
        /// </summary>
        public abstract void OnAccept();

        /// <summary>
        /// How to update the menu option based on any changes
        /// applied to it depending on its type.
        /// </summary>
        /// <param name="input">
        /// The <see cref="MenuInputBridge"/> to interpret and apply
        /// directional and action inputs from.
        /// </param>
        public abstract void Update(MenuInputBridge input);

        /// <summary>
        /// The colors to render the options with when selected and
        /// not selected.
        /// </summary>
        /// <param name="selected">The color to render the menu option
        /// when selected.</param>
        /// <param name="unselected">The color to render the menu
        /// option when not selected.</param>
        public void LabelColor(Color selected, Color unselected)
        {
            SelectedColor = selected;
            UnselectedColor = unselected;
        }
    }
}