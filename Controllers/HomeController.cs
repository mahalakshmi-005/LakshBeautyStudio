using LakshBeautyStudio.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LakshBeautyStudio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /
        public async Task<IActionResult> Index()
        {
            ViewBag.FeaturedServices = await _context.Services
                .Where(s => s.IsFeatured && s.IsActive)
                .Include(s => s.Category)
                .ToListAsync();

            ViewBag.Testimonials = await _context.Testimonials
                .Where(t => t.IsActive)
                .OrderByDescending(t => t.CreatedAt)
                .Take(6)
                .ToListAsync();

            return View();
        }

        // GET: /Home/Services
        public async Task<IActionResult> Services()
        {
            var categories = await _context.ServiceCategories
                .Include(c => c.Services.Where(s => s.IsActive))
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();

            return View(categories);
        }

        // GET: /Home/Pricing
        public async Task<IActionResult> Pricing()
        {
            var categories = await _context.ServiceCategories
                .Include(c => c.Services.Where(s => s.IsActive))
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();

            ViewBag.Packages = await _context.Packages
                .Where(p => p.IsActive)
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();

            return View(categories);
        }

        // GET: /Home/Offers
        public IActionResult Offers()
        {
            return View();
        }

        // GET: /Home/Gallery
        public async Task<IActionResult> Gallery()
        {
            var images = await _context.GalleryImages
                .Where(g => g.IsActive)
                .OrderBy(g => g.DisplayOrder)
                .ToListAsync();
            return View(images);
        }

        // GET: /Home/About
        public IActionResult About()
        {
            return View();
        }

        // GET: /Home/FAQPage
        public async Task<IActionResult> FAQPage()
        {
            var faqs = await _context.FAQs
                .Where(f => f.IsActive)
                .OrderBy(f => f.DisplayOrder)
                .ToListAsync();

            return View(faqs);
        }

        // GET: /Home/Contact
        public IActionResult Contact()
        {
            return View();
        }

        // GET: /Home/Error
        public IActionResult Error()
        {
            return View();
        }
    }
}
