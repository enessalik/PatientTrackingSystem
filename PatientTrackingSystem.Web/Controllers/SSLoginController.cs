using Microsoft.AspNetCore.Mvc;
using PatientTrackingSystem.Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace PatientTrackingSystem.Web.Controllers
{
    public class SSLoginController : Controller
    {
        private readonly AppDbContext _context;


        public SSLoginController(AppDbContext context)
        {
            _context = context;

        }


        public IActionResult AccessDenied()
        {

            return View();
        }

        public async Task<IActionResult> Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            
            if (claimUser.Identity.IsAuthenticated)
                if (claimUser.IsInRole("admin"))
                    return RedirectToAction("Index", "Home");
                else if (claimUser.IsInRole("user"))
                {
                    string nameIdentifier = claimUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var isUser = await _context.Users.FirstOrDefaultAsync(x => x.username == nameIdentifier);
                    var patient = await _context.Patients.FirstOrDefaultAsync(x => x.id_card == isUser.identity);

                    if (patient != null)
                        return RedirectToAction("Visits", "Home", new { id = patient.id });
                    else
                        return RedirectToAction("Visits", "Home");
                }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] string UserName, [FromForm] string Password)
        {

            var user = await _context.Users.FirstOrDefaultAsync(m => m.username == UserName);

            if (user != null && user.username == UserName)
            {
                if (user.password == Password)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, UserName),
                        new Claim(ClaimTypes.Role,user.role)
                    };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                        , new ClaimsPrincipal(claimsIdentity), properties);
                    if (user.role == "admin")
                        return RedirectToAction("Index", "Home");
                    else if (user.role == "user")
                    {
                        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.id_card == user.identity);
                        if(patient != null)
                            return RedirectToAction("Visits", "Home", new { id = patient.id });
                        else
                            return RedirectToAction("Visits", "Home");
                    }
                  
                        

                }else
                {
                    TempData["Alert2"] = "Password is incorrect.";
                    
                    return View();
                }
                
                
            }
            TempData["Alert"] = "Username is not found.";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/SSLogin/Login");


        }

    }
}
