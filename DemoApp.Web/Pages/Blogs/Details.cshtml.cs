using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoApp.Web.Models;

namespace DemoApp.Web.Pages.Blogs
{
  public class DetailsModel : PageModel
  {
    private readonly DemoApp.Web.Data.BloggingContext _context;

    public DetailsModel(DemoApp.Web.Data.BloggingContext context)
    {
      _context = context;
    }

    public Blog Blog { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Blog = await _context.Blogs.FirstOrDefaultAsync(m => m.BlogId == id);

      if (Blog == null)
      {
        return NotFound();
      }
      return Page();
    }
  }
}
