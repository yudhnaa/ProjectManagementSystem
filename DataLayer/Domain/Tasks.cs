namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tasks()
        {
            Files = new HashSet<Files>();
            TaskComments = new HashSet<TaskComments>();
            TaskDependencies = new HashSet<TaskDependencies>();
            TaskDependencies1 = new HashSet<TaskDependencies>();
            TaskHelpRequests = new HashSet<TaskHelpRequests>();
            TaskHistory = new HashSet<TaskHistory>();
            Tasks1 = new HashSet<Tasks>();
            TimeEntries = new HashSet<TimeEntries>();
        }

        [Key]
        public int TaskID { get; set; }

        [StringLength(20)]
        public string TaskCode { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int ProjectID { get; set; }

        public int MainUserID { get; set; }

        public int StatusID { get; set; }

        public int PriorityID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DueDate { get; set; }

        public decimal? EstimatedHours { get; set; }

        public decimal? ActualHours { get; set; }

        public decimal? PercentComplete { get; set; }

        public int? ParentTaskID { get; set; }

        public DateTime? LastStatusChangeDate { get; set; }

        [StringLength(500)]
        public string BlockReason { get; set; }

        public bool? IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Files> Files { get; set; }

        public virtual Projects Projects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskComments> TaskComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskDependencies> TaskDependencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskDependencies> TaskDependencies1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskHelpRequests> TaskHelpRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskHistory> TaskHistory { get; set; }

        public virtual TaskPriority TaskPriority { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tasks> Tasks1 { get; set; }

        public virtual Tasks Tasks2 { get; set; }

        public virtual TaskStatus TaskStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeEntries> TimeEntries { get; set; }
    }
}
