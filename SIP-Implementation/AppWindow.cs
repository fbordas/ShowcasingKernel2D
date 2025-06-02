using System.Reflection;
using System.Threading.Tasks.Sources;
using SpriteImageParser.Core;
using Exporter = SpriteImageParser.Core.SpriteRegionExporter;

namespace SIP_Implementation
{
    public partial class AppWindow : Form
    {
        public AppWindow()
        {
            InitializeComponent();
        }


        private void bLoad_Click(object sender, EventArgs e)
        {
            boxOpen.InitialDirectory = Assembly.GetEntryAssembly()?.Location;
            
            boxOpen.ShowDialog();
            if (boxOpen.FileName == "") { return; }

            spriteBox.Image = Image.FromFile(boxOpen.FileName);
            spriteBox.SizeMode = PictureBoxSizeMode.Normal;
            spriteBox.Size = spriteBox.Image.Size;

            ParseSpritesUsingSIP();
        }

        private void ParseSpritesUsingSIP()
        {
            listCols.Items.Clear();
            // Load image into local bitmap
            Bitmap bm = new(spriteBox.Image);

            // Convert to 2D pixel matrix
            Pixel[,] pixels = new Pixel[bm.Width, bm.Height];
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    Color pixelColor = bm.GetPixel(x, y);
                    pixels[x, y] = new Pixel(pixelColor.R, pixelColor.G, pixelColor.B, pixelColor.A);
                }
            }

            // Use SIP to detect sprite regions
            var regions = Parser.DetectSpritesInImage(pixels);
            if (regions.Count == 0)
            {
                lbFound.Text = "No sprites found in image.";
                return;
            }
            else
            {
                // If sprites were found, display their information
                lbFound.Text = $"{regions.Count} sprites found in image.";
                foreach (var region in regions)
                {
                    string[] values = {
                        region.X.ToString(),
                        region.Y.ToString(),
                        region.Width.ToString(),
                        region.Height.ToString()
                    };
                    ListViewItem item = new(values);
                    listCols.Items.Add(item);
                }

                // Serialize sprite regions to JSON and XML
                jsonOut.Text = Exporter.SerializeToJson(regions);
                xmlOut.Text = Exporter.SerializeToXml(regions);
            }
        }
    }
}
