namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Roles()
        {
            RolePermissions = new HashSet<RolePermissions>();
            Users = new HashSet<Users>();
        }

        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolePermissions> RolePermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
