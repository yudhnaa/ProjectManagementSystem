using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class DepartmentDTO
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public int? ManagerId {get; set;}
        public bool? IsActive {get; set;}
    }
}
