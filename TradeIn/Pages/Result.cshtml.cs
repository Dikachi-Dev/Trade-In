using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TradeIn.Data;
using TradeIn.Models;

namespace TradeIn.Pages
{
    public class ResultModel : PageModel
    {
        private TradeInContext _context;

        public ResultModel(TradeInContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EstimateRef estimate { get; set; } = default!;

        public void OnGet(string refId)
        {
            var estima = _context.EstimateRefs.FirstOrDefault(x => x.Refnumber == refId);

            estimate = estima!;
            // Console.WriteLine(estimate?.Amount);

            //try { Estimate = await _context.EstimateRef.Where(e => e.Refnumber == refId).FirstAsync(); }
            //catch (Exception e) { Console.WriteLine(e); }

            //Console.WriteLine(Estimate.Amount);
        }
    }
}
