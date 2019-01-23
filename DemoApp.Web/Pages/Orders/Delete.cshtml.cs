using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoApp.Web.Data;
using DemoApp.Web.Models;

namespace DemoApp.Web.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly DemoApp.Web.Data.WideWorldImportersContext _context;

        public DeleteModel(DemoApp.Web.Data.WideWorldImportersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Orders Orders { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orders = await _context.Orders
                .Include(o => o.BackorderOrder)
                .Include(o => o.ContactPerson)
                .Include(o => o.Customer)
                .Include(o => o.LastEditedByNavigation)
                .Include(o => o.PickedByPerson)
                .Include(o => o.SalespersonPerson).FirstOrDefaultAsync(m => m.OrderId == id);

            if (Orders == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orders = await _context.Orders.FindAsync(id);

            if (Orders != null)
            {
                _context.Orders.Remove(Orders);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
