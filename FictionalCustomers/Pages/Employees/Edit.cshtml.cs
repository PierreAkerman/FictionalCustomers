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

namespace FictionalCustomers.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly FictionalCustomers.Models.fictionalCustomersContext _context;

        public EditModel(FictionalCustomers.Models.fictionalCustomersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }
        [BindProperty]
        public int[] ProjectID { get; set; } = null;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees
                .Include(e => e.Projects)
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "ProjectName");

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Employee).State = EntityState.Modified;

            List<Project> Projects_temp = new();
            foreach (int id in ProjectID)
            {
                Project p = _context.Projects.Single(p => p.Id == id);
                Projects_temp.Add(p);
            }
            _context.Entry(Employee).Collection(e => e.Projects).Load();
            Employee.Projects = Projects_temp;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id))
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

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
