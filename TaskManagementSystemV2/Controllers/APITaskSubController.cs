using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystemV2.Controllers
{
    public class APITaskSubController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // LIST Item
        // =========== 
        [Route("api/tasksub/list/{t}")]
        public List<Models.MstTaskSub> Get(int t)
        {
            
            var tasksub = from d in db.trnTaskSubs
                          where d.TaskId == t
                          select new Models.MstTaskSub
                          {
                              Id = d.Id,
                              TaskId = d.TaskId,
                              DateCalled = d.DateCalled,
                              Action = d.Action,
                              TimeCalled = d.TimeCalled,
                              FinishedDate = d.FinishedDate,
                              FinishedTime = d.FinishedTime,
                              Remarks = d.Remarks
                          };

            return tasksub.ToList();
        }

        // ===========
        // ADD Item
        // ===========
        [Route("api/tasksub/add")]
        public HttpResponseMessage Post(Models.MstTaskSub item)
        {
            try
            {
                var identityUserId = User.Identity.GetUserId();
                var date = DateTime.Now;

                Data.trnTaskSub newItem = new Data.trnTaskSub();

                newItem.TaskId = item.TaskId;
                newItem.Action = item.Action != null ? item.Action : "00000";

                if (item.DateCalled == null && item.TimeCalled == null)
                {
                    newItem.DateCalled = null;
                    newItem.TimeCalled = null;
                }
                else
                {
                    newItem.DateCalled = Convert.ToDateTime(item.DateCalled);
                    newItem.TimeCalled = Convert.ToDateTime(item.TimeCalled);
                }

                if (item.FinishedDate == null && item.FinishedTime == null)
                {
                    newItem.FinishedDate = null;
                    newItem.FinishedTime = null;
                }
                else
                {
                    newItem.FinishedDate = Convert.ToDateTime(item.FinishedDate);
                    newItem.FinishedTime = Convert.ToDateTime(item.FinishedTime);
                }
                newItem.Remarks = item.Remarks != null ? item.Remarks : "00000";

                //ALLOW NULL

                db.trnTaskSubs.InsertOnSubmit(newItem);
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
        [Route("api/tasksub/update/{id}")]
        public HttpResponseMessage Put(String id, Models.MstTaskSub task)
        {
            try
            {
                var identityUserId = User.Identity.GetUserId();

                var date = DateTime.Now;

                var taskSubId = Convert.ToInt32(id);
                var taskSub = from d in db.trnTaskSubs where d.Id == taskSubId select d;

                if (taskSub.Any())
                {
                    var updateItem = taskSub.FirstOrDefault();

                    updateItem.TaskId = task.TaskId;
                    updateItem.Action = task.Action;
                    updateItem.DateCalled = task.DateCalled;
                    updateItem.TimeCalled = task.TimeCalled;
                    updateItem.FinishedDate = task.FinishedDate;
                    updateItem.FinishedTime = task.FinishedTime;
                    updateItem.Remarks = task.Remarks;

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
        [Route("api/tasksub/delete/{id}")]
        public HttpResponseMessage Delete(String id)
        {

            try
            {
                var taskId = Convert.ToInt32(id);
                var tasks = from d in db.trnTaskSubs where d.Id == taskId select d;

                if (tasks.Any())
                {
                    db.trnTaskSubs.DeleteOnSubmit(tasks.First());
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
