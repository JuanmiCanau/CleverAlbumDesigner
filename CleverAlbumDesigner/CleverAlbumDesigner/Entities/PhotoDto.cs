namespace CleverAlbumDesigner.Entities
{
    public class PhotoDto
    {
        public Guid PhotoId { get; set; }
        public required string PreSignedUrl { get; set; }
    }
}
