
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CleverAlbumUnitTests.Helpers
{
    public static class SimulateImageHelper
    {
        public static MemoryStream SimulateImageStream()
        {
            var memoryStream = new MemoryStream();

            using (var image = new Image<Rgba32>(100, 100)) // Create a 100x100 image
            {
                // Fill the image with a solid color
                image.Mutate(ctx => ctx.BackgroundColor(SixLabors.ImageSharp.Color.Red));

                // Save the image as a PNG to the memory stream
                image.SaveAsPng(memoryStream);
            }

            // Reset the stream position to the beginning for reading
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
