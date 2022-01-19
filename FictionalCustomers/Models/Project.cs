using System;
using System.Collections.Generic;

namespace FictionalCustomers.Models
{
    public partial class Project
    {
        public Project()
        {
            Clients = new HashSet<ClientCompany>();
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ReqSkillLevel { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProgressStatus { get; set; } = null!;
        public string Location { get; set; } = null!;

        public virtual ICollection<ClientCompany> Clients { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
