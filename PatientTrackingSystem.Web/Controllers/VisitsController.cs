using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatientTrackingSystem.Web.Models;

namespace PatientTrackingSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class VisitsController : Controller
    {
        private readonly AppDbContext _context;
        
        public VisitsController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: Visit
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Visits.OrderBy(v => v.Visit_Id).ToListAsync());
        }
       
        // GET: Visit/Create
        public IActionResult Create()
        {
            ViewData["Patient_Id"] = new SelectList(_context.Patients.OrderBy(p => p.id), "id", "name_surname");
            return View();
        }

        // POST: Visit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw($"call public.insert_visit({visit.Patient_Id}, '{visit.Visit_Date}', '{visit.Doctor}', '{visit.Complaint}', '{visit.Treatment}')");
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Patient_Id"] = new SelectList(_context.Patients.OrderBy(p => p.id), "id", "name_surname");
            return View(visit);
        }

        // GET: Visit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.Visit_Id == id);
            if (visit == null)
            {
                return NotFound();
            }
            
            return View(visit);
        }


        // GET: Visit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewData["Patient_Id"] = new SelectList(_context.Patients.OrderBy(p => p.id), "id", "name_surname", visit.Patient_Id);
            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Visit visit)
        {
    
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw($"call public.update_visit({visit.Visit_Id},{visit.Patient_Id}, '{visit.Visit_Date}', '{visit.Doctor}', '{visit.Complaint}', '{visit.Treatment}')");
      
                await _context.SaveChangesAsync();        
                return RedirectToAction(nameof(Index));
            }
            ViewData["Patient_Id"] = new SelectList(_context.Patients.OrderBy(p => p.id), "id", "name_surname");
            return View(visit);
        }

        // GET: Visit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visits == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(m => m.Visit_Id == id);
            if (visit == null)
            {
                return NotFound();
            }
           
            return View(visit);
        }

        // POST: Visit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visits == null)
            {
                return Problem("Entity set 'AppDbContext.Visits'  is null.");
            }
            var visit = await _context.Visits.FindAsync(id);
            if (visit != null)
            {
                _context.Database.ExecuteSqlRaw($"call public.delete_visit({id})");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        
        public async Task<IActionResult> Visits(int? id)
        {
            var visits = await _context.Visits.OrderBy(v => v.Visit_Id).ToListAsync();


            List<Visit> user_visits = new List<Visit>();
            
            foreach (var item in visits)
            {
                if(item.Patient_Id == id)
                {
                    user_visits.Add(item);
                }
            }


            return View(user_visits);
        }













        private bool VisitExists(int id)
        {
          return (_context.Visits?.Any(e => e.Visit_Id == id)).GetValueOrDefault();
        }
    }
}
