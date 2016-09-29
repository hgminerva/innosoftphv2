using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystemV2.Models
{
    public class MstTask
    {
        [Key]
        public Int32 Id { get; set; }
        public String TaskNo { get; set; }
        public String TaskDate { get; set; }
        public Int32 ClientId { get; set; }
        public String CompanyName { get; set; }
        public String Caller { get; set; }
        public String Concern { get; set; }
        public Int32 AnsweredBy  { get; set; }
        public Int32 StaffId { get; set; }
        public Int32 ProductId { get; set; }
        public String ProductCode { get; set; }
        public String Remarks { get; set; }
        public String Status { get; set; }
        public String ProblemType { get; set; }
        public String Severity { get; set; }
        public String Solution { get; set; }
        public String DoneDate { get; set; }
        public String DoneTime { get; set; }
        public Int32? VerifiedBy { get; set; }
        public Boolean IsLocked { get; set; }
}
}