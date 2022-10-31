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
    public class IndexModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public IndexModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public IList<Generation> Generation { get; set; } = default!;

        public async Task<JsonResult> OnGetGeneration(int offset, int limit, string search, string order, string sort)
        {
            //brand = await _context.Brands.FirstOrDefaultAsync(x=> x.Id == )
            Generation = await _context.Generations
                .Include(m => m.Model).ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                Generation = Generation.Where(x => (x.Description != null && x.Description.StartsWith(search))).ToList();
            }
            int count = Generation.Count();
            List<object> aData = new List<object>();

            var mylist = Generation.Skip(offset).Take(limit).ToList();

            foreach (var item in mylist)

            {
                aData.Add(new
                {
                    Id = item.Id,
                    Model = Helper.getModel(item.ModelId),

                    Description = item.Description,

                    Delete = $"<a href=\"/Admin/Components/GenerationPage/Delete?id={item.Id}\"  >Delete</a> ",
                    Details = $"<a href=\"/Admin/Components/GenerationPage/Details?id={item.Id}\" >Details</a> ",
                    Edit = $"<a href=\"/Admin/Components/GenerationPage/Edit?id={item.Id}\" >Edit</a> "
                });
            }
            return new JsonResult(
                new
                {
                    total = count,
                    rows = aData.ToArray()
                });
        }
    }
}
