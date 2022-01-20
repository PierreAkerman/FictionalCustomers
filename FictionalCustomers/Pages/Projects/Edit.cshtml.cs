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
        [BindProperty]
        public int[] ClientID { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.Employees)
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewData["EmployeeID"] = new SelectList(_context.Employees.OrderBy(e => e.FirstName), "Id", "FullName");
            ViewData["ClientID"] = new SelectList(_context.ClientCompanies.OrderBy(c => c.CompanyName), "Id", "CompanyName");

            if (Project == null)
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

            _context.Attach(Project).State = EntityState.Modified;

            List<Employee> Employees_temp = new();
            foreach (int id in EmployeeID)
            {
                Employee e = _context.Employees.Single(e => e.Id == id);
                Employees_temp.Add(e);
            }
            _context.Entry(Project).Collection(c => c.Employees).Load();

            List<ClientCompany> Clients_temp = new();
            foreach (int id in ClientID)
            {
                ClientCompany c = _context.ClientCompanies.Single(c => c.Id == id);
                Clients_temp.Add(c);
            }
            _context.Entry(Project).Collection(c =>c.Clients).Load();

            Project.Employees = Employees_temp;
            Project.Clients = Clients_temp;

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
