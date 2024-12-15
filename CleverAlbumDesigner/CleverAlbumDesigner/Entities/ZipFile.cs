namespace CleverAlbumDesigner.Entities
{
    public class ZipFile
    {
        public required string AlbumName { get; set; }
        public required byte[] Data { get; set; }
    }
}
