using KuaforYonetim.Models;

namespace KuaforYonetim.Services
{
    public class DataService
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public DataService()
        {
            // Çalışanlar
            Employees.Add(new Employee
            {
                Id = 1,
                Name = "Mehmet Topal(Serdivan)",
                Position = "Saç Kesimi",
                Phone = "5369874521",
                Email = "mehmet@example.com",
                Availability = new List<AvailableSlot>
                {
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                }
            });

            Employees.Add(new Employee
            {
                Id = 2,
                Name = "Mustafa Aslan(Serdivan)",
                Position = "Saç Kesimi",
                Phone = "5427895686",
                Email = "mustafa@example.com",
                Availability = new List<AvailableSlot>
                {
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                }
            });

            Employees.Add(new Employee
            {
                Id = 3,
                Name = "Cüneyt Akif(Arifiye)",
                Position = "Saç Kesimi",
                Phone = "5239687542",
                Email = "cuneyt@example.com",
                Availability = new List<AvailableSlot>
                {
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                }
            });

            Employees.Add(new Employee
            {
                Id = 4,
                Name = "Samet Akpınar(Arifiye)",
                Position = "Saç Kesimi",
                Phone = "5648569575",
                Email = "samet@example.com",
                Availability = new List<AvailableSlot>
                {
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0) },
                    new AvailableSlot { Day = DayOfWeek.Monday, StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
                }
            });

            // Randevular
            Appointments.Add(new Appointment
            {
                Id = 1,
                CustomerName = "Ahmet Yılmaz",
                Phone = "5551234567",
                Service = "Saç Kesimi",
                Date = DateTime.Now.AddDays(2), // Bugünden 2 gün sonraya
                SelectedTimeSlot = "10:00 - 11:00",
                EmployeeId = 1,
                Employee = Employees.FirstOrDefault(e => e.Id == 1),
                Status = "Pending"
            });

            Appointments.Add(new Appointment
            {
                Id = 2,
                CustomerName = "Ayşe Demir",
                Phone = "5549876543",
                Service = "Manikür",
                Date = DateTime.Now.AddDays(3), // Bugünden 3 gün sonraya
                SelectedTimeSlot = "14:00 - 15:00",
                EmployeeId = 2,
                Employee = Employees.FirstOrDefault(e => e.Id == 2),
                Status = "Pending"
            });

            Appointments.Add(new Appointment
            {
                Id = 3,
                CustomerName = "Caner Yılmaz",
                Phone = "5534567890",
                Service = "Saç Boyama",
                Date = DateTime.Now.AddDays(5), // Bugünden 5 gün sonraya
                SelectedTimeSlot = "09:00 - 10:00",
                EmployeeId = 1,
                Employee = Employees.FirstOrDefault(e => e.Id == 1),
                Status = "Confirmed"
            });
        }
    }
}
