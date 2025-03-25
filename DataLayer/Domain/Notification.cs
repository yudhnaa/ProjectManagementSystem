namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Message { get; set; }

        public int NotificationTypeId { get; set; }

        public int? RelatedId { get; set; }

        public bool? IsRead { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual NotificationType NotificationType { get; set; }

        public virtual User User { get; set; }
    }
}
