using Microsoft.Xna.Framework;

namespace Kernel2D.Input
{
    /// <summary>
    /// Represents an abstraction for retrieving stateful input data in a
    /// menu context.
    /// </summary>
    public interface IMenuInputBridge : IInputBridge
    {
        /// <summary>
        /// Gets the current state of the "Up" input.
        /// </summary>
        InputState Up { get; }

        /// <summary>
        /// Gets the current state of the "Down" input.
        /// </summary>
        InputState Down { get; }

        /// <summary>
        /// Gets the current state of the "Left" input.
        /// </summary>
        InputState Left { get; }

        /// <summary>
        /// Gets the current state of the "Right" input.
        /// </summary>
        InputState Right { get; }

        /// <summary>
        /// Gets the current state of the "Accept" input, typically used for
        /// confirming selections or actions.
        /// </summary>
        InputState Accept { get; }

        /// <summary>
        /// Gets the current state of the "Cancel" input, typically used for
        /// going back or dismissing menus.
        /// </summary>
        InputState Cancel { get; }

        /// <summary>
        /// Gets the current position of the pointer (mouse or touch) in the
        /// menu context.
        /// </summary>
        Vector2? PointerPosition { get; }

        /// <summary>
        /// Gets whether the pointer is currently pressed down.
        /// </summary>
        bool PointerPressed { get; }

        /// <summary>
        /// Gets whether the pointer was released since the last input update.
        /// </summary>
        bool PointerReleased { get; }
    }
}