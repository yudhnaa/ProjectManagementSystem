using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class TaskHistoryDTO
    {
        public int Id {get; set;}
        public int TaskId {get; set;}
        public string FieldChanged {get; set;}
        public string OldValue {get; set;}
        public string NewValue {get; set;}
        public int ChangedBy {get; set;}
    }
}
