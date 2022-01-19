#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FictionalCustomers.Models;

namespace FictionalCustomers.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly FictionalCustomers.Models.fictionalCustomersContext _context;

        public DeleteModel(FictionalCustomers.Models.fictionalCustomersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClientCompany ClientCompany { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClientCompany = await _context.ClientCompanies.FirstOrDefaultAsync(m => m.Id == id);

            if (ClientCompany == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClientCompany = await _context.ClientCompanies.FindAsync(id);

            if (ClientCompany != null)
            {
                _context.ClientCompanies.Remove(ClientCompany);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
