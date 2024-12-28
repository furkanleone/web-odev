using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AccountController : Controller
{
    public static List<User> Users = new List<User>
{
    new User { Username = "b221210382@sakarya.edu.tr", Password = "sau", Role = "Admin" },
    new User { Username = "kullanici1@mail.com", Password = "1234", Role = "User" }
};

    public IActionResult Login()
    {
        return View(); // Login formu için bir view gösterecek
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = AccountController.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user == null)
        {
            ViewBag.Error = "Hatalı kullanıcı adı veya şifre.";
            return View();
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

        TempData["Message"] = "Giriş başarılı! Hoş geldiniz, " + user.Username + ".";
        return RedirectToAction("Index", "Home");
    }


    // HTTP GET: Üye Ol sayfasını göster
    [HttpGet]
    public IActionResult Register()
    {
        return View(); // Views/Account/Register.cshtml dosyasını döner
    }

    // HTTP POST: Üye Ol formundan gelen verileri işleyin
    [HttpPost]
    public IActionResult Register(string fullname, string email, string password, string confirmPassword)
    {
        // Şifre doğrulaması
        if (password != confirmPassword)
        {
            ViewBag.Error = "Şifreler eşleşmiyor.";
            return View(); // Hatalı girişte aynı sayfayı göster
        }

        // Yeni kullanıcıyı statik bir listeye eklemek (örnek)
        AccountController.Users.Add(new User
        {
            Username = email,
            Password = password,
            Role = "User"
        });

        // Kayıt başarılı, giriş sayfasına yönlendir
        return RedirectToAction("Login", "Account");
    }


    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        TempData["Message"] = "Oturumunuz başarıyla kapatıldı.";
        return RedirectToAction("Login");
    }
}
