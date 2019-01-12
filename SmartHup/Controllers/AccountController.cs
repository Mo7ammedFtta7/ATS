using SmartHup.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SmartHup.Controllers
{
    public class AccountController : Controller
    {
        private TicketsEntities db = new TicketsEntities();

        // GET: Login
        public ActionResult login()
        {
            //string hpass = Crypto.HashPassword("admin2");
            return View();
        }
        //public ActionResult repassword()s
        //{
        //    return View();
        //}
        public ActionResult LogOff()
        {
          
            Session["admin"] = null;
            user_info.user = null;

            try
            {
                HttpCookie userInfo = new HttpCookie("userdata");
                userInfo.Expires=DateTime.Now.AddDays(-1);
                Response.Cookies.Add(userInfo);
            }
            catch (Exception)
            {

            }


            return RedirectToAction("login", "account");
            
        }
        //[HttpPost]
        //public ActionResult repassword(string old_pass, string cur_pass, string new_pass)
        //{
        //    if (user_info.user.PasswordHash == old_pass && cur_pass == new_pass)
        //    {
        //        global.admin_user.password = new_pass;
        //        global.admin_user.re_password = false;

        //        db.Entry(global.admin_user).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("index", "tickets");

        //    }

        //    return View();
        //}
        [AllowAnonymous]
        [HttpPost]
        public ActionResult login(LoginVM login)
        {

            if (ModelState.IsValid)
            {
                //string hpass = Crypto.HashPassword(login.password);
                var result = db.Users.Where(c => c.UserName == login.username ).FirstOrDefault();
                if (result==null)
                {
                    ViewBag.message = "Login failed";
                    return View();
                }
                string ss = Crypto.HashPassword(login.password);
                if (Crypto.VerifyHashedPassword( result.PasswordHash.ToString(), login.password))
                {
                    if (result.LockoutEnabled == true || result.status == 2)
                    {
                        ViewBag.message = "Account Locked";
                        return View();
                    }
                    HttpCookie userInfo = new HttpCookie("userdata");
                    userInfo["usertoken"] = Crypto.HashPassword(login.username);
                    userInfo["Count"] = result.systemId.ToString();
                    userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                    Response.Cookies.Add(userInfo);

                    //Session["admin"] = result.First();
                    user_info.user = result;
                    //WizardProductVM wiz = (WizardProductVM)Session["object"];
                    if (Session["url"] != null)
                    {
                        return Redirect(Session["url"].ToString());
                    }
                    //Session["adminname"] = result.First().user_name;
                    //Session["usertype"] = result.First().user_type;
                    //Session["client"] = result.First().Clients.client;
                    //Session["usertype"] = result.First().user_type;

                 

                    return RedirectToAction("index", "home");
                }
                ViewBag.message = "Login failed";
                return View();

            }
            ViewBag.message = "you must fill all data";

            return View();
        }
    }
}