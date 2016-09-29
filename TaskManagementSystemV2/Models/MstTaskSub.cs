using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystemV2.Models
{
    public class MstTaskSub
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 TaskId { get; set; }
        public DateTime? DateCalled { get; set; }
        public String Action { get; set; }
        public DateTime? TimeCalled { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? FinishedTime { get; set; }
        public String Remarks { get; set; }
    }
}