using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleverAlbumDesigner.Models
{
    public class Album
    {
        [Key]
        public Guid AlbumId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [ForeignKey(nameof(Theme))]
        public Guid ThemeId { get; set; }
        public Theme? Theme { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string SessionId { get; set; }
    }
}
