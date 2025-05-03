using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class ProjectForListDTO
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string ProjectCode {get; set;}
        public DateTime? StartDate {get; set;}
        public int StatusId {get; set;}
        public int PriorityId {get; set;}
        public decimal? PercentComplete {get; set;}
        public bool? IsDeleted {get; set;}
    }
}
