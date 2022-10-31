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
    public class IndexModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public IndexModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public IList<Valuation> Valuation { get; set; } = default!;

        public async Task<JsonResult> OnGetPriceList(int offset, int limit, string search, string order, string sort)
        {
            //brand = await _context.Brands.FirstOrDefaultAsync(x=> x.Id == )
            Valuation = await _context.Valuations.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                Valuation = Valuation.Where(x => (x.Category != null && x.Category.StartsWith(search)) || (x.Condition != null && x.Condition.StartsWith(search))).ToList();
            }
            int count = Valuation.Count();
            List<object> aData = new List<object>();

            var mylist = Valuation.Skip(offset).Take(limit).ToList();

            foreach (var item in mylist)

            {
                aData.Add(new
                {
                    Id = item.Id,
                    Brand = Helper.getBrand(item.Brand),
                    Category = item.Category,
                    Price = item.Price,
                    Condition = item.Condition,

                    Generation = Helper.getGen(item.Generation),

                    Model = Helper.getModel(item.Model),

                    Delete = $"<a href=\"/Admin/Components/PriceList/Delete?id={item.Id}\"  >Delete</a> ",
                    Details = $"<a href=\"/Admin/Components/PriceList/Details?id={item.Id}\" >Details</a> ",
                    Edit = $"<a href=\"/Admin/Components/PriceList/Edit?id={item.Id}\" >Edit</a> "
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
