using System.ComponentModel.DataAnnotations;

namespace CleverAlbumDesigner.Models
{
    public class Color
    {
        [Key]
        public Guid ColorId { get; set; }
        public string Name { get; set; } = null!;
        public string RgbaCode { get; set; } = null!;
    }
}
