namespace Kernel2D.Input
{
    /// <summary>
    /// A container for managing instances of <see cref="IInputBridge"/>
    /// implementations. This allows for easy retrieval and management of input
    /// bridges throughout the application. Uses a dictionary to store instances
    /// by their type, ensuring that each type has a single instance and
    /// providing a method to retrieve or create instances as needed.
    /// </summary>
    public static class InputBridgeContainer
    {
        private readonly static Dictionary<Type, IInputBridge> _instances = [];

        /// <summary>
        /// Retrieves an instance of the specified input bridge type.
        /// If an instance does not already exist, it creates a new one.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the input bridge to retrieve. It must implement
        /// <see cref="IInputBridge"/> and have a parameterless constructor.
        /// </typeparam>
        /// <returns>An instance of the specified input bridge type.</returns>
        public static T Get<T>() where T : class, IInputBridge, new()
        {
            if (_instances.TryGetValue(typeof(T), out var instance))
            { return (T)instance; }

            var newInstance = new T();
            _instances[typeof(T)] = newInstance;
            return newInstance;
        }

        /// <summary>
        /// Manually registers an instance of an input bridge type.
        /// This is useful for cases where the input bridge cannot be created
        /// using the default constructor or when you want to provide a specific
        /// instance of an input bridge.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the input bridge to register. It must implement
        /// <see cref="IInputBridge"/> and be a class.
        /// </typeparam>
        /// <param name="instance">An instance of the input bridge to register.</param>
        public static void Register<T>(T instance) where T : class, IInputBridge =>
            _instances[typeof(T)] = instance;

        /// <summary>
        /// Clears all registered input bridge instances.
        /// Useful for soft resets.
        /// </summary>
        public static void Clear() => _instances.Clear();

        /// <summary>
        /// Checks if an input bridge of the specified type is registered.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the input bridge to check. It must implement
        /// <see cref="IInputBridge"/> and be a class.
        /// </typeparam>
        /// <returns>
        /// True if an instance of the specified input bridge type
        /// is registered, false otherwise.
        /// </returns>
        public static bool Has<T>() where T : class, IInputBridge =>
            _instances.ContainsKey(typeof(T));

        /// <summary>
        /// Attempts to retrieve an instance of the specified input bridge type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the input bridge to retrieve. It must implement
        /// <see cref="IInputBridge"/> and be a class.
        /// </typeparam>
        /// <param name="result">
        /// When this method returns, contains the instance of the specified
        /// input bridge type if it exists; otherwise, null.
        /// </param>
        /// <returns>
        /// True if an instance of the specified input bridge type
        /// was found; otherwise, false.
        /// </returns>
        public static bool TryGet<T>(out T? result) where T : class, IInputBridge
        {
            if (_instances.TryGetValue(typeof(T), out var instance))
            {
                result = (T)instance;
                return true;
            }
            result = null;
            return false;
        }
    }
}