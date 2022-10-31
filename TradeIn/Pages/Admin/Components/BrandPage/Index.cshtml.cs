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
    public class IndexModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public IndexModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public IList<Brand> Brand { get; set; } = default!;

        public async Task<JsonResult> OnGetBrand(int offset, int limit, string search, string order, string sort)
        {
            Brand = await _context.Brands.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                Brand = Brand.Where(x => (x.Description != null && x.Description.StartsWith(search)) || (x.Category != null && x.Category.StartsWith(search))).ToList();
            }
            int count = Brand.Count();
            List<object> aData = new List<object>();

            var mylist = Brand.Skip(offset).Take(limit).ToList();

            foreach (var item in mylist)

            {
                aData.Add(new
                {
                    Id = item.Id,
                    Category = item.Category,

                    Description = item.Description,

                    Delete = $"<a href=\"/Admin/Components/BrandPage/Delete?id={item.Id}\"  >Delete</a> ",
                    Details = $"<a href=\"/Admin/Components/BrandPage/Details?id={item.Id}\" >Details</a> ",
                    Edit = $"<a href=\"/Admin/Components/BrandPage/Edit?id={item.Id}\" >Edit</a> "
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
