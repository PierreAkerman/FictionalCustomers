#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FictionalCustomers.Models;

namespace FictionalCustomers.Pages.Projects
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["ClientID"] = new SelectList(_context.ClientCompanies, "Id", "CompanyName");
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public int[] EmployeeID { get; set; }
        [BindProperty]
        public int[] ClientID { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            List<Employee> Employee_temp = new();
            foreach (int id in EmployeeID)
            {
                Employee e = _context.Employees.Single(e => e.Id == id);
                Project.Employees.Add(e);
            }

            List<ClientCompany> Client_temp = new();
            foreach (int id in ClientID)
            {
                ClientCompany c = _context.ClientCompanies.Single(c => c.Id == id);
                Project.Clients.Add(c);
            }

            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
