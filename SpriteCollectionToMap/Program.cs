using System.Drawing;

namespace SpriteCollectionToMap
{
    internal class Program
    {
        static string images = string.Empty;
        static string origins = string.Empty;
        static string target = string.Empty;
        static List<Tuple<string, int, int>> imageDimensions = new List<Tuple<string, int, int>>();

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

            ReadImages(images, origins, target);
            ReadShoeboxFile(origins);
        }

        static void ReadImages(string imgs, string orgs, string tgt)
        {
            List<string> imageFiles = [.. Directory.GetFiles(imgs, "*.png")];
            foreach (string imageFile in imageFiles)
            {
                using (var fileStream = new FileStream(imageFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var img = Image.FromStream(fileStream, false, false))
                    {
                        imageDimensions.Add(new Tuple<string, int, int>(Path.GetFileName(imageFile), img.Width, img.Height));
                    }
                }
            }
        }

        static void ReadShoeboxFile(string file)
        { 
            string fileContents = File.ReadAllText(file);
            // parsing logic here
        }
    }

    internal class SpriteObject
    { 
        public string Name { get; private set; }
        public Rectangle SourceRectangle { get; private set; }

        public SpriteObject(string name, Rectangle sourceArea)
        { Name = name; SourceRectangle = sourceArea; }
    }
}
