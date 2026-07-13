using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LakshBeautyStudio.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal StartingPrice { get; set; }

        public bool IsFeatured { get; set; } = false; // shows on homepage

        public bool IsActive { get; set; } = true;

        public string? ImagePath { get; set; } // thumbnail shown on service cards

        public int CategoryId { get; set; }
        public ServiceCategory? Category { get; set; }
    }
}
