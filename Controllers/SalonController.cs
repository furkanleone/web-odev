using KuaforYonetim.Data;
using KuaforYonetim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KuaforYonetim.Controllers
{
    public class SalonController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor ile ApplicationDbContext'i ekliyoruz.
        public SalonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tüm salonları listele
        public IActionResult Index()
        {
            // Veritabanından tüm salonları getiriyoruz
            var salons = _context.Salons.ToList();
            return View(salons);
        }

        // Salon detayları
        public IActionResult Details(int id)
        {
            // ID'ye göre salonu veritabanından bul
            var salon = _context.Salons.FirstOrDefault(s => s.Id == id);
            if (salon == null)
            {
                return NotFound();
            }

            return View(salon);
        }

        // Yeni salon oluştur
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Salon newSalon)
        {
            if (ModelState.IsValid)
            {
                // Yeni salonu veritabanına ekle
                _context.Salons.Add(newSalon);
                _context.SaveChanges(); // Değişiklikleri veritabanına kaydet
                return RedirectToAction("Index");
            }

            return View(newSalon);
        }
    }
}
