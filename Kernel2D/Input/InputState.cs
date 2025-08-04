namespace Kernel2D.Input
{
    /// <summary>
    /// Describes the current state of a given input.
    /// </summary>
    public enum InputState
    {
        /// <summary>
        /// Represents a state where no specific value or option is selected.
        /// </summary>
        /// <remarks>This value is typically used as a default or placeholder to indicate
        /// the absence of a meaningful selection.</remarks>
        None,
        /// <summary>
        /// The state where the input is currently being pressed down in the current frame.
        /// </summary>
        Pressed,
        /// <summary>
        /// The state where the input was pressed down in the previous frame,
        /// and continues to be held down in the current frame.
        /// </summary>
        Held,
        /// <summary>
        /// The state where the input was pressed down in the previous frame,
        /// but has been released in the current frame.
        /// </summary>
        Released
    }
}