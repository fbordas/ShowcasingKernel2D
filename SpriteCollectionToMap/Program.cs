using System.Drawing;
using System.Text.Json;

namespace SpriteCollectionToMap
{
    internal class Program
    {
        static string images = string.Empty;
        static string origins = string.Empty;
        static string target = string.Empty;
        static Dictionary<string, (int width, int height)> imageDimensions = new();
        static List<SpriteObject> spriteObjects = new List<SpriteObject>();
        const int ShoeboxYoffset = 37;

        static void Main(string[] args)
        {
            while (string.IsNullOrWhiteSpace(images) || !Directory.Exists(images))
            {
                Console.WriteLine("Enter path to sprite collection folder:");
                images = (Console.ReadLine() ?? "").Trim();
            }

            while (string.IsNullOrWhiteSpace(origins) || !File.Exists(origins))
            {
                Console.WriteLine("Enter path to Shoebox output data file:");
                origins = (Console.ReadLine() ?? "").Trim();
            }

            while (string.IsNullOrWhiteSpace(target) || !Directory.Exists(target))
            {
                Console.WriteLine("Enter path to folder in which to place current output:");
                target = (Console.ReadLine() ?? "").Trim();
            }

            ReadImages(images);
            ReadShoeboxFile(origins);
            ExportJson(target);
        }

        static void ReadImages(string imgs)
        {
            List<string> imageFiles = [.. Directory.GetFiles(imgs, "*.png")];
            foreach (string imageFile in imageFiles)
            {
                using var fileStream =
                    new FileStream(imageFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var img = Image.FromStream(fileStream, false, false);
                imageDimensions[Path.GetFileName(imageFile)] = (img.Width, img.Height);
            }
        }

        static void ReadShoeboxFile(string file)
        {
            var fileContents = File.ReadAllLines(file);
            foreach (var line in fileContents)
            {
                var spriteLocation = line.Split(' ');
                if (!imageDimensions.TryGetValue(spriteLocation[0], out var spriteDimensions))
                {
                    Console.WriteLine
                        ($"Warning: no dimensions found for {spriteLocation[0]}; continuing...");
                    continue;
                }
                Rectangle spriteRectangle = new
                    (int.Parse(spriteLocation[1]), int.Parse(spriteLocation[2]) + ShoeboxYoffset,
                    spriteDimensions.width, spriteDimensions.height);
                spriteObjects.Add(new(spriteLocation[0], spriteRectangle));
            }
        }

        static void ExportJson(string tgt)
        {
            spriteObjects = [.. spriteObjects.OrderBy(s => s.Name)];
            string json = JsonSerializer.Serialize(spriteObjects,
                new JsonSerializerOptions { WriteIndented = true });

            string outputPath = Path.Combine(tgt, "spriteMap.json");
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Export complete: {outputPath}");
        }
    }

    internal record SpriteObject
    {
        public string Name { get; private set; }
        public Rectangle SourceRectangle { get; private set; }

        public SpriteObject(string name, Rectangle sourceArea)
        { Name = name; SourceRectangle = sourceArea; }
    }
}
