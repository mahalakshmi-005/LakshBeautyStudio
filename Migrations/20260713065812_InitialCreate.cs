using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LakshBeautyStudio.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Service = table.Column<string>(type: "text", nullable: false),
                    PreferredDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PreferredTime = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Question = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Answer = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    Caption = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    ReviewText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    StartingPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_ServiceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FAQs",
                columns: new[] { "Id", "Answer", "DisplayOrder", "IsActive", "Question" },
                values: new object[,]
                {
                    { 1, "Yes, we recommend booking in advance to ensure availability, especially for bridal services.", 1, true, "Do I need an appointment?" },
                    { 2, "Yes, walk-ins are welcome subject to slot availability.", 2, true, "Is walk-in available?" },
                    { 3, "Currently we offer bridal services at our studio. Please contact us for special requests.", 3, true, "Do you provide bridal service at home?" },
                    { 4, "We are open 9 AM to 2 PM and 3 PM to 7 PM, all days.", 4, true, "What are your working hours?" },
                    { 5, "You can book via our website form, WhatsApp, or by calling us directly.", 5, true, "How do I book an appointment?" },
                    { 6, "Yes, we have Basic, Premium, and Luxury bridal packages tailored to your needs.", 6, true, "Do you offer bridal packages?" },
                    { 7, "We use premium quality, skin-friendly beauty products for all our services.", 7, true, "What products do you use?" },
                    { 8, "Yes, please contact us at least a few hours in advance to reschedule.", 8, true, "Can I reschedule my appointment?" },
                    { 9, "Yes, we have Hair Care Combo, Skin Glow Combo, and Nail Beauty Combo at special prices.", 9, true, "Do you have combo offers?" },
                    { 10, "Bridal packages require advance booking; please contact us for payment details.", 10, true, "Is advance payment required for bridal bookings?" }
                });

            migrationBuilder.InsertData(
                table: "GalleryImages",
                columns: new[] { "Id", "Caption", "DisplayOrder", "ImagePath", "IsActive", "UploadedAt" },
                values: new object[,]
                {
                    { 1, "Studio Interior", 1, "https://images.pexels.com/photos/7750099/pexels-photo-7750099.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Reception Lobby", 2, "https://images.pexels.com/photos/7750119/pexels-photo-7750119.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Styling Area", 3, "https://images.pexels.com/photos/7750091/pexels-photo-7750091.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Makeup Station", 4, "https://images.pexels.com/photos/7750104/pexels-photo-7750104.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Salon Seating", 5, "https://images.pexels.com/photos/7750100/pexels-photo-7750100.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Lounge Area", 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Vanity Mirrors", 7, "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Studio View", 8, "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Elegant Interior", 9, "https://images.pexels.com/photos/7750120/pexels-photo-7750120.jpeg?auto=compress&cs=tinysrgb&w=800", true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "Description", "DisplayOrder", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, 1, true, "Basic Bridal Package", 12999m },
                    { 2, null, 2, true, "Premium Bridal Package", 19999m },
                    { 3, null, 3, true, "Luxury Bridal Package", 29999m },
                    { 4, null, 4, true, "Pre-Bridal Package", 5999m },
                    { 5, null, 5, true, "Engagement Package", 8999m },
                    { 6, null, 6, true, "Reception Package", 9999m },
                    { 7, null, 7, true, "Groom Package", 2999m },
                    { 8, null, 8, true, "Couple Wedding Package", 24999m },
                    { 9, null, 9, true, "Monthly Beauty Package", 2499m }
                });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "Id", "DisplayOrder", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, 1, "💇", "Hair Services" },
                    { 2, 2, "💄", "Makeup Services" },
                    { 3, 3, "🌸", "Facial & Skin Care" },
                    { 4, 4, "💅", "Nail Services" },
                    { 5, 5, "👁️", "Eye & Brow Services" },
                    { 6, 6, "🧖", "Body Care" },
                    { 7, 7, "✨", "Special Treatments" },
                    { 8, 8, "🧒", "Kids & Special Occasion" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CategoryId", "ImagePath", "IsActive", "IsFeatured", "Name", "StartingPrice" },
                values: new object[,]
                {
                    { 1, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, true, "Hair Cut", 250m },
                    { 2, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Trim", 150m },
                    { 3, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Wash", 150m },
                    { 4, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Styling", 500m },
                    { 5, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, true, "Hair Spa", 800m },
                    { 6, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Smoothening", 3500m },
                    { 7, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Straightening", 4000m },
                    { 8, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Rebonding", 4500m },
                    { 9, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Keratin Treatment", 5000m },
                    { 10, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Botox Treatment", 4500m },
                    { 11, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Coloring", 1200m },
                    { 12, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Highlights", 2500m },
                    { 13, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Balayage", 3000m },
                    { 14, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Root Touch-up", 800m },
                    { 15, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Oil Massage", 300m },
                    { 16, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Fall Treatment", 800m },
                    { 17, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Dandruff Treatment", 700m },
                    { 18, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Scalp Treatment", 700m },
                    { 19, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hair Extensions", 3000m },
                    { 20, 1, "https://images.pexels.com/photos/367724/pexels-photo-367724.jpeg?auto=compress&cs=tinysrgb&w=600", true, true, "Bridal Hair Styling", 2000m },
                    { 21, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, true, "Bridal Makeup", 8000m },
                    { 22, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "HD Bridal Makeup", 12000m },
                    { 23, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Airbrush Makeup", 15000m },
                    { 24, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, true, "Party Makeup", 2000m },
                    { 25, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Engagement Makeup", 5000m },
                    { 26, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Reception Makeup", 6000m },
                    { 27, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Sangeet Makeup", 4000m },
                    { 28, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Mehendi Makeup", 3000m },
                    { 29, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Fashion Makeup", 2500m },
                    { 30, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Photoshoot Makeup", 2500m },
                    { 31, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Natural Makeup", 1500m },
                    { 32, 2, "https://images.pexels.com/photos/6161/makeup-make-up-artist-make-up.jpg?auto=compress&cs=tinysrgb&w=600", true, false, "Groom Makeup", 2500m },
                    { 33, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, true, "Cleanup", 500m },
                    { 34, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Fruit Facial", 700m },
                    { 35, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Gold Facial", 1200m },
                    { 36, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Diamond Facial", 1800m },
                    { 37, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Pearl Facial", 1500m },
                    { 38, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "O3+ Facial", 2000m },
                    { 39, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, true, "Hydra Facial", 3000m },
                    { 40, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Anti-Aging Facial", 2500m },
                    { 41, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Acne Treatment Facial", 1500m },
                    { 42, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Skin Brightening Facial", 1800m },
                    { 43, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "De-Tan Treatment", 700m },
                    { 44, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Bleach", 350m },
                    { 45, 3, "https://images.pexels.com/photos/361755/pexels-photo-361755.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Face Polishing", 1000m },
                    { 46, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, true, "Manicure", 500m },
                    { 47, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Spa Manicure", 900m },
                    { 48, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, true, "Pedicure", 700m },
                    { 49, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Spa Pedicure", 1000m },
                    { 50, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Gel Nail Extensions", 2000m },
                    { 51, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Acrylic Nail Extensions", 2500m },
                    { 52, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Nail Art", 500m },
                    { 53, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Gel Polish", 700m },
                    { 54, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Nail Refill", 800m },
                    { 55, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Nail Repair", 300m },
                    { 56, 4, "https://images.pexels.com/photos/5484948/pexels-photo-5484948.png?auto=compress&cs=tinysrgb&w=600", true, false, "Nail Removal", 400m },
                    { 57, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, true, "Eyebrow Threading", 50m },
                    { 58, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Upper Lip Threading", 40m },
                    { 59, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Full Face Threading", 200m },
                    { 60, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Eyebrow Shaping", 100m },
                    { 61, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Eyebrow Tinting", 300m },
                    { 62, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, true, "Eyelash Extensions", 2000m },
                    { 63, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Lash Lift", 1500m },
                    { 64, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Lash Tint", 500m },
                    { 65, 5, "https://images.pexels.com/photos/3885102/pexels-photo-3885102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Brow Lamination", 1200m },
                    { 66, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Full Arms Waxing", 400m },
                    { 67, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Half Arms Waxing", 250m },
                    { 68, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Full Legs Waxing", 600m },
                    { 69, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Half Legs Waxing", 350m },
                    { 70, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Underarm Waxing", 200m },
                    { 71, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, true, "Full Body Waxing", 2000m },
                    { 72, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Chocolate Wax", 900m },
                    { 73, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Rica Wax", 1000m },
                    { 74, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Bikini Wax", 800m },
                    { 75, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Body Polishing", 2500m },
                    { 76, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Body Scrub", 1500m },
                    { 77, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Body Spa", 2000m },
                    { 78, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Aroma Body Massage", 1500m },
                    { 79, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Deep Tissue Massage", 2000m },
                    { 80, 6, "https://images.pexels.com/photos/7750102/pexels-photo-7750102.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Hot Stone Massage", 2500m },
                    { 81, 7, "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Skin Consultation", 200m },
                    { 82, 7, "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Pigmentation Treatment", 2000m },
                    { 83, 7, "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Tan Removal", 700m },
                    { 84, 7, "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Dark Circle Treatment", 1500m },
                    { 85, 7, "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Anti-Acne Treatment", 1500m },
                    { 86, 7, "https://images.pexels.com/photos/7750108/pexels-photo-7750108.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Skin Hydration Therapy", 2000m },
                    { 87, 8, "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Kids Hair Cut", 150m },
                    { 88, 8, "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Kids Party Hairstyle", 500m },
                    { 89, 8, "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Birthday Makeup", 1500m },
                    { 90, 8, "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Graduation Makeup", 1500m },
                    { 91, 8, "https://images.pexels.com/photos/7750116/pexels-photo-7750116.jpeg?auto=compress&cs=tinysrgb&w=600", true, false, "Festive Makeup", 1500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_CategoryId",
                table: "Services",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "GalleryImages");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropTable(
                name: "ServiceCategories");
        }
    }
}
