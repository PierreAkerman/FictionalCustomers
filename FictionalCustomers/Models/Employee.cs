using System;
using System.Collections.Generic;

namespace FictionalCustomers.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string? SkillLevel { get; set; }
        public string? Email { get; set; }
        public string? UserRole { get; set; }
        public string? EmploymentStatus { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
