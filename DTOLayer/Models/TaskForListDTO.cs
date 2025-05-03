using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class TaskForListDTO
    {
        public int Id {get; set;}
        public string Code {get; set;}
        public string Name {get; set;}
        public int StatusId {get; set;}
        public DateTime? StartDate {get; set;}
        public int CreatedBy {get; set;}
    }
}
