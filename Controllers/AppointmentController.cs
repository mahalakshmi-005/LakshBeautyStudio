using LakshBeautyStudio.Data;
using LakshBeautyStudio.Models;
using LakshBeautyStudio.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LakshBeautyStudio.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Studio WhatsApp number in international format (no + or spaces)
        private const string StudioWhatsAppNumber = "919585885247";

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Appointment/Book
        [HttpGet]
        public IActionResult Book(string? service)
        {
            var model = new AppointmentViewModel
            {
                PreferredDate = DateTime.Today,
                Service = service ?? string.Empty // pre-fill from "Book This Service" quick buttons
            };
            return View(model);
        }

        // POST: /Appointment/Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(AppointmentViewModel model)
        {
            // Step 1: Standard data annotation validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Step 2: Business rule validation (past date, working hours)
            var businessErrors = model.ValidateBusinessRules();
            if (businessErrors.Any())
            {
                foreach (var error in businessErrors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(model);
            }

            // Step 3: Save to DB
            var appointment = new Appointment
            {
                Name = model.Name,
                Phone = model.Phone,
                Service = model.Service,
                PreferredDate = model.PreferredDate,
                PreferredTime = model.PreferredTime,
                Message = model.Message,
                Status = AppointmentStatus.Pending,
                CreatedAt = DateTime.Now
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Step 4: Build WhatsApp deep link with pre-filled message
            string whatsAppMessage =
                $"Hello, I would like to book an appointment.%0A" +
                $"Name: {appointment.Name}%0A" +
                $"Service: {appointment.Service}%0A" +
                $"Date: {appointment.PreferredDate:dd-MM-yyyy}%0A" +
                $"Time: {appointment.PreferredTime}";

            string whatsAppUrl = $"https://api.whatsapp.com/send?phone={StudioWhatsAppNumber}&text={whatsAppMessage}";

            // Step 5: Redirect to confirmation page, which auto-redirects to WhatsApp
            TempData["WhatsAppUrl"] = whatsAppUrl;
            TempData["CustomerName"] = appointment.Name;
            return RedirectToAction("Confirmation");
        }

        // GET: /Appointment/Confirmation
        [HttpGet]
        public IActionResult Confirmation()
        {
            ViewBag.WhatsAppUrl = TempData["WhatsAppUrl"] as string ?? $"https://api.whatsapp.com/send?phone={StudioWhatsAppNumber}";
            ViewBag.CustomerName = TempData["CustomerName"] as string ?? "there";
            return View();
        }
    }
}
