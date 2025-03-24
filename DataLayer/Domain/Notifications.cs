namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notifications
    {
        [Key]
        public int NotificationID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Message { get; set; }

        public int NotificationType { get; set; }

        public int? RelatedID { get; set; }

        public bool? IsRead { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual NotificaitonTypes NotificaitonTypes { get; set; }

        public virtual Users Users { get; set; }
    }
}
