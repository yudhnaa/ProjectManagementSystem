namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmailNotifications
    {
        [Key]
        public int EmailID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime? SentDate { get; set; }

        public bool? IsSuccessful { get; set; }

        [StringLength(500)]
        public string ErrorMessage { get; set; }

        public virtual Users Users { get; set; }
    }
}
