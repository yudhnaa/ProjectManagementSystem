namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskDependency
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int DependsOnTaskId { get; set; }

        public int DependencyType { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Task Task { get; set; }

        public virtual TaskDependencyType TaskDependencyType { get; set; }

        public virtual Task Task1 { get; set; }
    }
}
