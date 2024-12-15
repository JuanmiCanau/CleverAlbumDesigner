using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleverAlbumDesigner.Models
{
    public class Photo
    {
        [Key]
        public Guid PhotoId { get; set; }
        [ForeignKey(nameof(Color))]
        public Guid? AlbumId { get; set; }        
        [Required]
        [Url]
        public required string Url { get; set; }
        public string? FileName { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey(nameof(Color))]
        public Guid? DominantColorId { get; set; }
        public required string SessionId { get; set; }
    }
}
