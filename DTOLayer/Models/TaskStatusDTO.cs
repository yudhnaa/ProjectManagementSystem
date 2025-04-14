using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class TaskStatusDTO
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
    }
}
