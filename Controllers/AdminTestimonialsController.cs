using LakshBeautyStudio.Data;
using LakshBeautyStudio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LakshBeautyStudio.Controllers
{
    [Authorize]
    public class AdminTestimonialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminTestimonialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /AdminTestimonials/Index
        public async Task<IActionResult> Index()
        {
            var testimonials = await _context.Testimonials.OrderByDescending(t => t.CreatedAt).ToListAsync();
            return View(testimonials);
        }

        // POST: /AdminTestimonials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string customerName, int rating, string reviewText)
        {
            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(reviewText))
            {
                TempData["Error"] = "Customer name and review text are required.";
                return RedirectToAction("Index");
            }

            _context.Testimonials.Add(new Testimonial
            {
                CustomerName = customerName,
                Rating = Math.Clamp(rating, 1, 5),
                ReviewText = reviewText,
                IsActive = true,
                CreatedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Testimonial added.";
            return RedirectToAction("Index");
        }

        // POST: /AdminTestimonials/ToggleActive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                testimonial.IsActive = !testimonial.IsActive;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // POST: /AdminTestimonials/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Testimonial deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}
