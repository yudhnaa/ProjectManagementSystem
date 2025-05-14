using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class UserExtraInfoDTO
    {
        public int Id {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string Email {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string PhoneNumber {get; set;}
        public string Address {get; set;}
        public string Avatar {get; set;}
        public int UserRoleId {get; set;}
        public int? PositionId {get; set;}
        public DateTime? HireDate {get; set;}
        public int DepartmentId {get; set;}
        public int? ReportTo {get; set;}
        public decimal? Salary {get; set;}
        public DateTime? LastLogin {get; set;}
        public bool? IsActive {get; set;}
    }
}
