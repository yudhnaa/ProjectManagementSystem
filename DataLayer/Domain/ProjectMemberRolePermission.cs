namespace DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectMemberRolePermission
    {
        public int Id { get; set; }

        public int ProjectMemberRoleId { get; set; }

        public int PermissionId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual ProjectMemberRole ProjectMemberRole { get; set; }
    }
}
