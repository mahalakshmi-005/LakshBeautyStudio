using System.ComponentModel.DataAnnotations;

namespace LakshBeautyStudio.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; } = string.Empty; // relative path under wwwroot

        [StringLength(150)]
        public string? Caption { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public string? PublicId { get; set; }
    }
}
