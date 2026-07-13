using System.ComponentModel.DataAnnotations;

namespace LakshBeautyStudio.ViewModels
{
    public class AppointmentViewModel
    {
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
        public DateTime PreferredDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Please select a preferred time")]
        public string PreferredTime { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Message { get; set; }

        // ---------- Custom business-rule validation ----------
        // Called manually in controller (keeps model reusable / testable)
        public List<string> ValidateBusinessRules()
        {
            var errors = new List<string>();

            // Rule 1: No past dates
            if (PreferredDate.Date < DateTime.Today)
            {
                errors.Add("Preferred date cannot be in the past.");
            }

            // Rule 2: Time must fall within working hours (9AM-2PM or 3PM-7PM)
            // PreferredTime expected format "HH:mm" (24-hour), e.g. "10:30"
            if (TimeSpan.TryParse(PreferredTime, out var time))
            {
                var morningStart = new TimeSpan(9, 0, 0);
                var morningEnd = new TimeSpan(14, 0, 0);   // 2 PM
                var eveningStart = new TimeSpan(15, 0, 0); // 3 PM
                var eveningEnd = new TimeSpan(19, 0, 0);   // 7 PM

                bool inMorningSlot = time >= morningStart && time <= morningEnd;
                bool inEveningSlot = time >= eveningStart && time <= eveningEnd;

                if (!inMorningSlot && !inEveningSlot)
                {
                    errors.Add("Please select a time within working hours: 9 AM–2 PM or 3 PM–7 PM.");
                }
            }
            else
            {
                errors.Add("Invalid time format.");
            }

            return errors;
        }
    }
}
