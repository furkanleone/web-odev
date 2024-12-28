using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class AIController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AIController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile uploadedPhoto)
    {
        if (uploadedPhoto == null || uploadedPhoto.Length == 0)
        {
            ModelState.AddModelError("", "Lütfen bir fotoğraf yükleyin.");
            return View("Upload");
        }

        // 1. Resmi analiz et
        string photoAnalysisResult = await ProcessPhoto(uploadedPhoto);
        if (string.IsNullOrEmpty(photoAnalysisResult))
        {
            ModelState.AddModelError("", "Fotoğraf işlenemedi. Lütfen tekrar deneyin.");
            return View("Upload");
        }

        // 2. OpenAI API çağrısı için istemci oluştur
        var client = _httpClientFactory.CreateClient("OpenAI");

        // Prompt oluştur
        string prompt = $"Kullanıcı bir fotoğraf yükledi ve bu fotoğraf {photoAnalysisResult} içeriyor. Buna göre saç modeli öner.";

        var requestBody = new
        {
            model = "gpt-4o-mini", // gpt-3.5-turbo modeli kullanılıyor
            messages = new[]
            {
            new { role = "system", content = "Sen bir saç modeli öneri asistanısın." },
            new { role = "user", content = prompt }
        },
            max_tokens = 50
        };

        try
        {
            // GPT modeline istek gönder
            var response = await client.PostAsJsonAsync("chat/completions", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                if (errorContent.Contains("insufficient_quota"))
                {
                    ModelState.AddModelError("", "API kotanız dolmuş. Lütfen OpenAI hesap ayarlarınızı kontrol edin.");
                }
                else
                {
                    ModelState.AddModelError("", $"API çağrısı başarısız: {errorContent}");
                }

                return View("Upload");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<dynamic>(responseContent);

            var recommendation = result?.choices?[0]?.message?.content?.ToString() ?? "Bir öneri oluşturulamadı.";

            // Sonuç sayfasına yönlendir
            return RedirectToAction("Result", new { recommendation });
        }
        catch (Exception ex)
        {
            // Hata yakalama
            Console.WriteLine($"Hata oluştu: {ex.Message}");
            ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
            return View("Upload");
        }
    }


    [HttpGet]
    public IActionResult Result(string recommendation)
    {
        ViewBag.Recommendation = recommendation;
        return View();
    }

    private async Task<string> ProcessPhoto(IFormFile photo)
    {
        // Resmi geçici bir dosyaya kaydet
        var filePath = Path.GetTempFileName();
        using (var stream = System.IO.File.Create(filePath))
        {
            await photo.CopyToAsync(stream);
        }

        // Resmi analiz et ve metin olarak döndür (placeholder)
        // Burada OpenCV veya Cloud Vision API gibi bir araç kullanabilirsiniz
        // Örneğin: "Saç tipi: Kıvırcık"
        return "Saç tipi: Kıvırcık"; // Örnek çıktı
    }
}
