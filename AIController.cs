using Microsoft.AspNetCore.Mvc;
using RestSharp;
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
    public async Task<IActionResult> Recommend(string userInput)
    {
        if (string.IsNullOrEmpty(userInput))
        {
            ModelState.AddModelError("", "Lütfen bir giriş yapın.");
            return View("Upload");
        }

        var client = _httpClientFactory.CreateClient("OpenAI");

        var requestBody = new
        {
            model = "text-davinci-003", // ChatGPT modeli
            prompt = $"Bir kullanıcı şu soruyu sordu: {userInput}. Ona bir öneri yap.",
            max_tokens = 100
        };

        var response = await client.PostAsJsonAsync("completions", requestBody);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "API çağrısı başarısız. Lütfen tekrar deneyin.");
            return View("Upload");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<dynamic>(responseContent);

        var recommendation = result?.choices?[0]?.text?.ToString() ?? "Bir öneri oluşturulamadı.";

        return RedirectToAction("Result", new { recommendation });
    }

    [HttpGet]
    public IActionResult Result(string recommendation)
    {
        return View("Result", recommendation);
    }
}
