using Microsoft.Xna.Framework;

namespace PlatformerGameProject.Core.Helpers
{
    public static class Helpers
    {
        internal record SpriteObject
        {
            public string Name { get; private set; }
            public SpriteFrame Frame { get; private set; }

            public SpriteObject(string name, Rectangle sourceArea)
            {
                Name = name;
                Frame = new SpriteFrame(sourceArea.X, sourceArea.Y,
                    sourceArea.Width, sourceArea.Height);
            }
        }

        internal class SpriteFrame
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }

            public SpriteFrame(int x, int y, int width, int height)
            { X = x; Y = y; Width = width; Height = height; }
        }
    }
}
