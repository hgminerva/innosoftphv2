using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystemV2.Models
{
    public class MstUserForms
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 StaffId { get; set; }
        public String Username { get; set; }
        public Int32 FormId { get; set; }
        public String FormDescription { get; set; }
        public Boolean CanAdd { get; set; }
        public Boolean CanSave { get; set; }
        public Boolean CanEdit { get; set; }
        public Boolean CanDelete { get; set; }
        public Boolean CanView { get; set; }
        public Boolean CanPreview { get; set; }
        public Boolean CanPrint { get; set; }
    }
}