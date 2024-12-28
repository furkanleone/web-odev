using Microsoft.EntityFrameworkCore;
using KuaforYonetim.Models;

namespace KuaforYonetim.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tabloları tanımlıyoruz
        public DbSet<Salon> Salons { get; set; } // Salon bağımsız tablo

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          

            // Seed Data: Salon bağımsız olarak tanımlanır
            modelBuilder.Entity<Salon>().HasData(
                new Salon
                {
                    Id = 1,
                    Name = "Serdivan Salon",
                    Address = "Serdivan, Sincan Mahallesi",
                    Phone = "0312-123-4567",
                    WorkingHours = "09:00 - 21:00",
                    Image = "serdivan4.jpg"
                },
                new Salon
                {
                    Id = 2,
                    Name = "Arifiye Salon",
                    Address = "Arifiye, Arifbey Mahallesi",
                    Phone = "0216-987-6543",
                    WorkingHours = "10:00 - 20:00",
                    Image = "arifiye.jpg"
                }
            );

            
        }
    }
}
