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

namespace SmartHup.Controllers.SP
{
    [Authorized]

    public class MonitoringController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: Monitoring
        public async Task<ActionResult> Index()
        {
            var mONITORINGJOB = db.MONITORINGJOB.Include(m => m.EntityStatus).Include(m => m.NotificationGroup).OrderByDescending(t => t.systemId);
            return View(await mONITORINGJOB.ToListAsync());
        }

        // GET: Monitoring/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONITORINGJOB mONITORINGJOB = await db.MONITORINGJOB.FindAsync(id);
            if (mONITORINGJOB == null)
            {
                return HttpNotFound();
            }
            return View(mONITORINGJOB);
        }

        // GET: Monitoring/Create
        public ActionResult Create()
        {
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.notificationGroupId = new SelectList(db.NotificationGroup, "systemId", "notificationGroupName");
            ViewBag.moduleid = new SelectList(db.Module, "systemId", "name");

            return View();
        }

        // POST: Monitoring/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Version,STATUS,moduleid,Notes,JOBID,JOBNAME,MONITOREDENTITYID,CHECKTYPE,CHECKTYPEPARAMETER,MONITORINGSTRING,cronExpression,jobStatus,linkId,notificationGroupId")] MONITORINGJOB mONITORINGJOB)
        {
            if (ModelState.IsValid)
            {
                mONITORINGJOB.Version = 0;
                mONITORINGJOB.entityStatus_systemId = 1;
                db.MONITORINGJOB.Add(mONITORINGJOB);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", mONITORINGJOB.entityStatus_systemId);
            ViewBag.notificationGroupId = new SelectList(db.NotificationGroup, "systemId", "notificationGroupName", mONITORINGJOB.notificationGroupId);
            ViewBag.moduleid = new SelectList(db.Module, "systemId", "name",mONITORINGJOB.moduleid);

            return View(mONITORINGJOB);
        }

        // GET: Monitoring/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONITORINGJOB mONITORINGJOB = await db.MONITORINGJOB.FindAsync(id);
            if (mONITORINGJOB == null)
            {
                return HttpNotFound();
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", mONITORINGJOB.entityStatus_systemId);
            ViewBag.notificationGroupId = new SelectList(db.NotificationGroup, "systemId", "notificationGroupName", mONITORINGJOB.notificationGroupId);
            ViewBag.moduleid = new SelectList(db.Module, "systemId", "name", mONITORINGJOB.moduleid);

            return View(mONITORINGJOB);
        }

        // POST: Monitoring/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,Version,STATUS,moduleid,Notes,JOBID,JOBNAME,MONITOREDENTITYID,CHECKTYPE,CHECKTYPEPARAMETER,MONITORINGSTRING,cronExpression,jobStatus,linkId,entityStatus_systemId,notificationGroupId")] MONITORINGJOB mONITORINGJOB)
        {
            if (ModelState.IsValid)
            {
                mONITORINGJOB.Version = mONITORINGJOB.Version +1;
                db.Entry(mONITORINGJOB).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", mONITORINGJOB.entityStatus_systemId);
            ViewBag.notificationGroupId = new SelectList(db.NotificationGroup, "systemId", "notificationGroupName", mONITORINGJOB.notificationGroupId);
            ViewBag.moduleid = new SelectList(db.Module, "systemId", "name", mONITORINGJOB.moduleid);

            return View(mONITORINGJOB);
        }

        // GET: Monitoring/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONITORINGJOB mONITORINGJOB = await db.MONITORINGJOB.FindAsync(id);
            if (mONITORINGJOB == null)
            {
                return HttpNotFound();
            }
            return View(mONITORINGJOB);
        }

        // POST: Monitoring/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            MONITORINGJOB mONITORINGJOB = await db.MONITORINGJOB.FindAsync(id);
            //mONITORINGJOB = (MONITORINGJOB)deleteEntity( mONITORINGJOB);
            mONITORINGJOB.entityStatus_systemId = Utils.STATUS_DELETED;

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
