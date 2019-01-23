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

namespace DemoApp.Web.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly DemoApp.Web.Data.WideWorldImportersContext _context;

        public EditModel(DemoApp.Web.Data.WideWorldImportersContext context)
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
           ViewData["BackorderOrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
           ViewData["ContactPersonId"] = new SelectList(_context.People, "PersonId", "FullName");
           ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
           ViewData["LastEditedBy"] = new SelectList(_context.People, "PersonId", "FullName");
           ViewData["PickedByPersonId"] = new SelectList(_context.People, "PersonId", "FullName");
           ViewData["SalespersonPersonId"] = new SelectList(_context.People, "PersonId", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Orders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(Orders.OrderId))
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

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
