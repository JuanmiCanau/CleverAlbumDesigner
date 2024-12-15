using CleverAlbumDesigner.Entities;
using CleverAlbumDesigner.Exceptions;
using CleverAlbumDesigner.Managers.Interfaces;
using CleverAlbumDesigner.Models;
using CleverAlbumDesigner.Repositories.Interfaces;
using CleverAlbumDesigner.Services.Interfaces;
using ColorMine.ColorSpaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = CleverAlbumDesigner.Models.Color;

namespace CleverAlbumDesigner.Managers
{
    public class PhotoManager(IPhotoRepository photoRepository, IStorageService storageService, IColorRepository colorRepository) : IPhotoManager
    {
        private readonly IPhotoRepository _photoRepository = photoRepository;
        private readonly IStorageService _storageService = storageService;
        private readonly IColorRepository _colorRepository = colorRepository;

        public async Task AddPhotoAsync(Stream fileStream, Guid photoId, string fileName, string originalName, string contentType, string sessionId)
        {
            try
            {
                //Retrieving the dominant color of the uploaded photo. We are calculating the average red, green and blue values re
                var dominantColorRgba32 = GetDominantColor(fileStream);

                //Photos are stored in a separate repository, an AWS bucket, so
                // this step ensures photos are private and only the user can access them.
                var url = await _storageService.UploadFileAsync(fileName, fileStream, contentType);

                var photo = new Photo
                {
                    PhotoId = photoId,
                    Url = url,
                    UploadedAt = DateTime.UtcNow,
                    FileName = originalName,
                    DominantColorId = FindClosestColorId(dominantColorRgba32, await _colorRepository.GetAllColors()),
                    SessionId = sessionId
                };

                //uploading file to the db
                await _photoRepository.AddPhotoAsync(photo);
            }
            catch (Exception ex)
            {
                throw new OperationException("Failed to add the photo.", ex);
            }
        }      

        public async Task<List<PhotoDto>> GetAllUnassignedPhotosAsync(string sessionId)
        {
            try
            {
                //retrieve photos from db
                var photos = await _photoRepository.GetAllUnassignedAsync(sessionId);

                // Generating pre signed url per photo in order to retrieve them from the private bucket
                var photoDtos = new List<PhotoDto>();

                foreach (var photo in photos)
                {
                    var signedUrl = _storageService.GeneratePreSignedUrl($"{photo.PhotoId}-{photo.FileName}", 30);

                    photoDtos.Add(new PhotoDto
                    {
                        PhotoId = photo.PhotoId,
                        PreSignedUrl = signedUrl
                    });
                }

                return photoDtos;

            }
            catch (Exception ex)
            {
                throw new OperationException("Failed to retrieve photos.", ex);
            }
        }

        public async Task DeletePhotoAsync(Guid photoId)
        {
            try
            {
                var photo = await _photoRepository.GetPhotoByIdAsync(photoId) ?? throw new OperationException("Photo not found.");

                //deleting from s3
                await _storageService.DeleteFileAsync($"{photo.PhotoId}-{photo.FileName}");
                //deleting from db
                await _photoRepository.DeletePhotoAsync(photoId);
            }
            catch (Exception ex)
            {
                throw new OperationException("Failed to delete the photo.", ex);
            }
        }

        private static Rgba32 GetDominantColor(Stream fileStream)
        {
            using var image = Image.Load<Rgba32>(fileStream);

            // Variables to calculate the average color
            long totalR = 0, totalG = 0, totalB = 0;
            int pixelCount = image.Width * image.Height;

            // Iterate through the entire image
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image[x, y];
                    totalR += pixel.R;
                    totalG += pixel.G;
                    totalB += pixel.B;
                }
            }

            // Calculate the average RGB values
            return new Rgba32(
                (byte)(totalR / pixelCount),
                (byte)(totalG / pixelCount),
                (byte)(totalB / pixelCount)
            );
        }

        private static Guid? FindClosestColorId(Rgba32 dominantColor, List<Color> colors)
        {
            Guid? closestColorId = null;
            double minDistance = double.MaxValue;

            foreach (var color in colors)
            {
                // Convert RgbaCode from database to Rgba32
                var rgbaValues = color.RgbaCode.Split(',').Select(byte.Parse).ToArray();
                var dbColor = new Rgba32(rgbaValues[0], rgbaValues[1], rgbaValues[2]);

                // Calculate the distance to check what is the closest color we have stored in the dd using the Euclidean color distance
                var distance = CalculateColorDistanceLab(dominantColor, dbColor);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestColorId = color.ColorId;
                }
            }

            return closestColorId;
        }

        private static double CalculateColorDistanceLab(Rgba32 color1, Rgba32 color2)
        {
            // Convert Rgba32 to LAB using ColorMine
            var lab1 = new ColorMine.ColorSpaces.Rgb { R = color1.R, G = color1.G, B = color1.B }.To<Lab>();
            var lab2 = new ColorMine.ColorSpaces.Rgb { R = color2.R, G = color2.G, B = color2.B }.To<Lab>();

            // Calculate the perceptual distance between two LAB colors
            return Math.Sqrt(
                Math.Pow(lab1.L - lab2.L, 2) +
                Math.Pow(lab1.A - lab2.A, 2) +
                Math.Pow(lab1.B - lab2.B, 2)
            );
        }
    }
}
