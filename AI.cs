namespace KuaforYonetim.Models
{
    public class HaircutRecommendationRequest
    {
        public string ImageBase64 { get; set; } // Fotoğraf Base64 formatında
    }

    public class HaircutRecommendationResponse
    {
        public string Recommendation { get; set; } // Önerilen saç stili
    }

}
