using KuaforYonetim.Data;
using KuaforYonetim.Models;
using KuaforYonetim.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// **1. OpenAI Servisi Ayarlar�**
var openAiApiKey = builder.Configuration["OpenAI:ApiKey"];

if (string.IsNullOrEmpty(openAiApiKey))
{
    throw new Exception("OpenAI API anahtar� yap�land�r�lmad�. L�tfen appsettings.json dosyan�za 'OpenAI:ApiKey' ekleyin.");
}

builder.Services.AddHttpClient("OpenAI", client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["OpenAI:ApiKey"]}");
});


// **2. Veritaban� Ba�lant�s�**
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new Exception("Veritaban� ba�lant� dizesi bulunamad�. L�tfen appsettings.json dosyan�za 'ConnectionStrings:DefaultConnection' ekleyin.");
    }
    options.UseSqlServer(connectionString);
});

// **3. Data Servisi ve Singleton Ba��ml�l�klar�**
builder.Services.AddSingleton<DataService>();

// **4. MVC Servislerini ve Yetkilendirmeyi Ekle**
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login"; // Giri� sayfas� y�nlendirmesi
        options.AccessDeniedPath = "/Account/AccessDenied"; // Yetki reddedilme sayfas�
    });

builder.Services.AddAuthorization();

// **5. Swagger Dok�mantasyonu**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swagger i�in gerekli servisler

// **6. Uygulamay� Yap�land�rma**
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
// **7. Hata Y�netimi**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Kullan�c� dostu hata sayfas�
    app.UseHsts(); // HTTP Strict Transport Security (G�venlik i�in �nemli)
}

// **8. HTTPS Y�nlendirme ve Statik Dosyalar**
app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot klas�r�ndeki statik dosyalara eri�imi etkinle�tirir

// **9. Kimlik Do�rulama ve Yetkilendirme**
app.UseRouting();
app.UseAuthentication(); // Kullan�c� kimlik do�rulamas�n� devreye al�r
app.UseAuthorization(); // Kullan�c� yetkilendirmesini devreye al�r

// **10. Swagger Middleware**
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KuaforYonetim API v1");
        c.RoutePrefix = "swagger"; // Swagger UI sadece /swagger adresinde eri�ilebilir
    });
}

// **11. Varsay�lan Rota Ayarlar�**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// **12. Uygulamay� �al��t�r**
app.Run();
