using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class UserDTO
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
        public UserRoleDTO UserRole {get; set;}
        public DateTime? LastLogin {get; set;}
        public DateTime? CreatedDate {get; set;}
        public ICollection<TaskDTO> Tasks {get; set;}
        public ICollection<TaskDTO> Tasks1 {get; set;}
    }
}
