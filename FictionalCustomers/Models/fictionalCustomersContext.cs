using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FictionalCustomers.Models
{
    public partial class fictionalCustomersContext : DbContext
    {
        public fictionalCustomersContext()
        {
        }

        public fictionalCustomersContext(DbContextOptions<fictionalCustomersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientCompany> ClientCompanies { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5VND1TV; Initial Catalog = fictionalCustomers;Integrated Security=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientCompany>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("company_name");

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contact_person");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EmploymentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("employment_status");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.SkillLevel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("skill_level");

                entity.Property(e => e.UserRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_role");

                entity.HasMany(d => d.Projects)
                    .WithMany(p => p.Employees)
                    .UsingEntity<Dictionary<string, object>>(
                        "EmployeesProject",
                        l => l.HasOne<Project>().WithMany().HasForeignKey("ProjectId").HasConstraintName("FK_employees_projects_Projects"),
                        r => r.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId").HasConstraintName("FK_employees_projects_Employees"),
                        j =>
                        {
                            j.HasKey("EmployeeId", "ProjectId");

                            j.ToTable("employees_projects");

                            j.IndexerProperty<int>("EmployeeId").HasColumnName("employee_id");

                            j.IndexerProperty<int>("ProjectId").HasColumnName("project_id");
                        });
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ProgressStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("progress_status");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("project_name");

                entity.Property(e => e.ReqSkillLevel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("req_skill_level");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.HasMany(d => d.Clients)
                    .WithMany(p => p.Projects)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProjectsClient",
                        l => l.HasOne<ClientCompany>().WithMany().HasForeignKey("ClientId").HasConstraintName("FK_projects_clients_ClientCompanies"),
                        r => r.HasOne<Project>().WithMany().HasForeignKey("ProjectId").HasConstraintName("FK_projects_clients_Projects"),
                        j =>
                        {
                            j.HasKey("ProjectId", "ClientId");

                            j.ToTable("projects_clients");

                            j.IndexerProperty<int>("ProjectId").HasColumnName("project_id");

                            j.IndexerProperty<int>("ClientId").HasColumnName("client_id");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
