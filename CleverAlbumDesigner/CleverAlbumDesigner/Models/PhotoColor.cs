using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleverAlbumDesigner.Models
{
    public class PhotoColor
    {
        [Key]
        public Guid PhotoColorId { get; set; }

        [ForeignKey(nameof(Photo))]
        public Guid PhotoId { get; set; }

        [ForeignKey(nameof(Color))]
        public Guid ColorId { get; set; }
    }
}
