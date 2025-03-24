namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskHelpRequests
    {
        [Key]
        public int RequestID { get; set; }

        public int TaskID { get; set; }

        public int RequestedBy { get; set; }

        public int RequestedTo { get; set; }

        [StringLength(500)]
        public string RequestMessage { get; set; }

        public bool? IsResolved { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }

        public virtual Tasks Tasks { get; set; }
    }
}
