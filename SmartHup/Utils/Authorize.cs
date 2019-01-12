using SmartHup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHup
{
    public class Authorized : FilterAttribute, IAuthorizationFilter
    {
        private TicketsEntities db = new TicketsEntities();

        private readonly string[] allowedroles;
        public Authorized(params string[] roles)
        {
            this.allowedroles = roles;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //try
            //{
                var Request = HttpContext.Current.Request;
                //return Request.Cookies["userdata"].Value == null ? false : true;
                //filterContext.HttpContext.Session["url"] = HttpContext.Current.Request.RawUrl;
            //Request.Cookies["userdata"].Value
            bool throwe = false;
         
            try
            {
                var xx = Request.Cookies["userdata"]["count"].ToString();
                throwe = true;
             
            }
            catch (Exception)
            {

             
            }
                if  (throwe)
                    {
                //if (filterContext.HttpContext.Session["admin"] == null)
                user_info.user = db.Users.Find(Convert.ToInt64(Request.Cookies["userdata"]["count"].ToString()));
                if (user_info.user.systemId==null)
                {
                    filterContext.Result = new RedirectResult("/account/login");

                }

            }
                else
                {
                //filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.RawUrl);
                filterContext.HttpContext.Session["lurl"] =
                                      filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult("/account/login");
            }
                //if (global.admin_user.re_password == true)
                //{
                //    filterContext.Result = new RedirectResult("/login/repassword");

                //}
                //if (this.allowedroles.Contains(global.admin_user.user_type.ToString()) == false && this.allowedroles.Count() > 0)
                //{
                //    filterContext.Result = new RedirectResult("/login/index");

                //}
            //}
            //catch (Exception)
            //{

            //    filterContext.Result = new RedirectResult("/account/login");
            //}
        }
    }

}