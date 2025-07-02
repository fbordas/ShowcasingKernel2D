namespace MonoGame.Kernel2D.Helpers
{
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
