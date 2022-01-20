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
    public class IndexModel : PageModel
    {
        private readonly FictionalCustomers.Models.fictionalCustomersContext _context;

        public IndexModel(FictionalCustomers.Models.fictionalCustomersContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }

        public async Task OnGetAsync()
        {
            Employee = await _context.Employees.OrderBy(e => e.FirstName)
                .Include(e => e.Projects).ToListAsync();
        }
    }
}
