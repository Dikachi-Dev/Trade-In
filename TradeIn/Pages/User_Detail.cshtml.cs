using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TradeIn.Data;
using TradeIn.Models;

namespace TradeIn.Pages
{
    public class User_DetailModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public User_DetailModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserInfo UserInfo { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            UserInfo.Email = "";
            UserInfo.PhoneNumber = "";

            _context.UserInfos.Add(UserInfo);
            await _context.SaveChangesAsync();
            // await Response.WriteAsync("<script>alert('We will be in touch');</script>");

            return RedirectToPage("./ThankYou");
        }
    }
}
