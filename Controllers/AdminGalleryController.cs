using LakshBeautyStudio.Data;
using LakshBeautyStudio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LakshBeautyStudio.Controllers
{
    [Authorize]
    public class AdminGalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public AdminGalleryController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: /AdminGallery/Index
        public async Task<IActionResult> Index()
        {
            var images = await _context.GalleryImages.OrderBy(g => g.DisplayOrder).ToListAsync();
            return View(images);
        }

        // POST: /AdminGallery/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file, string? caption, int displayOrder)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Please choose an image file.";
                return RedirectToAction("Index");
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                TempData["Error"] = "Only JPG, PNG, or WEBP images are allowed.";
                return RedirectToAction("Index");
            }

            if (file.Length > 5 * 1024 * 1024) // 5MB limit
            {
                TempData["Error"] = "Image must be smaller than 5MB.";
                return RedirectToAction("Index");
            }

            var galleryFolder = Path.Combine(_env.WebRootPath, "images", "gallery");
            Directory.CreateDirectory(galleryFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(galleryFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _context.GalleryImages.Add(new GalleryImage
            {
                ImagePath = $"/images/gallery/{uniqueFileName}",
                Caption = caption,
                DisplayOrder = displayOrder,
                IsActive = true,
                UploadedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();

            TempData["Success"] = "Image uploaded successfully.";
            return RedirectToAction("Index");
        }

        // POST: /AdminGallery/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _context.GalleryImages.FindAsync(id);
            if (image != null)
            {
                // Remove the physical file if it exists
                var physicalPath = Path.Combine(_env.WebRootPath, image.ImagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }

                _context.GalleryImages.Remove(image);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Image deleted.";
            }
            return RedirectToAction("Index");
        }

        // POST: /AdminGallery/ToggleActive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var image = await _context.GalleryImages.FindAsync(id);
            if (image != null)
            {
                image.IsActive = !image.IsActive;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
