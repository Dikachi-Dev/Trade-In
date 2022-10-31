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

namespace TradeIn.Pages.Admin.Components.BrandPage
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public DetailsModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public Brand Brand { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                Brand = brand;
            }
            return Page();
        }
    }
}
