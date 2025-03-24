namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Files
    {
        [Key]
        public int FileID { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; }

        public long? FileSize { get; set; }

        [StringLength(100)]
        public string FileType { get; set; }

        public int? ProjectID { get; set; }

        public int? TaskID { get; set; }

        public int UploadedBy { get; set; }

        public DateTime? UploadDate { get; set; }

        public virtual Projects Projects { get; set; }

        public virtual Tasks Tasks { get; set; }

        public virtual Users Users { get; set; }
    }
}
