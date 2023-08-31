using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using card.Models;
using task1.Models;
using task1.Models.Pages;

namespace task1.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return _context.Patient != null ?
                        View(await _context.Patient.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Patient'  is null.");
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IIN,FullName,Address,Phone")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IIN,FullName,Address,Phone")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Patient'  is null.");
            }
            var patient = await _context.Patient.FindAsync(id);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return (_context.Patient?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // GET: Patients/Search
        public IActionResult Search()
        {
            var model = new SearchViewModel(); // Инициализация модели
            return View(model); // Передача модели в представление
        }

        // POST: Patients/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var searchString = model.SearchString;
                var validationContext = new ValidationContext(new { IIN = searchString, FullName = searchString }, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(validationContext.ObjectInstance, validationContext, validationResults, validateAllProperties: true))
                {
                    foreach (var validationResult in validationResults)
                    {
                        ModelState.AddModelError("", validationResult.ErrorMessage ?? "Validation error");
                    }

                    var allPatients = _context.Patient.ToList();
                    return View("Index", allPatients);
                }

                var matchingPatients = _context.Patient
                    .Where(p => p.IIN.Contains(searchString) || p.FullName.Contains(searchString))
                    .ToList();

                return View("Index", matchingPatients);
            }

            var emptyPatients = new List<Patient>();
            return View("Index", emptyPatients);
        }

   

}
}
