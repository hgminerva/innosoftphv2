using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementSystemV2.Controllers
{
    public class SoftwareController : Controller
    {
        // GET: Software
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public ActionResult Product()
        {
            return View();
        }

        [Authorize]
        public ActionResult Staff()
        {
            return View();
        }

        [Authorize]
        public ActionResult Client()
        {
            return View();
        }

        [Authorize]
        public ActionResult Users()
        {
            return View();
        }

        [Authorize]
        public ActionResult Task()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddTask()
        {
            return View();
        }

        [Authorize]
        public ActionResult Calls()
        {
            return View();
        }

        [Authorize]
        public ActionResult EditTask()
        {
            return View();
        }
    }
}