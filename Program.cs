using KuaforYonetim.Data;
using KuaforYonetim.Models;
using KuaforYonetim.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// **1. OpenAI Servisi Ayarlarý**
var openAiApiKey = builder.Configuration["OpenAI:ApiKey"];

if (string.IsNullOrEmpty(openAiApiKey))
{
    throw new Exception("OpenAI API anahtarý yapýlandýrýlmadý. Lütfen appsettings.json dosyanýza 'OpenAI:ApiKey' ekleyin.");
}

builder.Services.AddHttpClient("OpenAI", client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["OpenAI:ApiKey"]}");
});


// **2. Veritabaný Baðlantýsý**
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new Exception("Veritabaný baðlantý dizesi bulunamadý. Lütfen appsettings.json dosyanýza 'ConnectionStrings:DefaultConnection' ekleyin.");
    }
    options.UseSqlServer(connectionString);
});

// **3. Data Servisi ve Singleton Baðýmlýlýklarý**
builder.Services.AddSingleton<DataService>();

// **4. MVC Servislerini ve Yetkilendirmeyi Ekle**
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login"; // Giriþ sayfasý yönlendirmesi
        options.AccessDeniedPath = "/Account/AccessDenied"; // Yetki reddedilme sayfasý
    });

builder.Services.AddAuthorization();

// **5. Swagger Dokümantasyonu**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swagger için gerekli servisler

// **6. Uygulamayý Yapýlandýrma**
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
// **7. Hata Yönetimi**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Kullanýcý dostu hata sayfasý
    app.UseHsts(); // HTTP Strict Transport Security (Güvenlik için önemli)
}

// **8. HTTPS Yönlendirme ve Statik Dosyalar**
app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot klasöründeki statik dosyalara eriþimi etkinleþtirir

// **9. Kimlik Doðrulama ve Yetkilendirme**
app.UseRouting();
app.UseAuthentication(); // Kullanýcý kimlik doðrulamasýný devreye alýr
app.UseAuthorization(); // Kullanýcý yetkilendirmesini devreye alýr

// **10. Swagger Middleware**
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KuaforYonetim API v1");
        c.RoutePrefix = "swagger"; // Swagger UI sadece /swagger adresinde eriþilebilir
    });
}

// **11. Varsayýlan Rota Ayarlarý**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// **12. Uygulamayý Çalýþtýr**
app.Run();
