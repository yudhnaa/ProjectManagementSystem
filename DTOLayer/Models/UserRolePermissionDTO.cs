using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class UserRolePermissionDTO
    {
        public int Id {get; set;}
        public int UserRoleId {get; set;}
        public int PermissionId {get; set;}
    }
}
