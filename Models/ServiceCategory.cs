using System.ComponentModel.DataAnnotations;

namespace LakshBeautyStudio.Models
{
    public class ServiceCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // e.g. "Hair Services"

        [StringLength(50)]
        public string? Icon { get; set; } // e.g. "💇" or a CSS icon class

        public int DisplayOrder { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
