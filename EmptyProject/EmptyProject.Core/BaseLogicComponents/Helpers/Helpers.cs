using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Drawing;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace EmptyProject.Core.BaseLogicComponents
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    public static class Helpers
    {
        [Flags]
        public enum AnimationTypes
        {
            Interactable,
            Looping,
            Interruptible,
            FacingRight,
            Unskippable,
            Grounded,
            Airborne,
            Climbing,
            WallSliding,
            Dashing
        }

        internal static List<SpriteObject> GetSpritesFromJson(string path)
        {
            if (!File.Exists(path)) { throw new FileNotFoundException(); }
            try
            {
                var spriteMap = JsonSerializer.Deserialize<List<SpriteObject>>(File.ReadAllText(path));
                return spriteMap;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    internal record SpriteObject
    {
        public string Name { get; private set; }
        public SpriteFrame Frame { get; private set; }

        public SpriteObject(string name, Rectangle sourceArea)
        {
            Name = name;
            Frame = new SpriteFrame(sourceArea.X, sourceArea.Y, sourceArea.Width, sourceArea.Height);
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
