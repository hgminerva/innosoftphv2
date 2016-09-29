using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystemV2.Models
{
    public class MstClient
    {
        [Key]
        public Int32 Id { get; set; }
        public String CompanyName { get; set; }
        public String CompanyAddress { get; set; }
        public String ContactNumber { get; set; }
        public String DateAccepted { get; set; }
        public Boolean IsLocked { get; set; }
    }
}