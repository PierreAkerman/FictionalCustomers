#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FictionalCustomers.Models;

namespace FictionalCustomers.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly FictionalCustomers.Models.fictionalCustomersContext _context;

        public EditModel(FictionalCustomers.Models.fictionalCustomersContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ClientCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientCompanyExists(ClientCompany.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClientCompanyExists(int id)
        {
            return _context.ClientCompanies.Any(e => e.Id == id);
        }
    }
}
