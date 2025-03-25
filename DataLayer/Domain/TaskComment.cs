namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskComment
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(1000)]
        public string CommentText { get; set; }

        public bool? IsEdited { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }
    }
}
