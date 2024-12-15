using CleverAlbumDesigner.Exceptions;
using CleverAlbumDesigner.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleverAlbumDesigner.Controllers
{
    [ApiController]
    [Route("api/photo")]
    public class PhotoController(IPhotoManager photoManager) : ControllerBase
    {
        private readonly IPhotoManager _photoManager = photoManager;

        //Endpoint to upload photos. The sessionid is being send from
        //FE to ensure photos only stay during the current session
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFile file, [FromHeader] string sessionId)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("File is missing.");

                var photoId = Guid.NewGuid();
                var fileName = $"{photoId}-{file.FileName}";
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    await _photoManager.AddPhotoAsync(stream, photoId, fileName, file.FileName, file.ContentType, sessionId);
                }

                return Ok("File uploaded successfully.");
            }
            catch (OperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred.{ex.Message}");
            }
        }

        //Endpoint to retrieve all photos without an album. The sessionid is being send from
        //FE to ensure photos will only be retrieved in the current session
        [HttpGet("allunassigned")]
        public async Task<IActionResult> GetAllUnassignedPhotos([FromHeader] string sessionId)
        {
            try
            {
                var photos = await _photoManager.GetAllUnassignedPhotosAsync(sessionId);
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

        //Endpoint to delete a photo
        [HttpDelete("delete/{photoId}")]
        public async Task<IActionResult> DeletePhoto(Guid photoId)
        {
            try
            {
                await _photoManager.DeletePhotoAsync(photoId);
                return Ok("Photo deleted successfully.");
            }
            catch (OperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred.{ex.Message}");
            }
        }
    }
}
