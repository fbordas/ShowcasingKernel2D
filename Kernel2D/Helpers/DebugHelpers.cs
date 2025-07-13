namespace Kernel2D.Helpers
{
    /// <summary>
    /// Helpers to dump debug messages into the debug console.
    /// </summary>
    public class DebugHelpers
    {
        /// <summary>
        /// Writes a message to the debug console followed by a line break.
        /// </summary>
        /// <param name="str">The message to write.</param>
        public static void WriteLine(string str) => System.Diagnostics.Debug.WriteLine(str);

        /// <summary>
        /// Writes a message to the debug console.
        /// </summary>
        /// <param name="str">The message to write.</param>
        public static void Write(string str) => System.Diagnostics.Debug.Write(str);
    }
}
