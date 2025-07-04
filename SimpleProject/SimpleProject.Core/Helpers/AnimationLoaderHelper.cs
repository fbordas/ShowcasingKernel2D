using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using K2D = MonoGame.Kernel2D.Animation;

namespace PlatformerGameProject.Core.Helpers
{
    public static class AnimationLoaderHelper
    {
        internal static List<SpriteGroup> GetSpritesFromJson(string path)
        {
            if (!File.Exists(path)) { throw new FileNotFoundException(); }
            try
            {
                var spriteMap = JsonSerializer.Deserialize<List<SpriteGroup>>(File.ReadAllText(path));
                return spriteMap;
            }
            catch (Exception) { throw; }
        }

        internal static K2D.Spritesheet TranslateIntoDomainModel(List<SpriteGroup> rows, Texture2D playerTexture,
            string sheetName)
        {
            var sheet = new K2D.Spritesheet
            {
                Texture = playerTexture,
                Name = sheetName,
                Animations = []
            };

            foreach (var row in rows)
            {
                var frameList = row.Frames.Select(s => new K2D.AnimationFrame
                {
                    Name = s.Name,
                    Duration = s.Duration,
                    SourceRectangle = new Rectangle(s.Frame.X, s.Frame.Y, s.Frame.Width, s.Frame.Height)
                }).ToList();

                sheet.Animations[row.Name] = new K2D.SpriteAnimation
                {
                    Name = row.Name,
                    Frames = frameList,
                    Loop = row.Loop
                };
            }
            return sheet;
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

    internal class SpriteGroup
    {
        public string Name { get; set; }
        public List<SpriteObject> Frames { get; set; }
        public bool Loop { get; set; } = true; // Default to true if not specified
        public SpriteGroup(string name, List<SpriteObject> frames)
        { Name = name; Frames = frames; }
        public SpriteGroup() { }
    }
}
