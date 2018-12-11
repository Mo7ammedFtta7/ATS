using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartHup.Models;

namespace SmartHup.Controllers
{
    [Authorized]

    public class NotificationsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: Notifications
        public async Task<ActionResult> Index()
        {
            return View(await db.MONITORINGLOG.Where(m=>m.userId==user_info.user.systemId).OrderByDescending(t => t.systemId).ToListAsync());
        }

        public JsonResult NotificationsList()
        {
            return Json(db.MONITORINGLOG.Where(m => m.userId == user_info.user.systemId && m.isRead==false).Select(s => new { type = db.Module.Where(y=>y.systemId==  s.moduleId.Value).FirstOrDefault().Ename ,sysid=s.systemId, mres = s.MONITORINGRESULT, res = s.result, date=s.MONITORINGTIME.ToString(),id=s.objectId, status =s.isRead}).OrderByDescending(s=>s.date).ToList(), JsonRequestBehavior.AllowGet);
        }

        public string read(Int64 id)
        {
            try
            {
                MONITORINGLOG monlog = db.MONITORINGLOG.Where(m=>m.systemId==id).FirstOrDefault();
                monlog.isRead = true;
                db.Entry(monlog).State = EntityState.Modified;
                db.SaveChanges();
                return "true";
            }
            catch (Exception e)
            {

                return e.ToString();
            }
   


        }        // GET: Notifications/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONITORINGLOG mONITORINGLOG = await db.MONITORINGLOG.FindAsync(id);
            var alertid = Convert.ToInt64(mONITORINGLOG.alertId);
            read(alertid);

            //var alter= db.ALERT.Find(alertid);
            //ViewBag.ALERT = alter;
            // ALERTLEVEL   alertlevel = db.ALERTLEVEL.Find(alter.);
            //ViewBag.alertlevel = alertlevel;
            if (mONITORINGLOG == null)
            {
                return HttpNotFound();
            }
            return View(mONITORINGLOG);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,Version,STATUS,Notes,JOBID,MONITORINGTIME,roleId,spId,alertId,userId,isRead,MONITORINGRESULT,objectId,moduleId,result,entityStatus_systemId")] MONITORINGLOG mONITORINGLOG)
        {
            if (ModelState.IsValid)
            {
				  mONITORINGLOG = (MONITORINGLOG)createEntity( mONITORINGLOG);
                db.MONITORINGLOG.Add(mONITORINGLOG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mONITORINGLOG);
        }

        // GET: Notifications/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONITORINGLOG mONITORINGLOG = await db.MONITORINGLOG.FindAsync(id);
            if (mONITORINGLOG == null)
            {
                return HttpNotFound();
            }
            return View(mONITORINGLOG);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,Version,STATUS,Notes,JOBID,MONITORINGTIME,roleId,spId,alertId,userId,isRead,MONITORINGRESULT,objectId,moduleId,result,entityStatus_systemId")] MONITORINGLOG mONITORINGLOG)
        {
            if (ModelState.IsValid)
            {
				mONITORINGLOG = (MONITORINGLOG)updateEntity( mONITORINGLOG);
                db.Entry(mONITORINGLOG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mONITORINGLOG);
        }

        // GET: Notifications/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONITORINGLOG mONITORINGLOG = await db.MONITORINGLOG.FindAsync(id);
            if (mONITORINGLOG == null)
            {
                return HttpNotFound();
            }
            return View(mONITORINGLOG);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            MONITORINGLOG mONITORINGLOG = await db.MONITORINGLOG.FindAsync(id);
			mONITORINGLOG = (MONITORINGLOG)deleteEntity( mONITORINGLOG);
            
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
