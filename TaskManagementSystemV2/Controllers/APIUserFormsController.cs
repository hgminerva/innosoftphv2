using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystemV2.Controllers
{
    public class APIUserFormsController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // Get Item
        // =========== 
        [HttpGet]
        [Route("api/userforms/search/{id}")]
        public List<Models.MstUserForms> getItem(String id)
        {
            var ID = Convert.ToInt32(id);
            var item = from d in db.mstUserForms
                       where d.Id == ID
                       select new Models.MstUserForms
                       {
                           Id = d.Id,
                           StaffId = d.StaffId,
                           Username = d.Username,
                           FormId = d.FormId,
                           FormDescription = d.sysForm.FormDescription,
                           CanAdd = d.CanAdd,
                           CanSave = d.CanSave,
                           CanEdit = d.CanEdit,
                           CanDelete = d.CanDelete,
                           CanView = d.CanView,
                           CanPreview = d.CanPreview,
                           CanPrint = d.CanPrint
                       };

            return item.ToList();
        }

        // ===========
        // Get UserForms
        // =========== 
        [HttpGet]
        [Route("api/userforms/details/{id}")]
        public List<Models.MstUserForms> getUserForms(String id)
        {
            var ID = Convert.ToInt32(id);
            var item = from d in db.mstUserForms
                       where d.StaffId == ID
                       select new Models.MstUserForms
                       {
                           Id = d.Id,
                           StaffId = d.StaffId,
                           Username = d.Username,
                           FormId = d.FormId,
                           FormDescription = d.sysForm.FormDescription,
                           CanAdd = d.CanAdd,
                           CanSave = d.CanSave,
                           CanEdit = d.CanEdit,
                           CanDelete = d.CanDelete,
                           CanView = d.CanView,
                           CanPreview = d.CanPreview,
                           CanPrint = d.CanPrint
                       };

            return item.ToList();
        }

        // ===========
        // LIST Item
        // ===========
        [HttpGet]
        [Route("api/userforms/list")]
        public List<Models.MstUserForms> list()
        {
            
            var userforms = from d in db.mstUserForms
                        select new Models.MstUserForms
                        {
                            Id = d.Id,
                            StaffId = d.StaffId,
                            Username = d.Username,
                            FormId = d.FormId,
                            FormDescription = d.sysForm.FormDescription,
                            CanAdd = d.CanAdd,
                            CanSave = d.CanSave,
                            CanEdit = d.CanEdit,
                            CanDelete = d.CanDelete,
                            CanView = d.CanView,
                            CanPrint = d.CanPrint
                            
                        };

            return userforms.ToList();
        }

        // ===========
        // ADD Item
        // ===========
        [HttpPost]
        [Route("api/userforms/add")]
        public HttpResponseMessage add(Models.MstUserForms item)
        {
            try
            {
                var identityUserId = User.Identity.GetUserId();
                var date = DateTime.Now;

                Data.mstUserForm newItem = new Data.mstUserForm();

                newItem.StaffId = item.StaffId;
                newItem.Username = item.Username != null ? item.Username : "00000";
                newItem.FormId =item.StaffId;
                newItem.CanAdd = item.CanAdd;
                newItem.CanSave = item.CanSave;
                newItem.CanEdit = item.CanEdit;
                newItem.CanDelete = item.CanDelete;
                newItem.CanView = item.CanView;
                newItem.CanPrint = item.CanPrint;

                db.mstUserForms.InsertOnSubmit(newItem);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
