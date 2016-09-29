using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystemV2.Models
{
    public class MstUser
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 StaffId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public Int32? Designation { get; set; }
        public Boolean IsLocked { get; set; }
        public String StaffName { get; set; }
    }
}