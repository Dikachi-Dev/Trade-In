using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TradeIn.Data;
using TradeIn.Models;

namespace TradeIn.Pages.Admin.Components.GenerationPage
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public CreateModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Description"] = new SelectList(_context.Models, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public Generation Generation { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //  {
            //      return Page();
            //  }

            _context.Generations.Add(Generation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
