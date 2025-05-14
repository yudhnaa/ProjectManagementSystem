using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DTOLayer.Models
{
    public class NotificationDTO
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public string Title {get; set;}
        public string Message {get; set;}
        public int NotificationTypeId {get; set;}
        public int? RelatedId {get; set;}
        public bool? IsRead {get; set;}
        public DateTime? CreatedDate {get; set;}
    }
}
