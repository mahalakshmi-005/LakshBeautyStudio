using System.ComponentModel.DataAnnotations;

namespace LakshBeautyStudio.Models
{
    public class FAQ
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Question { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Answer { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
