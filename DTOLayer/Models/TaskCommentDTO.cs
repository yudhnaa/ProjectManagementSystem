using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class TaskCommentDTO
    {
        public int Id {get; set;}
        public int TaskId {get; set;}
        public int UserId {get; set;}
        public string CommentText {get; set;}
        public DateTime? CreatedDate {get; set;}
    }
}
