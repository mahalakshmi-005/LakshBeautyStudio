# Laksh Beauty and Bridal Studio — Website

ASP.NET Core 8 MVC + SQL Server + Entity Framework Core.
Public customer site + login-gated Admin Panel for managing appointments.

## 🚀 Setup (Visual Studio / VS Code)

1. **Prerequisites**: .NET 8 SDK, SQL Server (LocalDB is fine for dev).
2. Open the folder in Visual Studio 2022 / VS Code.
3. Restore packages:
   ```
   dotnet restore
   ```
4. Update the connection string in `appsettings.json` if needed (default uses LocalDB).
5. Create the database + tables:
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
   *(If `dotnet ef` is not found: `dotnet tool install --global dotnet-ef`)*

   ⚠️ **If you already ran migrations before and the Service model changed (new ImagePath field)**: your existing database won't have this column. Run:
   ```
   dotnet ef migrations add AddServiceImages
   dotnet ef database update
   ```
   This adds the new column without losing your existing data. If you don't mind losing test data, you can instead delete the `Migrations` folder and your local database, then start fresh with step 5 above.
6. Run the project:
   ```
   dotnet run
   ```
7. Visit `https://localhost:<port>/` for the website.

## 🔐 Admin Panel

- URL: `/Admin/Login`
- Default credentials (auto-seeded on first run):
  - **Username:** `admin`
  - **Password:** `Admin@123`
- ⚠️ Change this password directly in the database (`AdminUsers` table) after first login — there's no "change password" UI yet, add one before going live.

## 📁 What's Included

- **Public site**: Home, All Services, Pricing, Offers, Gallery, About, FAQ, Book Appointment
- **Booking flow**: Form → validated (past date + working hours blocked) → saved to DB → auto-redirects to WhatsApp with pre-filled message
- **Admin panel**: Dashboard (stats), Appointments (filter by status, update status, delete)
- Seed data included: service categories, starter services, all bridal packages, 10 FAQs

## 🖼️ Replacing Images

- Gallery images: drop files into `wwwroot/images/gallery/` named `placeholder-1.jpg` through `placeholder-9.jpg` (or update the loop in `Views/Home/Gallery.cshtml`)
- Favicon: add `wwwroot/images/favicon.png`
- Until real photos are added, broken gallery images auto-fallback to a styled placeholder.

## 🔜 Not Yet Built (next phase)

- Admin CRUD screens for Services / Packages / Testimonials / FAQs (currently DB-seeded, editable via SQL or a future admin UI)
- Change-password screen for admin
- Email/SMS notifications on new booking
- Sitemap.xml / robots.txt for SEO
- Deployment configuration (Render/Azure) — same pattern as your Smile Desk project once you're ready

## 🎨 Design Tokens

- Primary: Gold `#D4AF37`
- Secondary: Black `#111111`
- Background: White `#FFFFFF`
- Accent: Rose Gold `#B76E79`
- Fonts: Cormorant Garamond (headings), Jost (body)
