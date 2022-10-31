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

namespace TradeIn.Pages.Admin.Components.ModelPage
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public CreateModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            //ViewData["Description"] = new SelectList(_context.Brands, "Id", "Description");
            //ViewData["Category"] = new SelectList(_context.Brands, "Id", "Category");
            Brands = await _context.Brands.ToListAsync();
            return Page();
        }

        [BindProperty]
        public Model Model { get; set; } = default!;

        public List<Brand> Brands { get; private set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (ModelState.IsValid)
            //{
            _context.Models.Add(Model);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            //}
            //else
            //    return Page();
        }
    }
}
