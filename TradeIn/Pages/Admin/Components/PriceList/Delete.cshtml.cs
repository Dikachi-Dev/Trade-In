using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TradeIn.Data;
using TradeIn.Models;

namespace TradeIn.Pages.Admin.Components.PriceList
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public DeleteModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Valuation Valuation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Valuations == null)
            {
                return NotFound();
            }

            var valuation = await _context.Valuations.FirstOrDefaultAsync(m => m.Id == id);

            if (valuation == null)
            {
                return NotFound();
            }
            else
            {
                Valuation = valuation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Valuations == null)
            {
                return NotFound();
            }
            var valuation = await _context.Valuations.FindAsync(id);

            if (valuation != null)
            {
                Valuation = valuation;
                _context.Valuations.Remove(Valuation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
