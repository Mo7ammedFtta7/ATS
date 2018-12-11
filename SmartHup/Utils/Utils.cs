using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SmartHup
{
    public class Utils
    {
        public static string LINK_AccessDenied = "/Error/AccessDenied";
        public static string LINK_Login = "/Account/Login";
        public static string LINK_Home = "/Home/Index";

        public static long CSP = 2;

        public static long PAGEID_HOME = 4;
        public static long PAGEID_ERROR = 6;
        public static long PAGEID_ACCESSDENIED = 8;

        public static string PAGE_ViewPage = "NormalPage";
        public static string PAGE_ActionPage = "ActionPage";
        public static long[] IgnoredPages = new long[] { PAGEID_HOME, PAGEID_ERROR, PAGEID_ACCESSDENIED };

        public static long HTTP_BADREQUEST = 500;
        public static long HTTP_NOTFOUND = 404;

        public static int STATUS_ACTIVE = 1;
        public static int STATUS_DEACTIVE = 2;
        public static int STATUS_DELETED = 3;
        public static int STATUS_SUSPENDED = 4;
        public static int STATUS_PENDING = 5;
        public static int STATUS_PURGED = 6;
        public static int STATUS_CANCELLED = 7;
        public static int STATUS_COMPLETED = 8;


        public static Dictionary<string, string> showData(string txt)
        {
            Dictionary<string, string> data =    JsonConvert.DeserializeObject<Dictionary<string, string>>(txt);
            //var result = renderJson(data);
            return data;
        }

        public static string color(string status)
        {
            var color = "label label-";
            switch (status)
            {
                case "Active": color+="success"; break;
                case "Completed": color+= "success"; break;

                case "Not Activated": color+= "danger"; break;
                case "Deactive": color+= "danger"; break;
                case "Deleted": color+= "danger"; break;
                case "Cancelled": color+= "danger"; break;
                case "Expired": color+= "danger"; break;
                case "Suspended": color+= "danger"; break;
                case "Pending": color+="primary"; break;
                case "Purged": color+= "primary"; break;
                case "New": color+= "primary"; break;
                default: color+= "success"; break;
            }




            return color;

        }

    }

}