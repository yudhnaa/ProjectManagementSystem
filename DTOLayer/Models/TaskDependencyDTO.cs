using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class TaskDependencyDTO
    {
        public int Id {get; set;}
        public int TaskId {get; set;}
        public int DependsOnTaskId {get; set;}
        public int DependencyType {get; set;}
    }
}
