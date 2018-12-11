using SmartHup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace SmartHup
{
   
        public static class user_info
    {
            public static User user { get; set; }


        public static bool IsAuthenticated()
        {
            var Request = HttpContext.Current.Request;
            return Request.Cookies["userdata"].Value == null ? false : true;
        }
        public static string username()
        {
            var Request = HttpContext.Current.Request;
            return user_info.user.UserName;
        }
        public static string getusername(long id)
        {
            SMARTEntities db = new SMARTEntities();

            if (id!=null)
            {
                try
                {
                    return db.User.Find(id).UserName;

                }
                catch (Exception)
                {
                    return "-";
                }

            }
            return "-";
           
        }
        public static Int64 userid()
        {
            var Request = HttpContext.Current.Request;
            return user_info.user.systemId;
        }

    }


      


        //HttpCookie StudentCookies = new HttpCookie("userdata");
        //StudentCookies.Value =  Crypto.HashPassword("dsdsds"); ;
        //    StudentCookies.Expires = DateTime.Now.AddHours(1);



}