using System.ComponentModel.DataAnnotations;

namespace KuaforYonetim.Models
{
    public class Appointment
    {
        public int Id { get; set; } // Appointment'ın benzersiz kimliği

        // Müşteri Bilgileri
        public string CustomerName { get; set; } // Randevu alan müşterinin adı
        public string Phone { get; set; } // Müşterinin iletişim numarası
        public string Service { get; set; } // Hizmet türü (ör: Saç Kesimi, Manikür)

        // Randevu Tarihi ve Saat Bilgisi
        public DateTime Date { get; set; } // Randevu tarihi ve saati
        public string SelectedTimeSlot { get; set; } // Seçilen saat dilimi (ör: "09:00 - 10:00")

        // Çalışan ile İlişki
        public int EmployeeId { get; set; } // Çalışan Id'si (Foreign Key)
        public Employee Employee { get; set; } // Çalışan nesnesi ile ilişki

        // Randevu Durumu
        public string Status { get; set; } = "Pending"; // Varsayılan durum: Beklemede

    }
    // Gelecekteki tarih kontrolü için özel validasyon
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }
            return false;
        }
    }
}