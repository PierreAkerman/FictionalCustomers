#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FictionalCustomers.Models;

namespace FictionalCustomers.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly FictionalCustomers.Models.fictionalCustomersContext _context;

        public CreateModel(FictionalCustomers.Models.fictionalCustomersContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "ProjectName");
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public int[] ProjectID { get; set; } = null;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (int id in ProjectID)
            {
                Project p = _context.Projects.Single(p => p.Id == id);
                Employee.Projects.Add(p);
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
