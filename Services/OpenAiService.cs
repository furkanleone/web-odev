using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class OpenAIService
{
    private readonly HttpClient _httpClient;

    public OpenAIService(string apiKey)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }

    public async Task<string> GetResponseAsync(string prompt)
    {
        var requestBody = new
        {
            model = "gpt-3.5-turbo", // Model (gpt-4 olarak değiştirilebilir)
            messages = new[]
            {
                new { role = "system", content = "You are an assistant that provides hair style suggestions." },
                new { role = "user", content = prompt }
            },
            max_tokens = 100
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(responseContent);

            // OpenAI'nin yanıtındaki içeriği çıkartıyoruz
            var messageContent = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return messageContent ?? "No response received.";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}
