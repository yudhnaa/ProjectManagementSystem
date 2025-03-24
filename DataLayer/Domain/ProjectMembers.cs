namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectMembers
    {
        [Key]
        public int ProjectMemberID { get; set; }

        public int ProjectID { get; set; }

        public int UserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        [StringLength(100)]
        public string RoleInProject { get; set; }

        public bool? IsConfirmed { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Projects Projects { get; set; }

        public virtual Users Users { get; set; }
    }
}
