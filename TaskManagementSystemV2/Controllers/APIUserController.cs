using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystemV2.Controllers
{
    public class APIUserController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // Get Item
        // =========== 

        [Route("api/user/search/{id}")]
        public List<Models.MstUser> GetItem(String id)
        {
            var isLocked = true;

            var ID = Convert.ToInt32(id);
            var user = from d in db.mstUsers
                       where d.StaffId == ID
                       select new Models.MstUser
                       {
                           Id = d.StaffId,
                           StaffId = d.StaffId,
                           Username = d.Username,
                           Password = d.Password,
                           Designation = d.Designation,
                           IsLocked = isLocked
                       };

            return user.ToList();
        }

        // ===========
        // LIST Item
        // =========== 
        [Route("api/user/list")]
        public List<Models.MstUser> Get()
        {
            var isLocked = true;

            var user = from d in db.mstUsers
                          select new Models.MstUser
                          {
                              Id = d.Id,
                              StaffId = d.StaffId,
                              StaffName = d.mstStaff.StaffName,
                              Password = d.Password,
                              Designation = d.Designation,
                              Username = d.Username,
                              IsLocked = isLocked
                          };

            return user.ToList();
        }

        // ===========
        // ADD Item
        // ===========
        [Route("api/user/add")]
        public HttpResponseMessage Post(Models.MstUser item)
        {
            try
            {
                var identityUserId = User.Identity.GetUserId();
                var date = DateTime.Now;

                Data.mstUser newItem = new Data.mstUser();

                newItem.Username = item.Username != null ? item.Username : "00000";
                newItem.Password = item.Password != null ? item.Password : "00000";
                newItem.Designation = item.Designation;
                newItem.StaffId = item.StaffId;
                newItem.IsLocked = true;

                //ALLOW NULL

                db.mstUsers.InsertOnSubmit(newItem);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // ==============
        // UPDATE Item
        // ==============
        [Route("api/user/update/{id}")]
        public HttpResponseMessage Put(String id, Models.MstUser item)
        {
            try
            {
                var isLocked = true;
                var identityUserId = User.Identity.GetUserId();
                //var mstUserId = (from d in db.MstUsers where "" + d.Id == identityUserId select d.Id).SingleOrDefault();
                var date = DateTime.Now;

                var itemId = Convert.ToInt32(id);
                var items = from d in db.mstUsers where d.Id == itemId select d;

                if (items.Any())
                {
                    var updateItem = items.FirstOrDefault();

                    updateItem.Username = item.Username;
                    updateItem.Password = item.Password;
                    updateItem.StaffId = item.StaffId;
                    updateItem.Designation = item.Designation;
                    updateItem.IsLocked = isLocked;

                    //updateItem.UpdateUserId = 123;
                    //updateItem.UpdateDateTime = date;
                    //updateItem.IsLocked = isLocked;

                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // ==============
        // DELETE Item
        // ==============
        [Route("api/user/delete/{id}")]
        public HttpResponseMessage Delete(String id)
        {

            try
            {
                var userId = Convert.ToInt32(id);
                var users = from d in db.mstUsers where d.Id == userId select d;

                if (users.Any())
                {
                    db.mstUsers.DeleteOnSubmit(users.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


    }
}
