using Newtonsoft.Json;
using SmartHup.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartHup
{
    public class Base : Controller
    {


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        
            var userId = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;

            if (startCheckPermission(userId, filterContext))
                filterContext.Result = new RedirectResult(Utils.LINK_AccessDenied);


            base.OnActionExecuting(filterContext);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            //CultureInfo.CurrentUICulture = culture;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Console.WriteLine(filterContext.Exception.Message);
            filterContext.ExceptionHandled = false;

            exeptionHandler(filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString(), filterContext.Exception);

            //filterContext.Result =new RedirectResult(Url.Action("Error", "Error", new { httpCode = 0 }));
        }

        private void exeptionHandler(string controller, string action, Exception exc)
        {
            SMARTEntities db = new SMARTEntities(); 
            AuditingLog exception = new AuditingLog();
            var module = db.Module.FirstOrDefault(m => m.name.Equals(controller));
            var _action = db.Action.FirstOrDefault(a => a.name.Equals(action) && a.moduleId == module.systemId);
            long userId = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
            string type = exc.GetType().ToString() ?? "";
            string msg = exc.Message ?? "";
            string inner = exc.InnerException == null ? "" : exc.InnerException.ToString();
            var data = toStringJson(new
            {
                Type = type,
                message = msg,
                InnerException = inner
            });
            exception = new AuditingLog()
            {
                actionId = _action.systemId,
                moduleId = module.systemId,
                clientData = getClientData(),
                userId = userId,
                oldData = data,
                dateCreated = DateTime.Now

            };
            try
            {
                db.AuditingLog.Add(exception);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void log(object oldData)
        {
            string dataClient = "", temp_oldData = "";
            long moduleId, actionId;

            temp_oldData = toStringJson(oldData);
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            temp_oldData = rgx.Replace(temp_oldData, "");


            dataClient = getClientData();

            var link = Request.Url.AbsolutePath.ToString().Split('/');

           SMARTEntities db = new SMARTEntities(); /*var db = new UsersModel();*/
            string controller = link[1];//this.ControllerContext.RouteData.Values["controller"].ToString();
            string action = "";
            if (link.Length != 2)
                action = link[2]; //this.ControllerContext.RouteData.Values["action"].ToString();


            Models.Module moduleData = db.Module.FirstOrDefault(m => m.name.Equals(controller));
            if (moduleData == null)
            {
                addModule(controller);
                moduleId = db.Module.FirstOrDefault(m => m.name.Equals(controller)).systemId;
            }
            else
            {
                moduleId = moduleData.systemId;
            }

            Models.Action actionData = db.Action.FirstOrDefault(a => a.moduleId == moduleId && a.name.Equals(action));
            if (actionData == null)
            {
                addAction(action, moduleId);
                actionId = db.Action.FirstOrDefault(a => a.moduleId == moduleId && a.name.Equals(action)).systemId;
            }
            else
            {
                actionId = actionData.systemId;
            }

            var user = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;

            var log = new AuditingLog
            {
                userId = user,
                actionId = actionId,
                moduleId = moduleId,
                clientData = dataClient,
                oldData = temp_oldData,
                dateCreated = DateTime.Now
            };
            db.AuditingLog.Add(log);
            db.SaveChanges();
        }

        private void addAction(string action, long moduleId)
        {
            Models.Action model = new Models.Action
            {
                createdBy = user_info.user.systemId /*User.Identity.GetUserId<long>()*/,
                creationDate = DateTime.Now,
                name = action,
                status = 1,
                pageType = Utils.PAGE_ViewPage,
                moduleId = moduleId,
            };
           SMARTEntities db = new SMARTEntities(); /*var db = new UsersModel();*/
            db.Action.Add(model);
            db.SaveChanges();
        }

        private void addModule(string controller)
        {
            Models.Module model = new Models.Module
            {
                createdBy = user_info.user.systemId /*user_info.user.systemId /*User.Identity.GetUserId<long>()*/,
                creationDate = DateTime.Now,
                name = controller,
                status = 1,
            };
           SMARTEntities db = new SMARTEntities(); /*var db = new UsersModel();*/
            db.Module.Add(model);
            db.SaveChanges();
        }

        private string getClientData()
        {
            //string userIpAddress = this.Request.ServerVariables["REMOTE_ADDR"];
            string ipAddress = Request.UserHostAddress;
            string url = Request.Url.AbsolutePath.ToString();
            string UserAgent = Request.UserAgent;
            string UserHostName = Request.UserHostName;

            var client = new
            {
                host = UserHostName,
                ip = ipAddress,
                agent = UserAgent,
                url = url
            };

            return toStringJson(client);
        }

        private string toStringJson(object data)
        {
            try
            {
                string temp = JsonConvert.SerializeObject(data);

                string result = temp.Replace(@"\", " ");
                return result;
            }
            catch (Exception)
            {
                data = "SerializeObject error";
                string temp = JsonConvert.SerializeObject(data);

                string result = temp.Replace(@"\", " ");
                return result;
            }

           
            //return new JavaScriptSerializer().Serialize(data).Replace("/","");
        }

        private bool startCheckPermission(long userId, ActionExecutingContext filterContext)
        {
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            ViewBag.controller = controller;
            string action = filterContext.ActionDescriptor.ActionName;
            List<long> pagesID = new List<long>();

            //written by A.Murtada
            // List of Authorized Pages
           SMARTEntities db = new SMARTEntities(); /*var db = new UsersModel();*/
            var user = db.User.FirstOrDefault(o => o.systemId.Equals(userId));
            if (user != null)
            {
                renderLinkAuthorization(user.RoleId);
                pagesID = db.RoleActions.Where(o => o.RoleId == user.RoleId).Select(A => A.actionId).ToList();
            }
            else
            {
                renderLinkAuthorization(null);
                //pagesID = db.Action.Select(A => A.systemId).ToList();
            }

            // get the page
            Models.Action page = db.Action.Where(o => o.Module.name.Equals(controller)
                                                      && o.name.Equals(action)).FirstOrDefault();

            //if the page doesn't Exist in Pages
            if (page == null)
            {
                var module = db.Module.Where(o => o.name.Equals(controller)).FirstOrDefault();
                if (module == null) addModule(controller);
                module = db.Module.Where(o => o.name.Equals(controller)).FirstOrDefault();
                addAction(action, module.systemId);
                page = db.Action.Where(o => o.Module.name.Equals(controller)
                                                     && o.name.Equals(action)).FirstOrDefault();
            }

            //Authorization Check 
            if (Utils.IgnoredPages.Contains(page.systemId) || pagesID.Contains(page.systemId))
                return false;
            else
                return true;
        }

        private void renderLinkAuthorization(long? roleId)
        {
           SMARTEntities db = new SMARTEntities(); /*var db = new UsersModel();*/
            List<RoleActions> data = new List<RoleActions>();
            Dictionary<Tuple<string, string>, bool> menu = new Dictionary<Tuple<string, string>, bool>();
            Dictionary<string, List<Tuple<string, string, string>>> links = new Dictionary<string, List<Tuple<string, string, string>>>();

            if (roleId != null)
                data = db.RoleActions.Include("Action").Where(ra => ra.RoleId == roleId
                //&& ra.Action.Module.parentId != null 
                //&& ra.Action.pageType.Equals(Utils.PAGE_ViewPage)
                ).ToList();

            foreach (var a in data)
            {
                var key = Tuple.Create(a.Action.Module.name, a.Action.name);
                if (!menu.ContainsKey(key))
                    menu.Add(
                        key: key,
                        value: true
                    );
            }
            var linkData = data.Where(ra => ra.Action.Module.parentId != null && ra.Action.pageType.Equals(Utils.PAGE_ViewPage)).ToList();
            foreach (var a in linkData)
            {
                var linkKey = Tuple.Create(a.Action.Module.name, a.Action.name, a.Action.label);
                string moduleName = db.Module.FirstOrDefault(m => m.systemId == a.Action.Module.parentId).name;
                if (links.ContainsKey(moduleName))
                    links[moduleName].Add(linkKey);
                else
                    links.Add(moduleName, new List<Tuple<string, string, string>> { linkKey });
            }
            ViewBag.menu = menu;
            links.OrderBy(o => o.Value);
            ViewBag.authLinks = links;
        }

        public dynamic createEntity(dynamic entity)
        {
            entity.createdBy = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
            entity.creationDate = DateTime.Now;
            entity.version = 0;
            entity.status = Utils.STATUS_ACTIVE;

            return entity;
        }
        public dynamic updateEntity(dynamic entity)
        {
            entity.modifiedBy = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
            entity.modificationDate = DateTime.Now;
            entity.version = entity.version + 1;

            return entity;
        }
        public dynamic deleteEntity(dynamic entity)
        {
            entity.deletedBy = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
            entity.deletedDate = DateTime.Now;
            entity.version = entity.version + 1;
            entity.status = Utils.STATUS_DELETED;


            return entity;
        }
        public int GetStatusByName(string status)
        {
            SMARTEntities db = new SMARTEntities();


            return db.EntityStatus.Where(e=>e.EntityStatusName.Contains( status)).FirstOrDefault().systemId;
        }

        public Dictionary<string, string> renderJson(object entity)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            var properties = entity.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                data.Add(property.Name, property.GetConstantValue().ToString());
            }
            return data;
        }

        public void displayEntityInfo(long? createdBy, long? modifiedBy, long? deletedBy, int? status = null)
        {
           SMARTEntities db = new SMARTEntities(); /*var db = new UsersModel();*/
            if (createdBy != null)
                TempData["display_createdBy"] = db.User.FirstOrDefault(u => u.systemId == createdBy).UserName;
            else
                TempData["display_createdBy"] = "";

            if (modifiedBy != null)
                TempData["display_modifiedBy"] = db.User.FirstOrDefault(u => u.systemId == modifiedBy).UserName;
            else
                TempData["display_modifiedBy"] = "";

            if (deletedBy != null)
                TempData["display_deletedBy"] = db.User.FirstOrDefault(u => u.systemId == deletedBy).UserName;
            else
                TempData["display_deletedBy"] = "";

            if (status != null)
                TempData["display_status"] = db.EntityStatus.FirstOrDefault(u => u.systemId == status).EntityStatusName;
            else
                TempData["display_status"] = "";
        }
        public long GetUserType(User user)
        {
           SMARTEntities db = new SMARTEntities(); /*var db = new UsersModel();*/
            var typeId = db.ServiceProviderType.FirstOrDefault(spt => spt.systemId.Equals(Utils.CSP)).systemId;
            return typeId;
        }
        public User getUser()
        {
           SMARTEntities db = new SMARTEntities(); /*var db = new UsersModel();*/
            var userid = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
            var user = db.User.FirstOrDefault(u => u.systemId == userid);
            return user;
        }
    }

}