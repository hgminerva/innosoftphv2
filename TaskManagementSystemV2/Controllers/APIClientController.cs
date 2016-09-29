using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystemV2.Controllers
{
    public class APIClientController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // Get Item
        // =========== 

        [Route("api/client/search/{id}")]
        public List<Models.MstClient> GetItem(String id)
        {
            var isLocked = true;

            var ID = Convert.ToInt32(id);
            var client = from d in db.mstClients
                       where d.Id == ID
                       select new Models.MstClient
                       {
                           Id = d.Id,
                           CompanyName = d.CompanyName,
                           CompanyAddress = d.CompanyAddress,
                           ContactNumber = d.ContactNumber,
                           DateAccepted = d.DateAccepted.ToString(),
                           IsLocked = isLocked
                       };

            return client.ToList();
        }

        // ===========
        // LIST Item
        // =========== 
        [Route("api/client/list")]
        public List<Models.MstClient> Get()
        {
            var isLocked = true;

            var client = from d in db.mstClients
                        select new Models.MstClient
                        {
                            Id = d.Id,
                            CompanyName = d.CompanyName,
                            CompanyAddress = d.CompanyAddress,
                            ContactNumber = d.ContactNumber,
                            DateAccepted = d.DateAccepted.ToString(),
                            IsLocked = isLocked


                        };

            return client.ToList();
        }

        // ===========
        // ADD Item
        // ===========
        [Route("api/client/add")]
        public HttpResponseMessage Post(Models.MstClient item)
        {
            try
            {
                var identityUserId = User.Identity.GetUserId();
                var date = DateTime.Now;

                Data.mstClient newItem = new Data.mstClient();

                newItem.CompanyName = item.CompanyName != null ? item.CompanyName : "00000";
                newItem.CompanyAddress = item.CompanyAddress != null ? item.CompanyAddress : "00000";
                newItem.ContactNumber = item.ContactNumber != null ? item.ContactNumber : "00000";
                newItem.DateAccepted = null;
                newItem.IsLocked = false;

                //ALLOW NULL

                db.mstClients.InsertOnSubmit(newItem);
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
        [HttpPost]
        [Route("api/client/update/{id}")]
        public HttpResponseMessage update(String id, Models.MstClient item)
        {
            try
            {
                //var identityUserId = User.Identity.GetUserId();
                //var mstUserId = (from d in db.MstUsers where "" + d.Id == identityUserId select d.Id).SingleOrDefault();
                var date = DateTime.Now;

                var itemId = Convert.ToInt32(id);
                var items = from d in db.mstClients where d.Id == itemId select d;

                if (items.Any())
                {
                    var updateItem = items.FirstOrDefault();

                    updateItem.Id = itemId;
                    updateItem.CompanyName = item.CompanyName;
                    updateItem.CompanyAddress = item.CompanyAddress;
                    updateItem.ContactNumber = item.ContactNumber;
                    updateItem.DateAccepted = null;
                    updateItem.IsLocked = true;

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
        [Route("api/client/delete/{id}")]
        public HttpResponseMessage Delete(String id)
        {

            try
            {
                var clientId = Convert.ToInt32(id);
                var clients = from d in db.mstClients where d.Id == clientId select d;

                if (clients.Any())
                {
                    db.mstClients.DeleteOnSubmit(clients.First());
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
