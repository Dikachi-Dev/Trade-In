using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TradeIn.Data;
using TradeIn.Models;

namespace TradeIn.Pages.Admin.Components.GenerationPage
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public EditModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            Generation = generation;
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Generation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenerationExists(Generation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GenerationExists(int id)
        {
            return _context.Generations.Any(e => e.Id == id);
        }
    }
}
