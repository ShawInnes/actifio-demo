using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoApp.Web.Data;
using DemoApp.Web.Models;

namespace DemoApp.Web.Pages.People
{
    public class DetailsModel : PageModel
    {
        private readonly DemoApp.Web.Data.WideWorldImportersContext _context;

        public DetailsModel(DemoApp.Web.Data.WideWorldImportersContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
