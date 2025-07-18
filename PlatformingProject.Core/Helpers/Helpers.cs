using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformingProject.Core.Helpers
{
    public static class Helpers
    {
        public static Matrix GetScreenScaleMatrix(GraphicsDevice device, int baseWidth, int baseHeight)
        {
            var actualWidth = device.Viewport.Width;
            var actualHeight = device.Viewport.Height;

            float scaleX = (float)actualWidth / baseWidth;
            float scaleY = (float)actualHeight / baseHeight;

            float scale = Math.Min(scaleX, scaleY);

            return Matrix.CreateScale(scale, scale, 1f);
        }
    }
}
