using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Kernel2D.Helpers
{
    /// <summary>
    /// Provides helper methods for working with textures in a 2D game.
    /// </summary>
    public class TextureHelpers
    {
        private static Texture2D _whitePixel;

        /// <summary>
        /// Gets a white pixel texture that can be used for drawing purposes.
        /// </summary>
        /// <param name="device">
        /// The <see cref="GraphicsDevice"/> used to create the texture.
        /// </param>
        /// <returns>
        /// A <see cref="Texture2D"/> representing a single white pixel.
        /// </returns>
        public static Texture2D WhitePixel(GraphicsDevice device)
        {
            if (_whitePixel == null || _whitePixel.IsDisposed)
            {
                _whitePixel = new Texture2D(device, 1, 1);
                _whitePixel.SetData(new[] { Color.White });
            }
            return _whitePixel;
        }
    }
}