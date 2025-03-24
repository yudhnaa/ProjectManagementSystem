namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskComments
    {
        [Key]
        public int CommentID { get; set; }

        public int TaskID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(1000)]
        public string CommentText { get; set; }

        public bool? IsEdited { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Tasks Tasks { get; set; }

        public virtual Users Users { get; set; }
    }
}
