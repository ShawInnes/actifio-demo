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
    public class IndexModel : PageModel
    {
        private readonly DemoApp.Web.Data.WideWorldImportersContext _context;

        public IndexModel(DemoApp.Web.Data.WideWorldImportersContext context)
        {
            _context = context;
        }

        public IList<Models.People> People { get;set; }

        public async Task OnGetAsync()
        {
            People = await _context.People
                .Include(p => p.LastEditedByNavigation).ToListAsync();
        }
    }
}
