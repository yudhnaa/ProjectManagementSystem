namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskDependencies
    {
        [Key]
        public int DependencyID { get; set; }

        public int TaskID { get; set; }

        public int DependsOnTaskID { get; set; }

        public int DependencyType { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Tasks Tasks { get; set; }

        public virtual Tasks Tasks1 { get; set; }
    }
}
