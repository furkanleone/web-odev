using KuaforYonetim.Models;
using KuaforYonetim.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KuaforYonetim.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataService _dataService;

        // Constructor ile DataService dependency injection
        public EmployeeController(DataService dataService)
        {
            _dataService = dataService;
        }

        // Tüm çalışanları listele
        public IActionResult Index()
        {
            var employees = _dataService.Employees; // DataService'den çalışanları al
            return View(employees); // Çalışanları View'e gönder
        }

        // Belirli bir çalışanın detaylarını göster
        public IActionResult Details(int id)
        {
            var employee = _dataService.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Çalışan bulunamadı."); // Çalışan bulunamazsa 404 döner
            }
            return View(employee); // Çalışanı detay görünümüne gönder
        }

        // Yeni bir çalışan oluştur (Form Sayfası - GET)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new Employee()); // Boş bir çalışan modeli ile formu başlat
        }

        // Yeni bir çalışan oluştur (Formu İşle - POST)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Employee newEmployee)
        {
            if (!ModelState.IsValid)
            {
                return View(newEmployee); // Form doğrulama hataları varsa tekrar göster
            }

            // Yeni bir ID ata ve çalışanı listeye ekle
            var nextId = _dataService.Employees.Any() ? _dataService.Employees.Max(e => e.Id) + 1 : 1;
            newEmployee.Id = nextId;
            _dataService.Employees.Add(newEmployee);

            return RedirectToAction("Index"); // Çalışan listesini göster
        }

        // Çalışan düzenleme (Form Sayfası - GET)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var employee = _dataService.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Çalışan bulunamadı."); // Çalışan bulunamazsa hata döner
            }

            return View(employee); // Çalışanı düzenleme formuna gönder
        }

        // Çalışan düzenleme (Formu İşle - POST)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Employee updatedEmployee)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedEmployee); // Form doğrulama hataları varsa tekrar göster
            }

            var existingEmployee = _dataService.Employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
            if (existingEmployee == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }

            // Çalışan bilgilerini güncelle
            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Position = updatedEmployee.Position;
            existingEmployee.Phone = updatedEmployee.Phone;
            existingEmployee.Email = updatedEmployee.Email;

            // Uygunluk saatlerini güncelle
            existingEmployee.Availability = updatedEmployee.Availability
                ?.Where(slot => slot.StartTime < slot.EndTime) // Sadece geçerli saat aralıklarını al
                .ToList() ?? new List<AvailableSlot>();

            return RedirectToAction("Index");
        }

        // Çalışanı sil (Form Sayfası - GET)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var employee = _dataService.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }

            return View(employee); // Çalışanı silme onay sayfasına gönder
        }

        // Çalışanı sil (Formu İşle - POST)
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _dataService.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }

            _dataService.Employees.Remove(employee); // Çalışanı listeden kaldır
            return RedirectToAction("Index");
        }

    }
}
