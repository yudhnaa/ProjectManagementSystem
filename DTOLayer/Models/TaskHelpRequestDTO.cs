using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class TaskHelpRequestDTO
    {
        public int Id {get; set;}
        public int TaskId {get; set;}
        public int RequestedBy {get; set;}
        public int RequestedTo {get; set;}
        public string RequestMessage {get; set;}
        public bool? IsResolved {get; set;}
        public DateTime? ResolvedDate {get; set;}
        public DateTime? CreatedDate {get; set;}
        public DateTime? UpdatedDate {get; set;}
    }
}
