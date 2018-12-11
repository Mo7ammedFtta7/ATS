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

    public class ALERTsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: ALERTs
        public async Task<ActionResult> Index()
        {
            var aLERT = db.ALERT.Include(a => a.ALERTLEVEL1).Include(a => a.EntityStatus).Include(a => a.Role).Include(a => a.ServiceProvider).Include(a => a.User).Include(a => a.NotificationCategory).OrderByDescending(t => t.systemId);
            return View(await aLERT.ToListAsync());
        }

        // GET: ALERTs/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALERT aLERT = await db.ALERT.FindAsync(id);
            if (aLERT == null)
            {
                return HttpNotFound();
            }
            return View(aLERT);
        }

        // GET: ALERTs/Create
        public ActionResult Create()
        {
            ViewBag.ALERTLEVEL = new SelectList(db.ALERTLEVEL , "systemId", "ALERTLEVEL1");
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.roleId = new SelectList(db.Role, "systemId", "Name");
            ViewBag.spId = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId");
            ViewBag.userId = new SelectList(db.User, "systemId", "UserName");
            ViewBag.notificationCategoryId = new SelectList(db.NotificationCategory, "systemId", "notificationCategoryName");
            return View();
        }

        // POST: ALERTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,Version,STATUS,Notes,ALERTLEVEL,ALERTMESSAGE,ALERTMEDIUM,RECEIVERNAME,RECEIVERADDRESS,userId,spId,roleId,entityStatus_systemId,notificationCategoryId")] ALERT aLERT)
        {
            if (ModelState.IsValid)
            {
				  //aLERT = (ALERT)createEntity( aLERT);
                db.ALERT.Add(aLERT);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ALERTLEVEL = new SelectList(db.ALERTLEVEL, "systemId", "ALERTLEVEL1", aLERT.ALERTLEVEL);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", aLERT.entityStatus_systemId);
            ViewBag.roleId = new SelectList(db.Role, "systemId", "Name", aLERT.roleId);
            ViewBag.spId = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", aLERT.spId);
            ViewBag.userId = new SelectList(db.User, "systemId", "UserName", aLERT.userId);
            ViewBag.notificationCategoryId = new SelectList(db.NotificationCategory, "systemId", "notificationCategoryName", aLERT.notificationCategoryId);
            return View(aLERT);
        }

        // GET: ALERTs/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALERT aLERT = await db.ALERT.FindAsync(id);
            if (aLERT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ALERTLEVEL = new SelectList(db.ALERTLEVEL , "systemId", "ALERTLEVEL1", aLERT.ALERTLEVEL);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", aLERT.entityStatus_systemId);
            ViewBag.roleId = new SelectList(db.Role, "systemId", "Name", aLERT.roleId);
            ViewBag.spId = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", aLERT.spId);
            ViewBag.userId = new SelectList(db.User, "systemId", "UserName", aLERT.userId);
            ViewBag.notificationCategoryId = new SelectList(db.NotificationCategory, "systemId", "notificationCategoryName", aLERT.notificationCategoryId);
            return View(aLERT);
        }

        // POST: ALERTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,Version,STATUS,Notes,ALERTLEVEL,ALERTMESSAGE,ALERTMEDIUM,RECEIVERNAME,RECEIVERADDRESS,userId,spId,roleId,entityStatus_systemId,notificationCategoryId")] ALERT aLERT)
        {
            if (ModelState.IsValid)
            {
				//aLERT = (ALERT)updateEntity( aLERT);
                db.Entry(aLERT).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ALERTLEVEL = new SelectList(db.ALERTLEVEL, "systemId", "ALERTLEVEL1", aLERT.ALERTLEVEL);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", aLERT.entityStatus_systemId);
            ViewBag.roleId = new SelectList(db.Role, "systemId", "Name", aLERT.roleId);
            ViewBag.spId = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", aLERT.spId);
            ViewBag.userId = new SelectList(db.User, "systemId", "UserName", aLERT.userId);
            ViewBag.notificationCategoryId = new SelectList(db.NotificationCategory, "systemId", "notificationCategoryName", aLERT.notificationCategoryId);
            return View(aLERT);
        }

        // GET: ALERTs/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALERT aLERT = await db.ALERT.FindAsync(id);
            if (aLERT == null)
            {
                return HttpNotFound();
            }
            return View(aLERT);
        }

        // POST: ALERTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            ALERT aLERT = await db.ALERT.FindAsync(id);
			aLERT = (ALERT)deleteEntity( aLERT);
            
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
