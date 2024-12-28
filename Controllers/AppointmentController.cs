using Microsoft.AspNetCore.Mvc;
using KuaforYonetim.Models;
using KuaforYonetim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace KuaforYonetim.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly DataService _dataService;

        // Constructor ile DataService dependency injection
        public AppointmentController(DataService dataService)
        {
            _dataService = dataService;
        }

        [Authorize(Roles = "Admin")]
        // Randevuların listelendiği ana sayfa
        public IActionResult Index()
        {
            return View(_dataService.Appointments);
        }

        // Randevu detayları
        public IActionResult Details(int id)
        {
            var appointment = _dataService.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            return View(appointment);
        }

        // Randevu oluşturma sayfası (GET)
        public IActionResult Create()
        {
            ViewBag.DaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();
            ViewBag.Employees = _dataService.Employees;
            return View(new Appointment());
        }
        [Authorize(Roles = "User")]

        // Yeni randevu oluşturma işlemi (POST)
        [HttpPost]
        public IActionResult Create(Appointment newAppointment)
        {
            if (!ModelState.IsValid)
            {
                return View(newAppointment);
            }

            // Randevuyu oluştur
            newAppointment.Id = _dataService.Appointments.Any()
                ? _dataService.Appointments.Max(a => a.Id) + 1
                : 1;
            newAppointment.Status = "Pending";
            newAppointment.Employee = _dataService.Employees.FirstOrDefault(e => e.Id == newAppointment.EmployeeId);

            _dataService.Appointments.Add(newAppointment);

            return RedirectToAction("Index");
        }

        // Randevu silme
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var appointment = _dataService.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                _dataService.Appointments.Remove(appointment);
            }

            return RedirectToAction("Index");
        }

        // Randevu onaylama
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(int id)
        {
            var appointment = _dataService.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            appointment.Status = "Approved"; // Durumu onayla
            return RedirectToAction("Index");
        }

        // Randevu reddetme
        [Authorize(Roles = "Admin")]
        public IActionResult Reject(int id)
        {
            var appointment = _dataService.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            appointment.Status = "Rejected"; // Durumu reddet
            return RedirectToAction("Index");
        }

        // Bekleyen randevuları listeleme
        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            var pendingAppointments = _dataService.Appointments.Where(a => a.Status == "Pending").ToList();
            return View(pendingAppointments);
        }

        // Belirli bir gün için uygun çalışanları getir (JSON)
        [HttpGet]
        public JsonResult GetEmployeesByDay(DayOfWeek day)
        {
            var employees = _dataService.Employees
                .Where(e => e.Availability.Any(a => a.Day == day))
                .Select(e => new
                {
                    e.Id,
                    e.Name
                }).ToList();

            return Json(employees);
        }

        // Belirli bir çalışan için uygun saat aralıklarını getir (JSON)
        [HttpGet]
        public JsonResult GetAvailableSlots(int employeeId, DayOfWeek day)
        {
            var slots = _dataService.Employees.FirstOrDefault(e => e.Id == employeeId)?.Availability
                .Where(a => a.Day == day)
                .Select(a => new
                {
                    StartTime = a.StartTime.ToString(@"hh\:mm"),
                    EndTime = a.EndTime.ToString(@"hh\:mm")
                }).ToList();

            return Json(slots);
        }
    }
}
