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

namespace FictionalCustomers.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly FictionalCustomers.Models.fictionalCustomersContext _context;

        public EditModel(FictionalCustomers.Models.fictionalCustomersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public int[] EmployeeID { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewData["EmployeeID"] = new SelectList(_context.Employees, "Id", "FullName");

            if (Project == null)
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

            _context.Attach(Project).State = EntityState.Modified;

            List<Employee> Employees_temp = new();
            foreach (int id in EmployeeID)
            {
                Employee e = _context.Employees.Single(e => e.Id == id);
                Employees_temp.Add(e);
            }

            _context.Entry(Project).Collection(c => c.Employees).Load();
            Project.Employees = Employees_temp;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.Id))
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

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
