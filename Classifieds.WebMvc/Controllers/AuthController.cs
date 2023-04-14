using Classifieds.WebMvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Classifieds.WebMvc.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login (string returnUrl)
        {
            returnUrl ??= Url.Content("/");
            ViewBag.ReturnUrl = returnUrl;
            InputModel inputModel = new InputModel();
            return View(inputModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(InputModel model,string returnUrl=null) {

            if(model.UserName=="chanoo" && model.Password == "password")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, model.UserName),
                    new Claim(ClaimTypes.Name,"USERNAME HERE"),
                    new Claim(ClaimTypes.Role,"ROLE"),
                    new Claim("RandomDataPoint","RandomValue")
                };
                var identityUser=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var principal =new ClaimsPrincipal(identityUser);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal,new AuthenticationProperties{ IsPersistent=model.RememberMe});
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Unauthorized();
            }

          
        
        }
        public IActionResult Create() {
        return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); 
            return LocalRedirect("/");
        }
    }
}
