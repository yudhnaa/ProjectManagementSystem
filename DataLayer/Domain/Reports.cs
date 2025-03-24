namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reports
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        [StringLength(100)]
        public string ReportName { get; set; }

        public int ReportType { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public string ReportData { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Users Users { get; set; }

        public virtual ReportTypes ReportTypes { get; set; }
    }
}
