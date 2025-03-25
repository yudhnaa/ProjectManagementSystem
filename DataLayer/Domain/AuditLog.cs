namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AuditLog
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }

        [Required]
        [StringLength(50)]
        public string Module { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string IpAddress { get; set; }

        public DateTime? LogDate { get; set; }

        public virtual User User { get; set; }
    }
}
