namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskHistory")]
    public partial class TaskHistory
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        [Required]
        [StringLength(50)]
        public string FieldChanged { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public int ChangedBy { get; set; }

        public DateTime? ChangedDate { get; set; }

        public virtual User User { get; set; }

        public virtual Task Task { get; set; }
    }
}
