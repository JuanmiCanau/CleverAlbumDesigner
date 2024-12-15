using System.ComponentModel.DataAnnotations;

namespace CleverAlbumDesigner.Models
{
    public class Theme
    {
        [Key]
        public Guid ThemeId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

    }
}
