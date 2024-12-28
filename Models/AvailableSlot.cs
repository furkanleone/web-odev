using System;
using System.ComponentModel.DataAnnotations;

namespace KuaforYonetim.Models
{
    public class AvailableSlot
    {
        [Key] // Benzersiz kimlik
        public int Id { get; set; }

        [Required(ErrorMessage = "Gün bilgisi gereklidir.")]
        public DayOfWeek Day { get; set; } // Gün (ör: Pazartesi)

        [Required(ErrorMessage = "Başlangıç saati gereklidir.")]
        public TimeSpan StartTime { get; set; } // Başlangıç saati

        [Required(ErrorMessage = "Bitiş saati gereklidir.")]
        public TimeSpan EndTime { get; set; } // Bitiş saati

        // Çalışan ile ilişki

    }
}
