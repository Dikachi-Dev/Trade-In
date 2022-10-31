using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TradeIn.Data;
using TradeIn.Models;

namespace TradeIn.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public DeleteModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EstimateRef EstimateRef { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EstimateRefs == null)
            {
                return NotFound();
            }

            var estimateref = await _context.EstimateRefs.FirstOrDefaultAsync(m => m.Id == id);

            if (estimateref == null)
            {
                return NotFound();
            }
            else
            {
                EstimateRef = estimateref;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.EstimateRefs == null)
            {
                return NotFound();
            }
            var estimateref = await _context.EstimateRefs.FindAsync(id);

            if (estimateref != null)
            {
                EstimateRef = estimateref;
                _context.EstimateRefs.Remove(EstimateRef);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
