using LakshBeautyStudio.Data;
using LakshBeautyStudio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LakshBeautyStudio.Controllers
{
    [Authorize]
    public class AdminPackagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPackagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /AdminPackages/Index
        public async Task<IActionResult> Index()
        {
            var packages = await _context.Packages.OrderBy(p => p.DisplayOrder).ToListAsync();
            return View(packages);
        }

        // POST: /AdminPackages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, decimal price, string? description, int displayOrder)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Package name is required.";
                return RedirectToAction("Index");
            }

            _context.Packages.Add(new Package
            {
                Name = name,
                Price = price,
                Description = description,
                DisplayOrder = displayOrder,
                IsActive = true
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "Package added.";
            return RedirectToAction("Index");
        }

        // POST: /AdminPackages/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, decimal price, string? description, int displayOrder, bool isActive)
        {
            var package = await _context.Packages.FindAsync(id);
            if (package != null)
            {
                package.Name = name;
                package.Price = price;
                package.Description = description;
                package.DisplayOrder = displayOrder;
                package.IsActive = isActive;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Package updated.";
            }
            return RedirectToAction("Index");
        }

        // POST: /AdminPackages/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var package = await _context.Packages.FindAsync(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Package deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}
