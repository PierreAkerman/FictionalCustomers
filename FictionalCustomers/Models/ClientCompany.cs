using System;
using System.Collections.Generic;

namespace FictionalCustomers.Models
{
    public partial class ClientCompany
    {
        public ClientCompany()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
