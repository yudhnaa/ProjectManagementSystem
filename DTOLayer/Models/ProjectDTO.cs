using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer
{
    public class ProjectDTO
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string ProjectCode {get; set;}
        public string Description {get; set;}
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate {get; set;}
        public decimal? Budget {get; set;}
        public int StatusId {get; set;}
        public int ManagerId {get; set;}
        public int PriorityId {get; set;}
        public decimal? PercentComplete {get; set;}
        public bool? IsDeleted {get; set;}
        public int CreatedBy {get; set;}
        public DateTime? CreatedDate {get; set;}
        public DateTime? UpdatedDate {get; set;}
        public ICollection<TaskDTO> Tasks {get; set;}
    }
}
