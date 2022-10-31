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
    public class IndexModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public IndexModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public IList<Model> Model { get; set; } = default!;

        public async Task<JsonResult> OnGetModels(int offset, int limit, string search, string order, string sort)
        {
            //brand = await _context.Brands.FirstOrDefaultAsync(x=> x.Id == )
            Model = await _context.Models
                .Include(m => m.Brand).ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                Model = Model.Where(x => (x.Description != null && x.Description.StartsWith(search))).ToList();
            }
            int count = Model.Count();
            List<object> aData = new List<object>();

            var mylist = Model.Skip(offset).Take(limit).ToList();

            foreach (var item in mylist)

            {
                aData.Add(new
                {
                    Id = item.Id,
                    Brand = Helper.getBrand(item.Brand.Id),

                    Description = item.Description,

                    Delete = $"<a href=\"/Admin/Components/ModelPage/Delete?id={item.Id}\"  >Delete</a> ",
                    Details = $"<a href=\"/Admin/Components/ModelPage/Details?id={item.Id}\" >Details</a> ",
                    Edit = $"<a href=\"/Admin/Components/ModelPage/Edit?id={item.Id}\" >Edit</a> "
                });
            }
            return new JsonResult(
                new
                {
                    total = count,
                    rows = aData.ToArray()
                });
        }

        //public async Task OnGetAsync()
        //{
        //    if (_context.Models != null)
        //    {
        //        Model = await _context.Models
        //        .Include(m => m.Brand).ToListAsync();
        //    }
        //}
    }
}
