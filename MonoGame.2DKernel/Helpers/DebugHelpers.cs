namespace MonoGame.Kernel2D.Helpers
{
    public class DebugHelpers
    {
        /// <summary>
        /// Writes a message to the debug console.
        /// </summary>
        /// <param name="str">The message to write.</param>
        public static void DebugMessage(string str) => System.Diagnostics.Debug.WriteLine(str);
    }
}
