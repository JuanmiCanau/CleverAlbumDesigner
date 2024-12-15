using CleverAlbumDesigner.Managers;
using CleverAlbumDesigner.Repositories.Interfaces;
using CleverAlbumDesigner.Services.Interfaces;
using CleverAlbumDesigner.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CleverAlbumDesigner.Exceptions;

namespace CleverAlbumUnitTests
{
    [TestClass]
    public class AlbumManagerUnitTests
    {
        private Mock<IAlbumRepository> _albumRepositoryMock = new Mock<IAlbumRepository>();
        private Mock<IThemeRepository> _themeRepositoryMock = new Mock<IThemeRepository>();
        private Mock<IThemeColorRepository> _themeColorRepositoryMock = new Mock<IThemeColorRepository>();
        private Mock<IPhotoRepository> _photoRepositoryMock = new Mock<IPhotoRepository>();
        private Mock<IStorageService> _storageServiceMock = new Mock<IStorageService>();
        private AlbumManager _albumManager;

        [TestInitialize]
        public void Initialize()
        {
            _albumManager = new AlbumManager(
                _albumRepositoryMock.Object,
                _themeRepositoryMock.Object,
                _themeColorRepositoryMock.Object,
                _photoRepositoryMock.Object,
                _storageServiceMock.Object
            );
        }

        [TestMethod]
        public async Task GenerateAlbumAsync_Should_Create_Album_When_Photos_Exist()
        {
            // Arrange
            var themeName = "Nature";
            var suffix = "2023";
            var sessionId = "test-session";
            var themeId = Guid.NewGuid();
            var albumId = Guid.NewGuid();

            var theme = new Theme { ThemeId = themeId, Name = themeName };
            var colorIds = new List<Guid> { Guid.NewGuid() };
            var photos = new List<Photo>
            {
                new Photo { PhotoId = Guid.NewGuid(), FileName = "photo1.jpg", Url ="test",SessionId = sessionId },
                new Photo { PhotoId = Guid.NewGuid(), FileName = "photo2.jpg", Url ="test",SessionId = sessionId }
            };

            _themeRepositoryMock
                .Setup(repo => repo.GetThemeByNameAsync(themeName))
                .ReturnsAsync(theme);

            _themeColorRepositoryMock
                .Setup(repo => repo.GetColorIdsByThemeIdAsync(themeId))
                .ReturnsAsync(colorIds);

            _photoRepositoryMock
                .Setup(repo => repo.GetPhotosByColorIdsAsync(colorIds, sessionId))
                .ReturnsAsync(photos);

            _albumRepositoryMock
                .Setup(repo => repo.AddAlbumAsync(It.IsAny<Album>()))
                .Returns(Task.CompletedTask);

            _photoRepositoryMock
                .Setup(repo => repo.UpdatePhotosAsync(It.IsAny<List<Photo>>()))
                .Returns(Task.CompletedTask);

            // Act
            var album = await _albumManager.GenerateAlbumAsync(themeName, suffix, sessionId);

            // Assert
            Assert.IsNotNull(album);
            Assert.AreEqual($"{themeName} - {suffix}", album.Name);
            _albumRepositoryMock.Verify(repo => repo.AddAlbumAsync(It.IsAny<Album>()), Times.Once);
            _photoRepositoryMock.Verify(repo => repo.UpdatePhotosAsync(It.IsAny<List<Photo>>()), Times.Once);
        }

        [TestMethod]
        public async Task GenerateAlbumAsync_Should_Return_Null_When_No_Photos_Exist()
        {
            // Arrange
            var themeName = "Nature";
            var suffix = "2023";
            var sessionId = "test-session";
            var themeId = Guid.NewGuid();

            var theme = new Theme { ThemeId = themeId, Name = themeName };
            var colorIds = new List<Guid> { Guid.NewGuid() };

            _themeRepositoryMock
                .Setup(repo => repo.GetThemeByNameAsync(themeName))
                .ReturnsAsync(theme);

            _themeColorRepositoryMock
                .Setup(repo => repo.GetColorIdsByThemeIdAsync(themeId))
                .ReturnsAsync(colorIds);

            _photoRepositoryMock
                .Setup(repo => repo.GetPhotosByColorIdsAsync(colorIds, sessionId))
                .ReturnsAsync(new List<Photo>());

            // Act
            var album = await _albumManager.GenerateAlbumAsync(themeName, suffix, sessionId);

            // Assert
            Assert.IsNull(album);
            _albumRepositoryMock.Verify(repo => repo.AddAlbumAsync(It.IsAny<Album>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteAlbumAsync_Should_Delete_Album_And_Photos()
        {
            // Arrange
            var sessionId = "test-session";
            var albumId = Guid.NewGuid();
            var photos = new List<Photo>
            {
                new Photo { PhotoId = Guid.NewGuid(), FileName = "photo1.jpg" , Url ="test1",SessionId = sessionId},
                new Photo { PhotoId = Guid.NewGuid(), FileName = "photo2.jpg", Url ="test2",SessionId = sessionId }
            };

            _photoRepositoryMock
                .Setup(repo => repo.GetPhotosByAlbumIdAsync(albumId))
                .ReturnsAsync(photos);

            _photoRepositoryMock
                .Setup(repo => repo.DeletePhotosByAlbumIdAsync(albumId))
                .Returns(Task.CompletedTask);

            _storageServiceMock
                .Setup(service => service.DeleteFilesAsync(It.IsAny<List<string>>()))
                .Returns(Task.CompletedTask);

            _albumRepositoryMock
                .Setup(repo => repo.DeleteAlbumAsync(albumId))
                .Returns(Task.CompletedTask);

            // Act
            await _albumManager.DeleteAlbumAsync(albumId);

            // Assert
            _photoRepositoryMock.Verify(repo => repo.DeletePhotosByAlbumIdAsync(albumId), Times.Once);
            _albumRepositoryMock.Verify(repo => repo.DeleteAlbumAsync(albumId), Times.Once);
            _storageServiceMock.Verify(service => service.DeleteFilesAsync(It.IsAny<List<string>>()), Times.Once);
        }

        [TestMethod]
        public async Task GenerateAlbumZipAsync_Should_Return_Valid_ZipFile()
        {
            var sessionId = "test-session";
            // Arrange
            var albumId = Guid.NewGuid();
            var album = new Album { AlbumId = albumId, Name = "Test Album", SessionId = sessionId };
            var photos = new List<Photo>
            {
                new Photo { PhotoId = Guid.NewGuid(), FileName = "photo1.jpg" , Url ="test1",SessionId = sessionId},
                new Photo { PhotoId = Guid.NewGuid(), FileName = "photo2.jpg", Url ="test2",SessionId = sessionId }
            };

            var photoStream = new MemoryStream();
            await photoStream.WriteAsync(new byte[] { 1, 2, 3 });
            photoStream.Position = 0;

            _albumRepositoryMock
                .Setup(repo => repo.GetAlbumByIdAsync(albumId))
                .ReturnsAsync(album);

            _photoRepositoryMock
                .Setup(repo => repo.GetPhotosByAlbumIdAsync(albumId))
                .ReturnsAsync(photos);

            _storageServiceMock
                .Setup(service => service.GetFileStreamAsync(It.IsAny<string>()))
                .ReturnsAsync(photoStream);

            // Act
            var zipFile = await _albumManager.GenerateAlbumZipAsync(albumId);

            // Assert
            Assert.IsNotNull(zipFile);
            Assert.AreEqual("Test_Album", zipFile.AlbumName);
        }

        [TestMethod]
        public async Task GetAlbumPhotosAsync_Should_Return_PhotoDtos()
        {
            // Arrange
            var sessionId = "test-session";
            var albumId = Guid.NewGuid();
            var photo1Id = Guid.NewGuid();
            var photo2Id = Guid.NewGuid();
            var photos = new List<Photo>
            {
                new Photo { PhotoId = photo1Id, FileName = "photo1.jpg", Url="testurl1photo1.jpg", SessionId = sessionId },
                new Photo { PhotoId = photo2Id, FileName = "photo2.jpg", Url="testurl2photo2.jpg", SessionId = sessionId }
            };

            _photoRepositoryMock
                .Setup(repo => repo.GetPhotosByAlbumIdAsync(albumId))
                .ReturnsAsync(photos);

            _storageServiceMock
                .Setup(service => service.GeneratePreSignedUrl(It.IsAny<string>(), 30))
                .Returns((string fileName, int duration) => $"https://example.com/{fileName}");

            // Act
            var result = await _albumManager.GetAlbumPhotosAsync(albumId);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual($"https://example.com/{photo1Id}-photo1.jpg", result[0].PreSignedUrl);
            Assert.AreEqual($"https://example.com/{photo2Id}-photo2.jpg", result[1].PreSignedUrl);

            _photoRepositoryMock.Verify(repo => repo.GetPhotosByAlbumIdAsync(albumId), Times.Once);
            _storageServiceMock.Verify(service => service.GeneratePreSignedUrl(It.IsAny<string>(), 30), Times.Exactly(2));
        }

        [TestMethod]
        public async Task GetAllAlbumsAsync_Should_Return_Albums()
        {
            // Arrange
            var sessionId = "test-session";
            var albums = new List<Album>
            {
                new Album { AlbumId = Guid.NewGuid(), Name = "Album 1" ,SessionId = sessionId },
                new Album { AlbumId = Guid.NewGuid(), Name = "Album 2" ,SessionId = sessionId}
            };

            _albumRepositoryMock
                .Setup(repo => repo.GetAllAlbumsAsync(sessionId))
                .ReturnsAsync(albums);

            // Act
            var result = await _albumManager.GetAllAlbumsAsync(sessionId);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Album 1", result[0].Name);
            Assert.AreEqual("Album 2", result[1].Name);
            _albumRepositoryMock.Verify(repo => repo.GetAllAlbumsAsync(sessionId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationException))]
        public async Task GetAllAlbumsAsync_Should_Throw_OperationException_On_Error()
        {
            // Arrange
            var sessionId = "test-session";

            _albumRepositoryMock
                .Setup(repo => repo.GetAllAlbumsAsync(sessionId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            await _albumManager.GetAllAlbumsAsync(sessionId);
        }
    }
}
