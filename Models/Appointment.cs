using System.ComponentModel.DataAnnotations;

namespace LakshBeautyStudio.Models
{
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Completed,
        Cancelled
    }

    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a service")]
        public string Service { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a preferred date")]
        [DataType(DataType.Date)]
        public DateTime PreferredDate { get; set; }

        [Required(ErrorMessage = "Please select a preferred time")]
        public string PreferredTime { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Message { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
