using System.Reflection;
using System.Text.Json;

namespace Kernel2D.Helpers
{
    /// <summary>
    /// An utility class for loading JSON data from embedded resources in an assembly.
    /// </summary>
    internal static class EmbeddedJsonLoader
    {
        /// <summary>
        /// Loads a JSON object from an embedded resource in the specified assembly.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object to deserialize from the JSON data.
        /// </typeparam>
        /// <param name="resourcePath">
        /// The path to the embedded resource, which should be in the format
        /// "FolderPath.FileName".
        /// </param>
        /// <param name="assembly">
        /// The assembly containing the embedded resource. If null, the
        /// current executing assembly is used.
        /// </param>
        /// <returns>
        /// An instance of type <typeparamref name="T"/> deserialized from the JSON data.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the specified embedded resource cannot be found in the assembly.
        /// </exception>
        internal static T? LoadFromResource<T>(string resourcePath, Assembly? assembly = null)
        { 
            assembly ??= Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourcePath) ??
                throw new FileNotFoundException
                    ($"Embedded resource '{resourcePath}' not found in assembly '{assembly.FullName}'.");
            using var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Loads a JSON object from an embedded resource in the assembly of the specified anchor type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object to deserialize from the JSON data.
        /// </typeparam>
        /// <typeparam name="TAnchor">
        /// The anchor type whose assembly contains the embedded resource.
        /// </typeparam>
        /// <param name="resourcePath">
        /// The path to the embedded resource, which should be in the format
        /// "FolderPath.FileName". (No extensions or namespace.)
        /// </param>
        /// <returns>
        /// An instance of type <typeparamref name="T"/> deserialized from the JSON data.
        /// </returns>
        internal static T? LoadFromResource<T, TAnchor>(string resourcePath)
        {
            Assembly assembly = typeof(TAnchor).Assembly;
            return LoadFromResource<T>(resourcePath, assembly);
        }
    }
}