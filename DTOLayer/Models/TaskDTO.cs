using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class TaskDTO
    {
        public int Id {get; set;}
        public string Code {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public int ProjectId {get; set;}
        public int AssignedUserId {get; set;}
        public int StatusId {get; set;}
        public int PriorityId {get; set;}
        public DateTime? StartDate {get; set;}
        public DateTime? DueDate {get; set;}
        public decimal? EstimatedHours {get; set;}
        public decimal? ActualHours {get; set;}
        public decimal? PercentComplete {get; set;}
        public int? ParentTaskId {get; set;}
        public DateTime? LastStatusChangeDate {get; set;}
        public string BlockReason {get; set;}
        public bool? IsDeleted {get; set;}
        public int CreatedBy {get; set;}
        public DateTime? CreatedDate {get; set;}
        public DateTime? UpdatedDate {get; set;}
    }
}
