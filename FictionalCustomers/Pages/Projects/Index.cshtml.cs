#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FictionalCustomers.Models;

namespace FictionalCustomers.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly FictionalCustomers.Models.fictionalCustomersContext _context;

        public IndexModel(FictionalCustomers.Models.fictionalCustomersContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }

        public async Task OnGetAsync()
        {
            Project = await _context.Projects
                .Include(p => p.Employees)
                .Include(p => p.Clients).ToListAsync();
        }
    }
}
