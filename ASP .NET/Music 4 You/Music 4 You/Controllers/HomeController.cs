using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
      [AllowAnonymous]
      public ActionResult Index()
        {
            return View(); 
        }

        public ActionResult AnyUser()
        {
            ViewBag.Result = "Success - any user";
            return View("result");
        }


        [Authorize(Roles="Admin")]
        public ActionResult AdminsOnly()
        {
            ViewBag.Result = "Success- users in Admin Role";
            return View("result");

        }

        [Authorize(Roles ="Exec")]
        public ActionResult ExecsOnly()
        {
            ViewBag.Result = "Success - user in Exec Role";

            return View("result");
        }

        [Authorize(Roles = "Clerk")]
        public ActionResult ClerksOnly()
        {
            ViewBag.Result = "Success - user in  Clerk Role";

            return View("result");
        }

        [Authorize(Roles = "Staff")]
        public ActionResult StaffOnly()
        {
            ViewBag.Result = "Success - user in Staff Role";

            return View("result");
        }





    }
}