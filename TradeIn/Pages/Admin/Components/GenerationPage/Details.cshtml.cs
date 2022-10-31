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

namespace TradeIn.Pages.Admin.Components.GenerationPage
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public DetailsModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public Generation Generation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Generations == null)
            {
                return NotFound();
            }

            var generation = await _context.Generations.FirstOrDefaultAsync(m => m.Id == id);
            if (generation == null)
            {
                return NotFound();
            }
            else
            {
                Generation = generation;
            }
            return Page();
        }
    }
}
