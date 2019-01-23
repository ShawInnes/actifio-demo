using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoApp.Web.Models;

namespace DemoApp.Web.Pages.Blogs
{
  public class CreateModel : PageModel
  {
    private readonly DemoApp.Web.Data.BloggingContext _context;

    public CreateModel(DemoApp.Web.Data.BloggingContext context)
    {
      _context = context;
    }

    public IActionResult OnGet()
    {
      return Page();
    }

    [BindProperty]
    public Blog Blog { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      _context.Blogs.Add(Blog);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}