using LakshBeautyStudio.Data;
using LakshBeautyStudio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LakshBeautyStudio.Controllers
{
    [Authorize]
    public class AdminServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------------- SERVICES ----------------

        // GET: /AdminServices/Index
        public async Task<IActionResult> Index()
        {
            var services = await _context.Services
                .Include(s => s.Category)
                .OrderBy(s => s.CategoryId).ThenBy(s => s.Name)
                .ToListAsync();

            ViewBag.Categories = await _context.ServiceCategories.OrderBy(c => c.DisplayOrder).ToListAsync();
            return View(services);
        }

        // POST: /AdminServices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (string.IsNullOrWhiteSpace(service.Name) || service.CategoryId == 0)
            {
                TempData["Error"] = "Service name and category are required.";
                return RedirectToAction("Index");
            }

            service.IsActive = true;

            // If no custom image given, default to the category's representative image
            if (string.IsNullOrWhiteSpace(service.ImagePath))
            {
                var category = await _context.ServiceCategories.FindAsync(service.CategoryId);
                var sibling = await _context.Services.FirstOrDefaultAsync(s => s.CategoryId == service.CategoryId && s.ImagePath != null);
                service.ImagePath = sibling?.ImagePath;
            }

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Service added successfully.";
            return RedirectToAction("Index");
        }

        // POST: /AdminServices/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, decimal startingPrice, int categoryId, bool isFeatured, bool isActive, string? imagePath)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                service.Name = name;
                service.StartingPrice = startingPrice;
                service.CategoryId = categoryId;
                service.IsFeatured = isFeatured;
                service.IsActive = isActive;
                if (!string.IsNullOrWhiteSpace(imagePath))
                {
                    service.ImagePath = imagePath;
                }
                await _context.SaveChangesAsync();
                TempData["Success"] = "Service updated.";
            }
            return RedirectToAction("Index");
        }

        // POST: /AdminServices/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Service deleted.";
            }
            return RedirectToAction("Index");
        }

        // ---------------- CATEGORIES ----------------

        // GET: /AdminServices/Categories
        public async Task<IActionResult> Categories()
        {
            var categories = await _context.ServiceCategories
                .Include(c => c.Services)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
            return View(categories);
        }

        // POST: /AdminServices/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(string name, string? icon, int displayOrder)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Category name is required.";
                return RedirectToAction("Categories");
            }

            _context.ServiceCategories.Add(new ServiceCategory
            {
                Name = name,
                Icon = icon,
                DisplayOrder = displayOrder
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category added.";
            return RedirectToAction("Categories");
        }

        // POST: /AdminServices/DeleteCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.ServiceCategories.Include(c => c.Services).FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                if (category.Services.Any())
                {
                    TempData["Error"] = "Cannot delete a category that still has services. Move or delete its services first.";
                    return RedirectToAction("Categories");
                }
                _context.ServiceCategories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Category deleted.";
            }
            return RedirectToAction("Categories");
        }
    }
}
