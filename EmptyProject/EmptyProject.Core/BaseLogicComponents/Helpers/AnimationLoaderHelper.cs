using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using EmptyProject.Core.BaseLogicComponents.Animation;
using System.Reflection.Metadata.Ecma335;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace EmptyProject.Core.BaseLogicComponents
{
    public static class AnimationLoaderHelper
    {   
        internal static List<SpriteObject> GetSpritesFromJson(string path)
        {
            if (!File.Exists(path)) { throw new FileNotFoundException(); }
            try
            {
                var spriteMap = JsonSerializer.Deserialize<List<SpriteObject>>(File.ReadAllText(path));
                return spriteMap;
            }
            catch (Exception) { throw; }
        }

        internal static Spritesheet TranslateIntoDomainModel(List<SpriteObject> so, Texture2D playerTexture, string sheetName, AnimationTypes aniTypes)
        {
            var grouped = so.GroupBy(s => StripTrailingDigits(s.Name))
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(s => new AnimationFrame
                    {
                        Name = s.Name,
                        Duration = s.Duration,
                        SourceRectangle = new Rectangle(s.Frame.X, s.Frame.Y, s.Frame.Width, s.Frame.Height)
                    }).ToList()
                );

            var sheet = new Spritesheet
            {
                Texture = playerTexture,
                Name = sheetName,
                Animations = []
            };

            foreach (var (animationName, frameList) in grouped)
            {
                sheet.Animations[animationName] = new SpriteAnimation
                {
                    Name = animationName,
                    Frames = frameList,
                    Loop = true,
                    Tags = aniTypes
                };
            }
            return sheet;
        }

        private static string StripTrailingDigits(string name)
        {
            for (int i = name.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(name[i]))
                    return name[..(i + 1)];
            }
            return name;
        }
    }

    internal class SpriteObject
    {
        public string Name { get; set; }
        public SpriteFrame Frame { get; set; }
        public int Duration { get; set; }

        public SpriteObject(string name, Rectangle sourceArea, int duration)
        {
            Name = name;
            Duration = duration;
            Frame = new SpriteFrame(sourceArea.X, sourceArea.Y, sourceArea.Width, sourceArea.Height);
        }

        public SpriteObject() { }
    }

    internal class SpriteFrame
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public SpriteFrame(int x, int y, int width, int height)
        { X = x; Y = y; Width = width; Height = height; }

        public SpriteFrame() { }
    }
}
