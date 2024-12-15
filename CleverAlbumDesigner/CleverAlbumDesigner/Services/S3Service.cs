using Amazon.S3.Model;
using Amazon.S3;
using CleverAlbumDesigner.Services.Interfaces;
using CleverAlbumDesigner.Exceptions;

namespace CleverAlbumDesigner.Services
{
    public class S3Service(IAmazonS3 s3Client, string bucketName) : IStorageService
    {
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketName = bucketName;

        //uploading the images to s3 buckets
        public async Task<string> UploadFileAsync(string fileName, Stream fileStream, string contentType)
        {
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = contentType,
                CannedACL = S3CannedACL.Private,
                AutoCloseStream = false
            };

            try
            {
                await _s3Client.PutObjectAsync(request);
                return $"https://{_bucketName}.s3.{Amazon.RegionEndpoint.EUNorth1.SystemName}.amazonaws.com/{fileName}";
            }
            catch (AmazonS3Exception ex)
            {
                throw new OperationException($"Error while uploading file to S3: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new OperationException("Unexpected error while uploading file to S3.", ex);
            }
        }

        //signing the urls to allow access from the solution
        public string GeneratePreSignedUrl(string fileName, int expirationInMinutes = 30)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes)
            };

            try
            {
                return _s3Client.GetPreSignedURL(request);
            }
            catch (AmazonS3Exception ex)
            {
                throw new OperationException($"Error while generating signed URL: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new OperationException("Unexpected error while generating signed URL.", ex);
            }
        }

        //deleting file from s3
        public async Task DeleteFileAsync(string fileName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };
            try
            {
                await _s3Client.DeleteObjectAsync(request);
            }
            catch (AmazonS3Exception ex)
            {
                throw new OperationException($"Error while removing S3 file: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new OperationException("Unexpected error hile removing S3 file", ex);
            }
        }

        //deleting files from s3
        public async Task DeleteFilesAsync(List<string> fileNames)
        {
            try
            {
                var deleteObjectsRequest = new DeleteObjectsRequest
                {
                    BucketName = _bucketName,
                    Objects = fileNames.Select(fileName => new KeyVersion { Key = fileName }).ToList()
                };
                await _s3Client.DeleteObjectsAsync(deleteObjectsRequest);
            }
            catch (AmazonS3Exception ex)
            {
                throw new OperationException($"Error while deleting S3 files: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new OperationException("Unexpected error while deleting S3 files", ex);
            }            
        }

        //Retrieving Stream of images from s3
        public async Task<Stream> GetFileStreamAsync(string key)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                var response = await _s3Client.GetObjectAsync(request);
                return response.ResponseStream;
            }
            catch (AmazonS3Exception ex)
            {
                throw new OperationException($"Error retrieving file from S3: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new OperationException("Unexpected error while retrieving file from S3", ex);
            }
        }
    }
}
