using CleverAlbumDesigner.Exceptions;
using CleverAlbumDesigner.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleverAlbumDesigner.Controllers
{
    [ApiController]
    [Route("api/albums")]
    public class AlbumController(IAlbumManager albumManager) : ControllerBase
    {
        private readonly IAlbumManager _albumManager = albumManager;

        //Endpoint to generate albums based on given theme. Users can send an album name (suffix) and we are sending the sessionid
        //from FE to ensure albums only stay during the current session
        [HttpPost("generate/{themeName}")]
        public async Task<IActionResult> GenerateAlbum(string themeName, [FromQuery] string? suffix, [FromHeader] string sessionId)
        {
            try
            {
                var album = await _albumManager.GenerateAlbumAsync(themeName, suffix, sessionId);

                if (album == null)
                    return Ok(new { Message = "No album was created. There are no photos matching the selected theme." });

                return Ok(new { Message = "Album created successfully" });
            }
            catch (OperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        //Endpoint to retrieve all the albums based on the sessionid
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAlbums([FromHeader] string sessionId)
        {
            try
            {
                var albums = await _albumManager.GetAllAlbumsAsync(sessionId);
                return Ok(albums);
            }
            catch (OperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        //Endpoint to retrieve all the photos of a specific album
        [HttpGet("{albumId}/photos")]
        public async Task<IActionResult> GetAlbumPhotos(Guid albumId)
        {
            try
            {
                var photos = await _albumManager.GetAlbumPhotosAsync(albumId);
                if (photos == null || photos.Count == 0)
                {
                    return NotFound("No photos found for this album.");
                }
                return Ok(photos);
            }
            catch (OperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        //Endpoint to delete an album and all its related photos
        [HttpDelete("{albumId}")]
        public async Task<IActionResult> DeleteAlbum(Guid albumId)
        {
            try
            {
                await _albumManager.DeleteAlbumAsync(albumId);
                return Ok("Album and its photos have been deleted.");
            }
            catch (OperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        //Endpoint to download all photos from the album
        [HttpGet("download/{albumId}")]
        public async Task<IActionResult> DownloadAlbum(Guid albumId)
        {
            try
            {
                var zipFile = await _albumManager.GenerateAlbumZipAsync(albumId);

                if (zipFile == null)
                    return NotFound("The album does not exist or has no photos.");

                var fileName = $"{zipFile.AlbumName}.zip";

                return File(zipFile.Data, "application/zip", fileName);
            }
            catch (OperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
