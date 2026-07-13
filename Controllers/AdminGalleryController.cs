using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        private readonly Cloudinary _cloudinary;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public AdminGalleryController(ApplicationDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
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

            // Upload to Cloudinary instead of local disk
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "laksh-beauty-gallery",
                UniqueFilename = true,
                Overwrite = false
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                TempData["Error"] = $"Upload failed: {uploadResult.Error.Message}";
                return RedirectToAction("Index");
            }

            _context.GalleryImages.Add(new GalleryImage
            {
                ImagePath = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId,
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
                // Remove from Cloudinary if it has a PublicId
                if (!string.IsNullOrEmpty(image.PublicId))
                {
                    var deleteParams = new DeletionParams(image.PublicId);
                    await _cloudinary.DestroyAsync(deleteParams);
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