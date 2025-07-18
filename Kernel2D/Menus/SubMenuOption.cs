using Kernel2D.Drawing;
using Kernel2D.Input;
using Kernel2D.Screens;
using Kernel2D.Screens.ScreenTransitions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Menus
{
    /// <summary>
    /// Creates a menu option that will transition to a different screen upon
    /// selection/activation.
    /// </summary>
    public class SubMenuOption : MenuOption
    {
        private readonly ScreenTransitionBase? _transIn = null;
        private readonly ScreenTransitionBase? _transOut = null;
        private readonly ScreenTransitionPair? _transPair = null;
        private readonly ScreenBase _nextScreen;

        /// <summary>
        /// Creates a new menu option that will move the user to another screen, with optional
        /// screen transition effects.
        /// </summary>
        /// <param name="labelText">The text to display for the menu option.</param>
        /// <param name="nextScreen">The screen to change to.</param>
        /// <param name="transOut">(Optional) A <see cref="ScreenTransitionBase"/> effect to
        /// use when exiting the current screen.</param>
        /// <param name="transIn">(Optional) A <see cref="ScreenTransitionBase"/> effect to
        /// use when entering the next screen.</param>
        public SubMenuOption(string labelText, ScreenBase nextScreen,
            ScreenTransitionBase? transOut = null, ScreenTransitionBase ? transIn = null)
            : base(labelText)
        {
            if (transOut != null && transIn != null)
            {
                _transIn = transIn;
                _transOut = transOut;
                _transPair = new ScreenTransitionPair(transOut, transIn);
            }
            _nextScreen = nextScreen;
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
            float textScaling = 0.6f;
            var tdc = new TextDrawCommand
               (font, LabelText + " >>", Position, color, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0);
            context.DrawingQueue.Enqueue(tdc);
        }

        /// <summary>
        /// Triggers the screen change from the current menu screen to the next screen.
        /// </summary>
        public override void OnAccept()
        {
            if (_nextScreen == null) throw new
                    InvalidOperationException("SubMenuOption requires a screen to change to.");
            ScreenManager.Instance.ChangeScreen
                (_nextScreen!.ID, _nextScreen!.GetCurrentContent()!, _transPair);
        }

        /// <summary>
        /// Does nothing for this particular menu option.
        /// </summary>
        public override void OnLeft() { return; }

        /// <summary>
        /// Does nothing for this particular menu option.
        /// </summary>
        public override void OnRight() { return; }

        /// <summary>
        /// Does nothing for this particular menu option.
        /// </summary>
        /// <param name="input">Not applicable.</param>
        public override void Update(MenuInputBridge input) { return; }
    }
}