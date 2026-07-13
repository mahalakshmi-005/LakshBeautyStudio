using LakshBeautyStudio.Models;
using Microsoft.EntityFrameworkCore;

namespace LakshBeautyStudio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------- Seed: Service Categories ----------------
            modelBuilder.Entity<ServiceCategory>().HasData(
                new ServiceCategory { Id = 1, Name = "Hair Services", Icon = "💇", DisplayOrder = 1 },
                new ServiceCategory { Id = 2, Name = "Makeup Services", Icon = "💄", DisplayOrder = 2 },
                new ServiceCategory { Id = 3, Name = "Facial & Skin Care", Icon = "🌸", DisplayOrder = 3 },
                new ServiceCategory { Id = 4, Name = "Nail Services", Icon = "💅", DisplayOrder = 4 },
                new ServiceCategory { Id = 5, Name = "Eye & Brow Services", Icon = "👁️", DisplayOrder = 5 },
                new ServiceCategory { Id = 6, Name = "Body Care", Icon = "🧖", DisplayOrder = 6 },
                new ServiceCategory { Id = 7, Name = "Special Treatments", Icon = "✨", DisplayOrder = 7 },
                new ServiceCategory { Id = 8, Name = "Kids & Special Occasion", Icon = "🧒", DisplayOrder = 8 }
            );

            // ---------------- Seed: Services (full list, with category-representative images) ----------------
            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Hair Cut", StartingPrice = 250, CategoryId = 1, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 2, Name = "Hair Trim", StartingPrice = 150, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 3, Name = "Hair Wash", StartingPrice = 150, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 4, Name = "Hair Styling", StartingPrice = 500, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 5, Name = "Hair Spa", StartingPrice = 800, CategoryId = 1, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 6, Name = "Hair Smoothening", StartingPrice = 3500, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 7, Name = "Hair Straightening", StartingPrice = 4000, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 8, Name = "Hair Rebonding", StartingPrice = 4500, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 9, Name = "Hair Keratin Treatment", StartingPrice = 5000, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 10, Name = "Hair Botox Treatment", StartingPrice = 4500, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 11, Name = "Hair Coloring", StartingPrice = 1200, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 12, Name = "Hair Highlights", StartingPrice = 2500, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 13, Name = "Hair Balayage", StartingPrice = 3000, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 14, Name = "Hair Root Touch-up", StartingPrice = 800, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 15, Name = "Hair Oil Massage", StartingPrice = 300, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 16, Name = "Hair Fall Treatment", StartingPrice = 800, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 17, Name = "Dandruff Treatment", StartingPrice = 700, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 18, Name = "Scalp Treatment", StartingPrice = 700, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 19, Name = "Hair Extensions", StartingPrice = 3000, CategoryId = 1, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 20, Name = "Bridal Hair Styling", StartingPrice = 2000, CategoryId = 1, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 21, Name = "Bridal Makeup", StartingPrice = 8000, CategoryId = 2, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 22, Name = "HD Bridal Makeup", StartingPrice = 12000, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 23, Name = "Airbrush Makeup", StartingPrice = 15000, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 24, Name = "Party Makeup", StartingPrice = 2000, CategoryId = 2, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 25, Name = "Engagement Makeup", StartingPrice = 5000, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 26, Name = "Reception Makeup", StartingPrice = 6000, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 27, Name = "Sangeet Makeup", StartingPrice = 4000, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 28, Name = "Mehendi Makeup", StartingPrice = 3000, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 29, Name = "Fashion Makeup", StartingPrice = 2500, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 30, Name = "Photoshoot Makeup", StartingPrice = 2500, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 31, Name = "Natural Makeup", StartingPrice = 1500, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 32, Name = "Groom Makeup", StartingPrice = 2500, CategoryId = 2, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 33, Name = "Cleanup", StartingPrice = 500, CategoryId = 3, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 34, Name = "Fruit Facial", StartingPrice = 700, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 35, Name = "Gold Facial", StartingPrice = 1200, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 36, Name = "Diamond Facial", StartingPrice = 1800, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 37, Name = "Pearl Facial", StartingPrice = 1500, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 38, Name = "O3+ Facial", StartingPrice = 2000, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 39, Name = "Hydra Facial", StartingPrice = 3000, CategoryId = 3, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 40, Name = "Anti-Aging Facial", StartingPrice = 2500, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 41, Name = "Acne Treatment Facial", StartingPrice = 1500, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 42, Name = "Skin Brightening Facial", StartingPrice = 1800, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 43, Name = "De-Tan Treatment", StartingPrice = 700, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 44, Name = "Bleach", StartingPrice = 350, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 45, Name = "Face Polishing", StartingPrice = 1000, CategoryId = 3, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 46, Name = "Manicure", StartingPrice = 500, CategoryId = 4, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 47, Name = "Spa Manicure", StartingPrice = 900, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 48, Name = "Pedicure", StartingPrice = 700, CategoryId = 4, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 49, Name = "Spa Pedicure", StartingPrice = 1000, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 50, Name = "Gel Nail Extensions", StartingPrice = 2000, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 51, Name = "Acrylic Nail Extensions", StartingPrice = 2500, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 52, Name = "Nail Art", StartingPrice = 500, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 53, Name = "Gel Polish", StartingPrice = 700, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 54, Name = "Nail Refill", StartingPrice = 800, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 55, Name = "Nail Repair", StartingPrice = 300, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 56, Name = "Nail Removal", StartingPrice = 400, CategoryId = 4, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 57, Name = "Eyebrow Threading", StartingPrice = 50, CategoryId = 5, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 58, Name = "Upper Lip Threading", StartingPrice = 40, CategoryId = 5, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 59, Name = "Full Face Threading", StartingPrice = 200, CategoryId = 5, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 60, Name = "Eyebrow Shaping", StartingPrice = 100, CategoryId = 5, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 61, Name = "Eyebrow Tinting", StartingPrice = 300, CategoryId = 5, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 62, Name = "Eyelash Extensions", StartingPrice = 2000, CategoryId = 5, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 63, Name = "Lash Lift", StartingPrice = 1500, CategoryId = 5, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 64, Name = "Lash Tint", StartingPrice = 500, CategoryId = 5, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 65, Name = "Brow Lamination", StartingPrice = 1200, CategoryId = 5, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 66, Name = "Full Arms Waxing", StartingPrice = 400, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 67, Name = "Half Arms Waxing", StartingPrice = 250, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 68, Name = "Full Legs Waxing", StartingPrice = 600, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 69, Name = "Half Legs Waxing", StartingPrice = 350, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 70, Name = "Underarm Waxing", StartingPrice = 200, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 71, Name = "Full Body Waxing", StartingPrice = 2000, CategoryId = 6, IsFeatured = true, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 72, Name = "Chocolate Wax", StartingPrice = 900, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 73, Name = "Rica Wax", StartingPrice = 1000, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 74, Name = "Bikini Wax", StartingPrice = 800, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 75, Name = "Body Polishing", StartingPrice = 2500, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 76, Name = "Body Scrub", StartingPrice = 1500, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 77, Name = "Body Spa", StartingPrice = 2000, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 78, Name = "Aroma Body Massage", StartingPrice = 1500, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 79, Name = "Deep Tissue Massage", StartingPrice = 2000, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 80, Name = "Hot Stone Massage", StartingPrice = 2500, CategoryId = 6, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 81, Name = "Skin Consultation", StartingPrice = 200, CategoryId = 7, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 82, Name = "Pigmentation Treatment", StartingPrice = 2000, CategoryId = 7, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 83, Name = "Tan Removal", StartingPrice = 700, CategoryId = 7, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 84, Name = "Dark Circle Treatment", StartingPrice = 1500, CategoryId = 7, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 85, Name = "Anti-Acne Treatment", StartingPrice = 1500, CategoryId = 7, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 86, Name = "Skin Hydration Therapy", StartingPrice = 2000, CategoryId = 7, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 87, Name = "Kids Hair Cut", StartingPrice = 150, CategoryId = 8, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 88, Name = "Kids Party Hairstyle", StartingPrice = 500, CategoryId = 8, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 89, Name = "Birthday Makeup", StartingPrice = 1500, CategoryId = 8, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 90, Name = "Graduation Makeup", StartingPrice = 1500, CategoryId = 8, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600" },
                new Service { Id = 91, Name = "Festive Makeup", StartingPrice = 1500, CategoryId = 8, IsFeatured = false, ImagePath = "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600" }
            );

            // ---------------- Seed: Packages ----------------
            modelBuilder.Entity<Package>().HasData(
    new Package { Id = 1, Name = "Basic Bridal Package", Price = 12999, DisplayOrder = 1 },
    new Package { Id = 2, Name = "Premium Bridal Package", Price = 19999, DisplayOrder = 2 },
    new Package { Id = 3, Name = "Luxury Bridal Package", Price = 29999, DisplayOrder = 3 },
    new Package { Id = 4, Name = "Pre-Bridal Package", Price = 5999, DisplayOrder = 4 },
    new Package { Id = 5, Name = "Engagement Package", Price = 8999, DisplayOrder = 5 },
    new Package { Id = 6, Name = "Reception Package", Price = 9999, DisplayOrder = 6 },
    new Package { Id = 7, Name = "Groom Package", Price = 2999, DisplayOrder = 7 },
    new Package { Id = 8, Name = "Couple Wedding Package", Price = 24999, DisplayOrder = 8 },
    new Package { Id = 9, Name = "Monthly Beauty Package", Price = 2499, DisplayOrder = 9 }
);
            // ---------------- Seed: FAQs ----------------
            modelBuilder.Entity<FAQ>().HasData(
                new FAQ { Id = 1, Question = "Do I need an appointment?", Answer = "Yes, we recommend booking in advance to ensure availability, especially for bridal services.", DisplayOrder = 1 },
                new FAQ { Id = 2, Question = "Is walk-in available?", Answer = "Yes, walk-ins are welcome subject to slot availability.", DisplayOrder = 2 },
                new FAQ { Id = 3, Question = "Do you provide bridal service at home?", Answer = "Currently we offer bridal services at our studio. Please contact us for special requests.", DisplayOrder = 3 },
                new FAQ { Id = 4, Question = "What are your working hours?", Answer = "We are open 9 AM to 2 PM and 3 PM to 7 PM, all days.", DisplayOrder = 4 },
                new FAQ { Id = 5, Question = "How do I book an appointment?", Answer = "You can book via our website form, WhatsApp, or by calling us directly.", DisplayOrder = 5 },
                new FAQ { Id = 6, Question = "Do you offer bridal packages?", Answer = "Yes, we have Basic, Premium, and Luxury bridal packages tailored to your needs.", DisplayOrder = 6 },
                new FAQ { Id = 7, Question = "What products do you use?", Answer = "We use premium quality, skin-friendly beauty products for all our services.", DisplayOrder = 7 },
                new FAQ { Id = 8, Question = "Can I reschedule my appointment?", Answer = "Yes, please contact us at least a few hours in advance to reschedule.", DisplayOrder = 8 },
                new FAQ { Id = 9, Question = "Do you have combo offers?", Answer = "Yes, we have Hair Care Combo, Skin Glow Combo, and Nail Beauty Combo at special prices.", DisplayOrder = 9 },
                new FAQ { Id = 10, Question = "Is advance payment required for bridal bookings?", Answer = "Bridal packages require advance booking; please contact us for payment details.", DisplayOrder = 10 }
            );

            // ---------------- Seed: Gallery Images ----------------
            // Free-license stock photos (Pexels License - free for commercial use, no attribution required)
            // Replace with real studio/client photos anytime via Admin > Gallery
            modelBuilder.Entity<GalleryImage>().HasData(
                new GalleryImage { Id = 1, ImagePath = "https://images.pexels.com/photos/7750099/pexels-photo-7750099.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Studio Interior", DisplayOrder = 1, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) },
                new GalleryImage { Id = 2, ImagePath = "https://images.pexels.com/photos/7750119/pexels-photo-7750119.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Reception Lobby", DisplayOrder = 2, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) },
                new GalleryImage { Id = 3, ImagePath = "https://images.pexels.com/photos/7750091/pexels-photo-7750091.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Styling Area", DisplayOrder = 3, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) },
                new GalleryImage { Id = 4, ImagePath = "https://images.pexels.com/photos/7750104/pexels-photo-7750104.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Makeup Station", DisplayOrder = 4, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) },
                new GalleryImage { Id = 5, ImagePath = "https://images.pexels.com/photos/7750100/pexels-photo-7750100.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Salon Seating", DisplayOrder = 5, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) },
                new GalleryImage { Id = 6, ImagePath = "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Lounge Area", DisplayOrder = 6, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) },
                new GalleryImage { Id = 7, ImagePath = "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Vanity Mirrors", DisplayOrder = 7, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) },
                new GalleryImage { Id = 8, ImagePath = "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Studio View", DisplayOrder = 8, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) },
                new GalleryImage { Id = 9, ImagePath = "https://images.pexels.com/photos/7750120/pexels-photo-7750120.jpeg?auto=compress&cs=tinysrgb&w=800", Caption = "Elegant Interior", DisplayOrder = 9, IsActive = true, UploadedAt = new DateTime(2026, 1, 1) }
            );
        }
    }
}
