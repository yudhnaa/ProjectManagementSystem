namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskHelpRequest
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int RequestedBy { get; set; }

        public int RequestedTo { get; set; }

        [StringLength(500)]
        public string RequestMessage { get; set; }

        public bool? IsResolved { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        public virtual Task Task { get; set; }
    }
}
