using LakshBeautyStudio.Data;
using LakshBeautyStudio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LakshBeautyStudio.Controllers
{
    [Authorize]
    public class AdminFAQController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminFAQController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /AdminFAQ/Index
        public async Task<IActionResult> Index()
        {
            var faqs = await _context.FAQs.OrderBy(f => f.DisplayOrder).ToListAsync();
            return View(faqs);
        }

        // POST: /AdminFAQ/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string question, string answer, int displayOrder)
        {
            if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(answer))
            {
                TempData["Error"] = "Question and answer are required.";
                return RedirectToAction("Index");
            }

            _context.FAQs.Add(new FAQ
            {
                Question = question,
                Answer = answer,
                DisplayOrder = displayOrder,
                IsActive = true
            });
            await _context.SaveChangesAsync();
            TempData["Success"] = "FAQ added.";
            return RedirectToAction("Index");
        }

        // POST: /AdminFAQ/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string question, string answer, int displayOrder, bool isActive)
        {
            var faq = await _context.FAQs.FindAsync(id);
            if (faq != null)
            {
                faq.Question = question;
                faq.Answer = answer;
                faq.DisplayOrder = displayOrder;
                faq.IsActive = isActive;
                await _context.SaveChangesAsync();
                TempData["Success"] = "FAQ updated.";
            }
            return RedirectToAction("Index");
        }

        // POST: /AdminFAQ/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var faq = await _context.FAQs.FindAsync(id);
            if (faq != null)
            {
                _context.FAQs.Remove(faq);
                await _context.SaveChangesAsync();
                TempData["Success"] = "FAQ deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}
