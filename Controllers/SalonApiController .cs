using KuaforYonetim.Data;
using KuaforYonetim.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KuaforYonetim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalonApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tüm salonları getir (GET /api/salonapi)
        [HttpGet]
        public ActionResult<IEnumerable<Salon>> GetSalons()
        {
            return _context.Salons.ToList();
        }

        // ID'ye göre salon getir (GET /api/salonapi/{id})
        [HttpGet("{id}")]
        public ActionResult<Salon> GetSalon(int id)
        {
            var salon = _context.Salons.Find(id);
            if (salon == null)
            {
                return NotFound();
            }
            return salon;
        }

        // Yeni bir salon ekle (POST /api/salonapi)
        [HttpPost]
        public ActionResult<Salon> PostSalon(Salon salon)
        {
            _context.Salons.Add(salon);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSalon), new { id = salon.Id }, salon);
        }

        // Mevcut bir salonu güncelle (PUT /api/salonapi/{id})
        [HttpPut("{id}")]
        public IActionResult PutSalon(int id, Salon salon)
        {
            if (id != salon.Id)
            {
                return BadRequest();
            }

            _context.Entry(salon).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                if (!_context.Salons.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Bir salonu sil (DELETE /api/salonapi/{id})
        [HttpDelete("{id}")]
        public IActionResult DeleteSalon(int id)
        {
            var salon = _context.Salons.Find(id);
            if (salon == null)
            {
                return NotFound();
            }

            _context.Salons.Remove(salon);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
