using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoApp.Web.Data;
using DemoApp.Web.Models;

namespace DemoApp.Web.Pages.People
{
    public class CreateModel : PageModel
    {
        private readonly DemoApp.Web.Data.WideWorldImportersContext _context;

        public CreateModel(DemoApp.Web.Data.WideWorldImportersContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LastEditedBy"] = new SelectList(_context.People, "PersonId", "FullName");
            return Page();
        }

        [BindProperty]
        public Models.People People { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.People.Add(People);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}