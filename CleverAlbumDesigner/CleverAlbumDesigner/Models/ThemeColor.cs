using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleverAlbumDesigner.Models
{
    public class ThemeColor
    {       
        [Key]
        public Guid ThemeColorId { get; set; }

        [ForeignKey(nameof(Color))]
        public Guid ColorId { get; set; }
        public Color? Color { get; set; }

        [ForeignKey(nameof(Theme))]
        public Guid ThemeId { get; set; }
        public Theme? Theme { get; set; }
    }
}
