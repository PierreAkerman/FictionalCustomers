using FictionalCustomers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FictionalCustomers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly fictionalCustomersContext _context;

        public IndexModel(fictionalCustomersContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }
    }
}