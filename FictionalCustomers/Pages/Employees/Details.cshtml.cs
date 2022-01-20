#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FictionalCustomers.Models;

namespace FictionalCustomers.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly FictionalCustomers.Models.fictionalCustomersContext _context;

        public DetailsModel(FictionalCustomers.Models.fictionalCustomersContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees
                .Include(e => e.Projects)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
