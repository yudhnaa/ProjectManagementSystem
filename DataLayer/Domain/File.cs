namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class File
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; }

        public long? FileSize { get; set; }

        [StringLength(100)]
        public string FileType { get; set; }

        public int? ProjectId { get; set; }

        public int? TaskId { get; set; }

        public int UploadedBy { get; set; }

        public DateTime? UploadDate { get; set; }

        public virtual Project Project { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }
    }
}
