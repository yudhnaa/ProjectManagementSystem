using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataLayer.Domain
{
    public partial class ProjectManagementSystemContext : DbContext
    {
        public ProjectManagementSystemContext()
            : base("name=ProjectManagementSystemContext")
        {
        }

        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<EmailNotifications> EmailNotifications { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<NotificaitonTypes> NotificaitonTypes { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<ProjectMembers> ProjectMembers { get; set; }
        public virtual DbSet<ProjectPirority> ProjectPirority { get; set; }
        public virtual DbSet<ProjectPhases> ProjectPhases { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<ReportTypes> ReportTypes { get; set; }
        public virtual DbSet<RolePermissions> RolePermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<TaskComments> TaskComments { get; set; }
        public virtual DbSet<TaskDependencies> TaskDependencies { get; set; }
        public virtual DbSet<TaskHelpRequests> TaskHelpRequests { get; set; }
        public virtual DbSet<TaskHistory> TaskHistory { get; set; }
        public virtual DbSet<TaskPriority> TaskPriority { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TaskStatus> TaskStatus { get; set; }
        public virtual DbSet<TimeEntries> TimeEntries { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Employees1)
                .WithOptional(e => e.Employees2)
                .HasForeignKey(e => e.ReportsTo);

            modelBuilder.Entity<NotificaitonTypes>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.NotificaitonTypes)
                .HasForeignKey(e => e.NotificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permissions>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Permissions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectPirority>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.ProjectPirority)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Projects>()
                .Property(e => e.PercentComplete)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Projects>()
                .HasMany(e => e.ProjectMembers)
                .WithRequired(e => e.Projects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Projects>()
                .HasMany(e => e.ProjectPhases)
                .WithRequired(e => e.Projects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Projects>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Projects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectStatus>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.ProjectStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReportTypes>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.ReportTypes)
                .HasForeignKey(e => e.ReportType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskPriority>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.TaskPriority)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .Property(e => e.EstimatedHours)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Tasks>()
                .Property(e => e.ActualHours)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Tasks>()
                .Property(e => e.PercentComplete)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskComments)
                .WithRequired(e => e.Tasks)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskDependencies)
                .WithRequired(e => e.Tasks)
                .HasForeignKey(e => e.DependsOnTaskID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskDependencies1)
                .WithRequired(e => e.Tasks1)
                .HasForeignKey(e => e.TaskID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskHelpRequests)
                .WithRequired(e => e.Tasks)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskHistory)
                .WithRequired(e => e.Tasks)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.Tasks1)
                .WithOptional(e => e.Tasks2)
                .HasForeignKey(e => e.ParentTaskID);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TimeEntries)
                .WithRequired(e => e.Tasks)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskStatus>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.TaskStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Departments)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.ManagerID);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.EmailNotifications)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UploadedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.ProjectMembers)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Projects1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.ManagerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.TaskComments)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.TaskHelpRequests)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.RequestedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.TaskHelpRequests1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.RequestedTo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.TaskHistory)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.ChangedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Tasks1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.MainUserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.TimeEntries)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}
