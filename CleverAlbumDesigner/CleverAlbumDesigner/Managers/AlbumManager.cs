using System.IO.Compression;
using System.Security.Cryptography.Xml;
using CleverAlbumDesigner.Entities;
using CleverAlbumDesigner.Exceptions;
using CleverAlbumDesigner.Managers.Interfaces;
using CleverAlbumDesigner.Models;
using CleverAlbumDesigner.Repositories.Interfaces;
using CleverAlbumDesigner.Services.Interfaces;

namespace CleverAlbumDesigner.Managers
{
    public class AlbumManager(
        IAlbumRepository albumRepository,
        IThemeRepository themeRepository,
        IThemeColorRepository themeColorRepository,
        IPhotoRepository photoRepository,
         IStorageService storageService) : IAlbumManager
    {
        private readonly IAlbumRepository _albumRepository = albumRepository;
        private readonly IThemeRepository _themeRepository = themeRepository;
        private readonly IThemeColorRepository _themeColorRepository = themeColorRepository;
        private readonly IPhotoRepository _photoRepository = photoRepository;
        private readonly IStorageService _storageService = storageService;

        public async Task<Album?> GenerateAlbumAsync(string themeName, string? suffix, string sessionId)
        {
            try
            {
                // Retrieve the theme
                var theme = await _themeRepository.GetThemeByNameAsync(themeName) ?? throw new Exception("Theme not found.");

                // Retrieve associated colors
                var colorIds = await _themeColorRepository.GetColorIdsByThemeIdAsync(theme.ThemeId);
                if (colorIds.Count == 0)
                    throw new Exception("No colors associated with the theme.");

                // Retrieve photos matching the colors
                var photos = await _photoRepository.GetPhotosByColorIdsAsync(colorIds, sessionId);
                if (photos.Count == 0)
                    return null;

                // Create the album
                var album = new Album
                {
                    AlbumId = Guid.NewGuid(),
                    Name = string.IsNullOrEmpty(suffix) ? $"{theme.Name}" : $"{theme.Name} - {suffix}",
                    Theme = theme,
                    CreatedAt = DateTime.UtcNow,
                    SessionId = sessionId
                };

                // Save the album
                await _albumRepository.AddAlbumAsync(album);

                // Assign photos to the album
                photos.ForEach(photo => photo.AlbumId = album.AlbumId);
                await _photoRepository.UpdatePhotosAsync(photos);
                return album;
            }
            catch (Exception ex)
            {
                throw new OperationException("There was an error while generating the album", ex);
            }
        }

        public async Task<List<Album>> GetAllAlbumsAsync(string sessionId)
        {
            try
            {
                return await _albumRepository.GetAllAlbumsAsync(sessionId);
            }
            catch (Exception ex)
            {
                throw new OperationException("There was an error while recovering the albums", ex);
            }
        }

        public async Task<List<PhotoDto>> GetAlbumPhotosAsync(Guid albumId)
        {
            try
            {
                var photos = await _photoRepository.GetPhotosByAlbumIdAsync(albumId);
                
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
                throw new OperationException("There was an error while recovering the album photos", ex);
            }
        }

        public async Task DeleteAlbumAsync(Guid albumId)
        {
            try
            {
                // Obtaining photos linked to the album
                var photos = await _photoRepository.GetPhotosByAlbumIdAsync(albumId);
                                
                if (photos.Count > 0)
                {
                    // Delete photos from Sql database
                    await _photoRepository.DeletePhotosByAlbumIdAsync(albumId);

                    // Delete photos from aws s3 bucket
                    var fileNames = photos.Select(photo => photo.Url).ToList(); 
                    await _storageService.DeleteFilesAsync(fileNames);
                }

                // delete album from sql
                await _albumRepository.DeleteAlbumAsync(albumId);
            }
            catch (Exception ex)
            {
                throw new OperationException("There was an error while deleting the album", ex);
            }
        }

        public async Task<Entities.ZipFile> GenerateAlbumZipAsync(Guid albumId)
        {          
            //Obtaining the album from the db
            var album = await _albumRepository.GetAlbumByIdAsync(albumId) ?? throw new OperationException("Album not found.");

            //Retrieving all linked photos from db
            var photos = await _photoRepository.GetPhotosByAlbumIdAsync(albumId);
            if (photos == null || photos.Count == 0)
                throw new OperationException("No photos available in the album.");
           
            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var photo in photos)
                {
                    //Retrieving all linked photos from s3 service
                    var photoStream = await _storageService.GetFileStreamAsync($"{photo.PhotoId}-{photo.FileName}"); 
                    if (photoStream == null) continue;

                    //Zipping photos
                    var zipEntry = archive.CreateEntry(photo.FileName!, CompressionLevel.Fastest);
                    using var zipStream = zipEntry.Open();
                    await photoStream.CopyToAsync(zipStream);
                }
            }

            //returning support entity holding both the zip name and the files
            return new Entities.ZipFile
            {
                AlbumName = album.Name.Replace(" ", "_"),
                Data = memoryStream.ToArray()
            };
        }
    }
}
