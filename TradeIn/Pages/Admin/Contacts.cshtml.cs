using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TradeIn.Data;
using TradeIn.Models;

namespace TradeIn.Pages.Admin
{
    public class ContactsModel : PageModel
    {
        private readonly TradeIn.Data.TradeInContext _context;

        public ContactsModel(TradeIn.Data.TradeInContext context)
        {
            _context = context;
        }

        //public IList<Contact> Contact { get; set; } = default!;
        public Contact _contact { get; set; } = default!;

        public async Task<JsonResult> OnGetContact(int offset, int limit, string search, string order, string sort)
        {
            var Contact = await _context.Contacts.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                Contact = Contact.Where(x => (x.Name != null && x.Name.StartsWith(search)) || (x.Email != null && x.Email.StartsWith(search)) || (x.PhoneNumber != null && x.PhoneNumber.StartsWith(search))).ToList();
            }
            int count = Contact.Count();
            List<object> aData = new List<object>();

            var mylist = Contact.Skip(offset).Take(limit).ToList();

            foreach (var item in mylist)
            {
                aData.Add(new
                {
                    Id = item.Id,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    Delete = $"<a href=\"#\" onclick=\"deleteContact('{item.Id}')\" >Delete</a> "
                });
            }
            return new JsonResult(
                new
                {
                    total = count,
                    rows = aData.ToArray()
                });

            //return new JsonResult(Contact);
        }

        public async Task<JsonResult> OnGetDelete(string id)
        {
            if (id == null || _context.Contacts == null)
            {
                return new JsonResult(false);
            }
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (contact != null)
            {
                _contact = contact;
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
            return new JsonResult(true);
        }
    }
}
