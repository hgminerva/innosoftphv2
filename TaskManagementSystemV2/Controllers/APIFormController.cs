using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystemV2.Models
{
    public class APIFormController : ApiController
    {
        private Data.TMSdbmlDataContext db = new Data.TMSdbmlDataContext();

        // ===========
        // LIST Item
        // ===========
        [HttpGet]
        [Route("api/form/list")]
        public List<Models.MstForm> list()
        {

            var forms = from d in db.sysForms
                        select new Models.MstForm
                        {
                            Id = d.Id,
                            Form = d.Form,
                            FormDescription = d.FormDescription
                        };

            return forms.ToList();
        }
    }
}