using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartHup.Models;
using Newtonsoft.Json;


namespace SmartHup.Controllers
{
    [Authorized]

    public class AuditingLogController : Base
    {
        private SMARTEntities db = new SMARTEntities();
        // GET: AuditingLogs
        public ActionResult Index()
        {
            List<AuditingLog> auditingLogs = new List<AuditingLog>();
            var userid = user_info.user.systemId;
            var user = db.User.FirstOrDefault(u => u.systemId == userid);
            var typeId = db.ServiceProviderType.FirstOrDefault(spt => spt.systemId.Equals(Utils.CSP)).systemId;
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
                auditingLogs = db.AuditingLog.Include(a => a.Action).Include(a => a.Module).Include(a => a.User)
                    .Where(o => o.userId == userid).ToList();
            else
                auditingLogs = db.AuditingLog.Include(a => a.Action).Include(a => a.Module).Include(a => a.User).ToList();

            return View(auditingLogs.ToList());
        }

        // GET: AuditingLogs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditingLog auditingLog = db.AuditingLog.Find(id);
            if (auditingLog == null)
            {
                return HttpNotFound();
            }
            return View(auditingLog);
            //var obj = JsonConvert.DeserializeObjectAsync(auditingLog.oldData);
            //var result = renderJson(obj);
            //return View("showJsonData", result);
        }
        public ActionResult showData(string txt)
        {
            object data = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt);
            //var result = renderJson(data);
            return View(data);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public Dictionary<string, string> showJsonData(object data)
        {
            var result = renderJson(data);
            return result;
        }
    }
}
