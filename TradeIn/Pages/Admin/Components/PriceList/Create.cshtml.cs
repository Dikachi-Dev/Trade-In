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

namespace TradeIn.Pages.Admin.Components.PriceList
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
            return Page();
        }

        public async Task<JsonResult> OnGetBrands(string Id)
        {
            var Filterbrands = await _context.Brands.Where(m => m.Category == Id).ToListAsync();
            //get MedianTimes with Id
            //MedianTimes =.....
            return new JsonResult(Filterbrands);
        }

        public async Task<JsonResult> OnGetModels(int Id)
        {
            var Filtermodels = await _context.Models.Where(m => m.BrandId == Id).ToListAsync();
            //get MedianTimes with Id
            //MedianTimes =.....
            return new JsonResult(Filtermodels);
        }

        public async Task<JsonResult> OnGetGen(int Id)
        {
            var Filtergens = await _context.Generations.Where(m => m.ModelId == Id).ToListAsync();
            //get MedianTimes with Id
            //MedianTimes =.....
            return new JsonResult(Filtergens);
        }

        [BindProperty]
        public Valuation Valuation { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Valuations.Add(Valuation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
