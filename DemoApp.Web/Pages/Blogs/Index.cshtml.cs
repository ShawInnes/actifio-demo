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
  public class IndexModel : PageModel
  {
    private readonly DemoApp.Web.Data.BloggingContext _context;

    public IndexModel(DemoApp.Web.Data.BloggingContext context)
    {
      _context = context;
    }

    public IList<Blog> Blog { get; set; }

    public async Task OnGetAsync()
    {
      Blog = await _context.Blogs.ToListAsync();
    }
  }
}
