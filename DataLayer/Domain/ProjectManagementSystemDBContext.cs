using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataLayer.Domain
{
    public partial class ProjectManagementSystemDBContext : DbContext
    {
        public ProjectManagementSystemDBContext()
            : base("name=ProjectManagementSystemDBContext")
        {
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<EmailNotification> EmailNotifications { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<ProjectMemberRolePermission> ProjectMemberRolePermissions { get; set; }
        public virtual DbSet<ProjectMemberRole> ProjectMemberRoles { get; set; }
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; }
        public virtual DbSet<ProjectPriority> ProjectPriorities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportType> ReportTypes { get; set; }
        public virtual DbSet<TaskComment> TaskComments { get; set; }
        public virtual DbSet<TaskDependency> TaskDependencies { get; set; }
        public virtual DbSet<TaskHelpRequest> TaskHelpRequests { get; set; }
        public virtual DbSet<TaskHistory> TaskHistories { get; set; }
        public virtual DbSet<TaskPriority> TaskPriorities { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
        public virtual DbSet<TimeEntry> TimeEntries { get; set; }
        public virtual DbSet<UserRolePermission> UserRolePermissions { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<NotificationType>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.NotificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.ProjectMemberRolePermissions)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.UserRolePermissions)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectMemberRole>()
                .HasMany(e => e.ProjectMemberRolePermissions)
                .WithRequired(e => e.ProjectMemberRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectMemberRole>()
                .HasMany(e => e.ProjectMembers)
                .WithRequired(e => e.ProjectMemberRole)
                .HasForeignKey(e => e.RoleInProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectPriority>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.ProjectPriority)
                .HasForeignKey(e => e.PriorityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.PercentComplete)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.ProjectMembers)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectStatus>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.ProjectStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReportType>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.ReportType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskPriority>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.TaskPriority)
                .HasForeignKey(e => e.PriorityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.EstimatedHours)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Task>()
                .Property(e => e.ActualHours)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Task>()
                .Property(e => e.PercentComplete)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TaskComments)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TaskDependencies)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.DependsOnTaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TaskDependencies1)
                .WithRequired(e => e.Task1)
                .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TaskHelpRequests)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TaskHistories)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Tasks1)
                .WithOptional(e => e.Task1)
                .HasForeignKey(e => e.ParentTaskId);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TimeEntries)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskStatus>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.TaskStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>()
                .HasMany(e => e.UserRolePermissions)
                .WithRequired(e => e.UserRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Departments)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.ManagerId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.EmailNotifications)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UploadedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProjectMembers)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Projects1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.ManagerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TaskComments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TaskHelpRequests)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.RequestedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TaskHelpRequests1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.RequestedTo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TaskHistories)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ChangedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AssignedUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tasks1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TimeEntries)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
