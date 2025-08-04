using Kernel2D.Drawing;
using Kernel2D.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel2D.Menus
{
    /// <summary>
    /// Creates a new text-only menu option.
    /// </summary>
    public class LabelOption : MenuOption
    {
        private readonly Action? AcceptCallback = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="callback"></param>
        public LabelOption(string label, Action? callback = null) : base(label) 
        {
            if (callback != null) AcceptCallback = callback;
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public override void OnLeft() { return; }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public override void OnRight() { return; }

        /// <summary>
        /// When this option is selected, fires the callback action, if any.
        /// </summary>
        public override void OnAccept()
        { if (AcceptCallback != null) { AcceptCallback!.Invoke(); } }

        /// <summary>
        /// Draws the current menu option onscreen.
        /// </summary>
        /// <param name="context">The <see cref="DrawContext"/> to use to draw.</param>
        /// <param name="font">The <see cref="SpriteFont"/> to render the text with.</param>
        /// <param name="selected">Whether the current menu option is selected or not.</param>
        public override void Draw(DrawContext context, SpriteFont font, bool selected)
        {
            var color = selected ? SelectedColor : UnselectedColor;
            var textScaling = 0.6f;
            var tdc = new TextDrawCommand
                (font, LabelText, Position, color, 0, Vector2.Zero, textScaling, SpriteEffects.None, 0);
            context.DrawingQueue.Enqueue(tdc);
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="input">Not applicable.</param>
        public override void Update(IMenuInputBridge input) { return; }
    }
}