
using Microsoft.AspNetCore.Mvc;

using PatientTrackingSystem.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PatientTrackingSystem.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;


        public HomeController(AppDbContext context)
        {
            _context = context;

        }


        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            ViewData["patients"] = _context.Patients.Count();
            ViewData["visits"] = _context.Visits.Count();
            ViewData["users"] = _context.Users.Count();

            return View();
        }



        [Authorize(Roles = "user")]
        public async Task<IActionResult> Visits(int id)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string nameIdentifier = claimUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isUser = await _context.Users.FirstOrDefaultAsync(x => x.username == nameIdentifier);
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.id_card == isUser.identity);

            List<Visit> user_visits = new List<Visit>();
            if (patient != null && patient.id == id)
            {
                var visits = await _context.Visits.OrderBy(v => v.Visit_Id).ToListAsync();


                

                foreach (var item in visits)
                {
                    if (item.Patient_Id == id)
                    {
                        user_visits.Add(item);
                    }
                }
                
            }
            
            return View(user_visits);


            
        }






    }
}
