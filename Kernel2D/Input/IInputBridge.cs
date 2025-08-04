using Microsoft.Xna.Framework;

namespace Kernel2D.Input
{
    /// <summary>
    /// Represents an abstraction for retrieving stateful input data, enabling
    /// consistent input handling across different platforms and devices.
    /// </summary>
    public interface IInputBridge
    {
        /// <summary>
        /// Gets the state of the specified action input.
        /// </summary>
        /// <param name="action">The action whose input state is requested.</param>
        /// <returns>The current state of the specified action.</returns>
        InputState GetInputState(string action);

        /// <summary>
        /// Updates the input bridge, processing any input events and updating the state
        /// of the input actions. This method should be called once per frame to ensure
        /// the input state is current and reflects the latest user interactions.
        /// </summary>
        void Update();

        /// <summary>
        /// Retrieves a collection of all defined action names that this input bridge can handle.
        /// </summary>
        /// <returns>A collection of defined action names.</returns>
        IEnumerable<string>? GetDefinedActions();

        /// <summary>
        /// Gets the current position of the pointer (mouse or touch) in the game world.
        /// </summary>
        /// <returns>The current position of the pointer as a nullable Vector2.</returns>
        Vector2? GetPointerPosition();
    }
}