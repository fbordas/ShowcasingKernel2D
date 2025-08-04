using Kernel2D.Input.Bridges.Menu;

namespace Kernel2D.Input.Bridges
{
    /// <summary>
    /// Factory class for creating input bridges.
    /// </summary>
    public static class InputBridgeFactory
    {
        private static readonly Dictionary<string, Type> _menuBridgeOverrides = [];

        /// <summary>
        /// Registers a custom input bridge type for a specific ID.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the input bridge to register. It must implement
        /// <see cref="IInputBridge"/> and have a parameterless constructor.
        /// </typeparam>
        /// <param name="id">
        /// The unique identifier for the input bridge. This ID is used
        /// to retrieve the bridge later.
        /// </param>
        public static void RegisterBridge<T>(string id) where T : class, IInputBridge, new() =>
            _menuBridgeOverrides[id] = typeof(T);

        /// <summary>
        /// Creates an instance of an input bridge based on the provided ID.
        /// </summary>
        /// <param name="id">
        /// The unique identifier for the input bridge to create.
        /// </param>
        /// <returns>
        /// An instance of <see cref="IInputBridge"/> corresponding to the
        /// specified ID. If no custom bridge is registered for the ID,
        /// the default <see cref="HIDMenuInputBridge"/> is returned.
        /// </returns>
        public static IInputBridge CreateBridge(string id = "default")
        {
            if (_menuBridgeOverrides.TryGetValue(id, out var type))
            {
                var instance = (IInputBridge)Activator.CreateInstance(type)!;
                InputBridgeContainer.Register((IInputBridge)instance); // optional
                return instance;
            }

            return InputBridgeContainer.Get<HIDMenuInputBridge>();
        }
    }
}