using LakshBeautyStudio.Data;
using LakshBeautyStudio.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LakshBeautyStudio.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var admin = await _context.AdminUsers.FirstOrDefaultAsync(a => a.Username == username);

            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });

            return RedirectToAction("Dashboard");
        }

        // GET: /Admin/Dashboard
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var today = DateTime.Today;

            ViewBag.TodayCount = await _context.Appointments.CountAsync(a => a.PreferredDate.Date == today);
            ViewBag.PendingCount = await _context.Appointments.CountAsync(a => a.Status == AppointmentStatus.Pending);
            ViewBag.MonthCount = await _context.Appointments.CountAsync(a => a.CreatedAt.Month == today.Month && a.CreatedAt.Year == today.Year);
            ViewBag.TotalServices = await _context.Services.CountAsync(s => s.IsActive);

            var recentAppointments = await _context.Appointments
                .OrderByDescending(a => a.CreatedAt)
                .Take(5)
                .ToListAsync();

            return View(recentAppointments);
        }

        // GET: /Admin/Appointments?status=Pending
        [Authorize]
        public async Task<IActionResult> Appointments(string? status)
        {
            var query = _context.Appointments.AsQueryable();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<AppointmentStatus>(status, out var parsedStatus))
            {
                query = query.Where(a => a.Status == parsedStatus);
            }

            var list = await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
            ViewBag.SelectedStatus = status;

            return View(list);
        }

        // POST: /Admin/UpdateStatus
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, AppointmentStatus status)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                appointment.Status = status;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Appointments");
        }

        // POST: /Admin/DeleteAppointment
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Appointments");
        }

        // GET: /Admin/Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // GET: /Admin/ChangePassword
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Admin/ChangePassword
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var username = User.Identity?.Name;
            var admin = await _context.AdminUsers.FirstOrDefaultAsync(a => a.Username == username);

            if (admin == null || !BCrypt.Net.BCrypt.Verify(currentPassword, admin.PasswordHash))
            {
                ViewBag.Error = "Current password is incorrect.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                ViewBag.Error = "New password must be at least 6 characters.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "New password and confirmation do not match.";
                return View();
            }

            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();

            ViewBag.Success = "Password changed successfully.";
            return View();
        }
    }
}
