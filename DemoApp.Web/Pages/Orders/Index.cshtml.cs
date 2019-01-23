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
    public class IndexModel : PageModel
    {
        private readonly DemoApp.Web.Data.WideWorldImportersContext _context;

        public IndexModel(DemoApp.Web.Data.WideWorldImportersContext context)
        {
            _context = context;
        }

        public IList<Models.Orders> Orders { get;set; }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.BackorderOrder)
                .Include(o => o.ContactPerson)
                .Include(o => o.Customer)
                .Include(o => o.LastEditedByNavigation)
                .Include(o => o.PickedByPerson)
                .Include(o => o.SalespersonPerson).ToListAsync();
        }
    }
}
