using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class ProjectMemberDTO
    {
        public int Id {get; set;}
        public int ProjectId {get; set;}
        public int UserId {get; set;}
        public DateTime? JoinDate {get; set;}
        public int RoleInProject {get; set;}
        public bool? IsConfirmed {get; set;}
        public DateTime? ConfirmationDate {get; set;}
        public bool? IsActive {get; set;}
        public bool? IsDeleted {get; set;}
        public DateTime? CreatedDate {get; set;}
        public DateTime? UpdatedDate {get; set;}
    }
}
