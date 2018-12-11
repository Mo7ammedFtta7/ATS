
using SmartHup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHup.Controllers
{
    [Authorized]

    public class HomeController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        public ActionResult Index()
        {
            ViewBag.customer = db.customer.Count();
            ViewBag.User = db.User.Count();
            ViewBag.Service = db.Service.Count();
            ViewBag.Terminal = db.Terminal.Count();


            return View();
        }
 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}