using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystemV2.Controllers
{
    public class APITaskController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // LIST Item
        // =========== 
        [HttpGet]
        [Route("api/task/listByDate/{date}")] //
        public List<Models.MstTask> get(String date) //
        {
            var task = from d in db.trnTasks
                       where d.TaskDate == Convert.ToDateTime(date)
                       select new Models.MstTask
                       {
                           Id = d.Id,
                           TaskNo = d.TaskNo,
                           TaskDate = d.TaskDate.ToShortDateString(),
                           ClientId = d.ClientId,
                           Caller = d.Caller,
                           Concern = d.Concern,
                           AnsweredBy = d.AnsweredBy,
                           StaffId = d.StaffId,
                           ProductId = d.ProductId,
                           ProductCode = d.mstProduct.ProductCode,
                           Remarks = d.Remarks,
                           Status = d.Status,
                           ProblemType = d.ProblemType,
                           Severity = d.Severity,
                           Solution = d.Solution,
                           DoneDate = d.DoneDate.ToString(),
                           DoneTime = d.DoneTime.ToString(),
                           VerifiedBy = d.VerifiedBy,
                           IsLocked = d.IsLocked
                       };

            return task.ToList();

        }

        // ===========
        // LIST Item order by last
        // =========== 
        [HttpGet]
        [Route("api/task/listByOrder/{date}")] //
        public Models.MstTask getDate(String date) //
        {
            var task = from d in db.trnTasks.OrderByDescending(d => d.Id)
                       where d.TaskDate == Convert.ToDateTime(date)
                       select new Models.MstTask
                       {
                           TaskNo = d.TaskNo,
                       };

            return (Models.MstTask)task.FirstOrDefault();

        }

        // ===========
        // LIST Item search by ID
        // =========== 
        [HttpGet]
        [Route("api/task/list/{id}")] //
        public List<Models.MstTask> getId(String id) //
        {
            var ID = Convert.ToInt32(id);
            var task = from d in db.trnTasks
                       where d.Id == ID
                       select new Models.MstTask
                       {
                           Id = d.Id,
                           TaskNo = d.TaskNo,
                           TaskDate = d.TaskDate.ToShortDateString(),
                           ClientId = d.ClientId,
                           Caller = d.Caller,
                           Concern = d.Concern,
                           AnsweredBy = d.AnsweredBy,
                           StaffId = d.StaffId,
                           ProductId = d.ProductId,
                           ProductCode = d.mstProduct.ProductCode,
                           Remarks = d.Remarks,
                           Status = d.Status,
                           ProblemType = d.ProblemType,
                           Severity = d.Severity,
                           Solution = d.Solution,
                           DoneDate = d.DoneDate.ToString(),
                           DoneTime = d.DoneTime.ToString(),
                           VerifiedBy = d.VerifiedBy,
                           IsLocked = d.IsLocked
                       };

            return task.ToList();

        }

        // ===========
        // ADD Item
        // ===========
        [Route("api/task/add")]
        public HttpResponseMessage Post(Models.MstTask item)
        {
            try
            {
                var isLocked = true;
                var identityUserId = User.Identity.GetUserId();
                var date = DateTime.Now;

                Data.trnTask newItem = new Data.trnTask();

                newItem.TaskNo = item.TaskNo != null ? item.TaskNo : "00000";
                newItem.TaskDate = Convert.ToDateTime(item.TaskDate);
                newItem.ClientId = item.ClientId;
                newItem.Caller = item.Caller != null ? item.Caller : "00000";
                newItem.Concern = item.Caller != null ? item.Concern : "00000";
                newItem.AnsweredBy = item.AnsweredBy;
                newItem.StaffId = item.StaffId;
                newItem.ProductId = item.ProductId;
                newItem.Remarks = item.Remarks != null ? item.Remarks: "00000"; 
                newItem.Status = item.Status != null ? item.Status : "00000";
                newItem.ProblemType = item.ProblemType != null ? item.ProblemType : "00000";
                newItem.Severity = item.Severity != null ? item.Severity : "00000";
                newItem.Solution = item.Solution != null ? item.Solution : "00000";
                if (item.DoneDate == null && item.DoneTime == null)
                {
                    newItem.DoneDate = null;
                    newItem.DoneTime = null;
                }
                else
                {
                    newItem.DoneDate = Convert.ToDateTime(item.DoneDate);
                    newItem.DoneTime = Convert.ToDateTime(item.DoneTime);
                }
                newItem.VerifiedBy = item.VerifiedBy;
                newItem.IsLocked = isLocked;

                //ALLOW NULL

                db.trnTasks.InsertOnSubmit(newItem);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK, newItem.Id);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // ==============
        // UPDATE Item
        // ==============
        [Route("api/task/update/{id}")]
        public HttpResponseMessage Put(String id, Models.MstTask task)
        {
            try
            {
                var isLocked = true;
                var identityUserId = User.Identity.GetUserId();
               
                var date = DateTime.Now;

                var taskId = Convert.ToInt32(id);
                var tasks = from d in db.trnTasks where d.Id == taskId select d;

                if (tasks.Any())
                {
                    var updateItem = tasks.FirstOrDefault();

                    updateItem.ClientId = task.ClientId;
                    updateItem.Caller = task.Caller;
                    updateItem.Concern = task.Concern;
                    updateItem.AnsweredBy = task.AnsweredBy;
                    updateItem.StaffId = task.StaffId;
                    updateItem.ProductId = task.ProductId;
                    updateItem.Remarks = task.Remarks;
                    updateItem.Status = task.Status;
                    updateItem.ProblemType = task.ProblemType;
                    updateItem.Severity = task.Severity;
                    updateItem.Solution = task.Solution;
                    updateItem.DoneDate = Convert.ToDateTime(task.DoneDate);
                    updateItem.DoneTime = Convert.ToDateTime(task.DoneTime);
                    updateItem.VerifiedBy = task.VerifiedBy;
                    updateItem.IsLocked = isLocked;

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
        [Route("api/task/delete/{id}")]
        public HttpResponseMessage Delete(String id)
        {

            try
            {
                var taskId = Convert.ToInt32(id);
                var tasks = from d in db.trnTasks where d.Id == taskId select d;
                var tasksub = from t in db.trnTaskSubs where t.TaskId == taskId select t;
                
              
               if (tasks.Any())
                {
                    db.trnTasks.DeleteOnSubmit(tasks.First());
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
