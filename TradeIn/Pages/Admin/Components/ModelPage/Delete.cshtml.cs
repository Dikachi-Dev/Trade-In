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

namespace TradeIn.Pages.Admin.Components.ModelPage
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
        public Model Model { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var model = await _context.Models.FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                Model = model;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }
            var model = await _context.Models.FindAsync(id);

            if (model != null)
            {
                Model = model;
                _context.Models.Remove(Model);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
