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

namespace TradeIn.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public IndexModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public IList<EstimateRef> EstimateRef { get; set; }

        public async Task<JsonResult> OnGetEstimate(int offset, int limit, string search, string order, string sort)
        {
            //if (_context.EstimateRefs != null)
            //{
            //}
            EstimateRef = await _context.EstimateRefs.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                EstimateRef = EstimateRef.Where(x => (x.Refnumber != null && x.Refnumber.StartsWith(search)) || (x.Category != null && x.Category.StartsWith(search))).ToList();
            }
            int count = EstimateRef.Count();
            List<object> aData = new List<object>();

            var mylist = EstimateRef.Skip(offset).Take(limit).ToList();

            foreach (var item in mylist)

            {
                aData.Add(new
                {
                    Id = item.Id,
                    Brand = Helper.getBrand(item.Brand),
                    Category = item.Category,
                    Amount = item.Amount,
                    Condition = item.Condition,
                    Refnumber = item.Refnumber,
                    Generation = Helper.getGen(item.Generation),
                    OtherItem = item.OtherItem,
                    Name = item.FirstName + item.LastName,
                    Model = Helper.getModel(item.Model),
                    EstimateDate = item.Estimatedate,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    Delete = $"<a href=\"/Admin/Delete?id={item.Id}\"  >Delete</a> "
                });
            }
            return new JsonResult(
                new
                {
                    total = count,
                    rows = aData.ToArray()
                });

            //return Page();
        }
    }
}
