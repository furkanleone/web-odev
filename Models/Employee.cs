using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace KuaforYonetim.Models
{
    public class Employee
    {
        public int Id { get; set; } // Çalışanın benzersiz kimliği
        public string Name { get; set; } // Çalışanın adı
        public string Position { get; set; } // Pozisyon (ör: Saç Kesimi, Makyaj)
        public string Phone { get; set; } // Telefon numarası
        public string Email { get; set; } // E-posta adresi

        // Çalışanın uygunluk saatleri (Örn: Pazartesi 09:00 - 10:00)
        public List<AvailableSlot> Availability { get; set; } = new List<AvailableSlot>();

        // Çalışanın ilişkili olduğu randevular (Appointment ile bağlantı)
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
  


        public void AddAppointment(Appointment appointment)
        {
            if (appointment.Date < DateTime.Now)
            {
                throw new InvalidOperationException("Geçmiş bir tarih için randevu eklenemez.");
            }

            // Zaman çakışmalarını kontrol et
            foreach (var existingAppointment in Appointments)
            {
                if (existingAppointment.Date == appointment.Date)
                {
                    throw new InvalidOperationException("Bu tarih için zaten bir randevu var.");
                }
            }

            Appointments.Add(appointment);
        }

    }
}