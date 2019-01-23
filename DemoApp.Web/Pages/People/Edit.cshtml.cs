using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoApp.Web.Data;
using DemoApp.Web.Models;

namespace DemoApp.Web.Pages.People
{
    public class EditModel : PageModel
    {
        private readonly DemoApp.Web.Data.WideWorldImportersContext _context;

        public EditModel(DemoApp.Web.Data.WideWorldImportersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.People People { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            People = await _context.People
                .Include(p => p.LastEditedByNavigation).FirstOrDefaultAsync(m => m.PersonId == id);

            if (People == null)
            {
                return NotFound();
            }
           ViewData["LastEditedBy"] = new SelectList(_context.People, "PersonId", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(People).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(People.PersonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PeopleExists(int id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }
    }
}
