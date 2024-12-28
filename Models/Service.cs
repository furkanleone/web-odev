namespace KuaforYonetim.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; } // Dakika olarak süre
        public decimal Price { get; set; } // Hizmet ücreti

        // İlişkiler
      
    }
}
