using CleverAlbumDesigner.Exceptions;
using CleverAlbumDesigner.Managers;
using CleverAlbumDesigner.Models;
using CleverAlbumDesigner.Repositories.Interfaces;
using CleverAlbumDesigner.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using Color = CleverAlbumDesigner.Models.Color;
using SixLabors.ImageSharp.Processing;
using CleverAlbumUnitTests.Helpers;

namespace CleverAlbumUnitTests
{
    [TestClass]
    public class PhotoManagerUnitTests
    {
        private Mock<IPhotoRepository> _photoRepositoryMock = new Mock<IPhotoRepository>();
        private Mock<IStorageService> _storageServiceMock = new Mock<IStorageService>();
        private Mock<IColorRepository> _colorRepositoryMock = new Mock<IColorRepository>();
        private PhotoManager _photoManager;

        [TestInitialize]
        public void Initialize()
        {
            _photoManager = new PhotoManager(
                _photoRepositoryMock.Object,
                _storageServiceMock.Object,
                _colorRepositoryMock.Object
            );
        }

        [TestMethod]
        public async Task AddPhotoAsync_Should_Add_Photo()
        {
            // Arrange
            var photoId = Guid.NewGuid();
            var fileName = "test.jpg";
            var originalName = "original.jpg";
            var contentType = "image/jpeg";
            var sessionId = "test-session";
            var imageBytes = new byte[100]; 
            var fileStream = new MemoryStream(imageBytes);

            _storageServiceMock
                .Setup(s => s.UploadFileAsync(fileName, fileStream, contentType))
                .ReturnsAsync("https://mockurl.com/test.jpg");

            _colorRepositoryMock
                .Setup(c => c.GetAllColors())
                .ReturnsAsync(new List<Color>
                {
                    new Color { ColorId = Guid.NewGuid(), RgbaCode = "255,0,0,255" }
                });

            _photoRepositoryMock
                .Setup(p => p.AddPhotoAsync(It.IsAny<Photo>()))
                .Returns(Task.CompletedTask);           

            // Act       
            await _photoManager.AddPhotoAsync(SimulateImageHelper.SimulateImageStream(), photoId, fileName, originalName, contentType, sessionId);

            // Assert
            _photoRepositoryMock.Verify(p => p.AddPhotoAsync(It.IsAny<Photo>()), Times.Once);
        }    

        [TestMethod]
        public async Task GetAllUnassignedPhotosAsync_Should_Return_PhotoDtos()
        {
            // Arrange
            var sessionId = "test-session";
            Guid photo1Id = Guid.NewGuid();
            var photos = new List<Photo>
            {
                new Photo
                {
                    PhotoId = photo1Id,
                    FileName = "photo1.jpg",
                    Url="testurl",
                    SessionId=sessionId
                },
                new Photo
                {
                    PhotoId = Guid.NewGuid(),
                    FileName = "photo2.jpg",
                    Url="testurlphoto2",
                    SessionId=sessionId
                }
            };

            _photoRepositoryMock
                .Setup(p => p.GetAllUnassignedAsync(sessionId))
                .ReturnsAsync(photos);

            _storageServiceMock
                .Setup(s => s.GeneratePreSignedUrl(It.IsAny<string>(), 30))
                .Returns((string key, int expiration) => $"https://mockurl.com/{key}");

            // Act
            var result = await _photoManager.GetAllUnassignedPhotosAsync(sessionId);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(p => p.PreSignedUrl == $"https://mockurl.com/{photo1Id}-photo1.jpg"));
        }

        [TestMethod]
        public async Task DeletePhotoAsync_Should_Delete_Photo()
        {
            // Arrange
            var sessionId = "test-session";
            var photoId = Guid.NewGuid();
            var photo = new Photo
            {
                PhotoId = photoId,
                FileName = "photo.jpg",
                Url = "anothertesturlphoto2",
                SessionId = sessionId
            };

            _photoRepositoryMock
                .Setup(p => p.GetPhotoByIdAsync(photoId))
                .ReturnsAsync(photo);

            _storageServiceMock
                .Setup(s => s.DeleteFileAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _photoRepositoryMock
                .Setup(p => p.DeletePhotoAsync(photoId))
                .Returns(Task.CompletedTask);

            // Act
            await _photoManager.DeletePhotoAsync(photoId);

            // Assert
            _photoRepositoryMock.Verify(p => p.DeletePhotoAsync(photoId), Times.Once);
            _storageServiceMock.Verify(s => s.DeleteFileAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task DeletePhotoAsync_Should_Throw_Exception_When_Photo_Not_Found()
        {
            // Arrange
            var photoId = Guid.NewGuid();

            _photoRepositoryMock
                .Setup(p => p.GetPhotoByIdAsync(photoId))
                .ReturnsAsync((Photo?)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<OperationException>(() => _photoManager.DeletePhotoAsync(photoId));
        }
    }
}
