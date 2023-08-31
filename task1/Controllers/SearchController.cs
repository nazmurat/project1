using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using task1.Models.Pages;

namespace task1.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: /Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var searchString = model.SearchString;
                // Осуществите логику поиска здесь и передайте результаты в представление
                var searchResults = _context.Patient
                    .Where(p => p.IIN.Contains(searchString) || p.FullName.Contains(searchString))
                    .Select(p => new SearchResult
                    {
                        Title = p.FullName,
                        Description = p.IIN
                    })
                    .ToList();

                model.SearchResults = searchResults;
            }

            return View(model);
        }
    }
}