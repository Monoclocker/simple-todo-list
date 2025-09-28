using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Web.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated is true)
        {
            return RedirectToAction("Index", "Tasks");
        } 
        
        return Challenge(new AuthenticationProperties { RedirectUri = "/" });
    }

    public IActionResult Logout(string redirectUrl = "/")
    {
        return SignOut(new AuthenticationProperties { RedirectUri = redirectUrl },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }
}