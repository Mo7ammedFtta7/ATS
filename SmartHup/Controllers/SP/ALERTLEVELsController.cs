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

    public class ALERTLEVELsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: ALERTLEVELs
        public async Task<ActionResult> Index()
        {
            var aLERTLEVEL = db.ALERTLEVEL.Include(a => a.EntityStatus).Include(a => a.MONITORINGJOB).OrderByDescending(t => t.systemId);
            return View(await aLERTLEVEL.ToListAsync());
        }

        // GET: ALERTLEVELs/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALERTLEVEL aLERTLEVEL = await db.ALERTLEVEL.FindAsync(id);
            if (aLERTLEVEL == null)
            {
                return HttpNotFound();
            }
            return View(aLERTLEVEL);
        }

        // GET: ALERTLEVELs/Create
        public ActionResult Create()
        {
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.JOBID = new SelectList(db.MONITORINGJOB, "systemId", "JOBNAME");
            return View();
        }

        // POST: ALERTLEVELs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Notes,JOBID,MINTHRESHOLD,regex,MAXTHRESHOLD,ALERTLEVEL1")] ALERTLEVEL aLERTLEVEL)
        {
            if (ModelState.IsValid)
            {
     
                aLERTLEVEL.entityStatus_systemId = Utils.STATUS_ACTIVE;

                db.ALERTLEVEL.Add(aLERTLEVEL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", aLERTLEVEL.entityStatus_systemId);
            ViewBag.JOBID = new SelectList(db.MONITORINGJOB, "systemId", "JOBNAME", aLERTLEVEL.JOBID);
            return View(aLERTLEVEL);
        }

        // GET: ALERTLEVELs/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALERTLEVEL aLERTLEVEL = await db.ALERTLEVEL.FindAsync(id);
            if (aLERTLEVEL == null)
            {
                return HttpNotFound();
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", aLERTLEVEL.entityStatus_systemId);
            ViewBag.JOBID = new SelectList(db.MONITORINGJOB, "systemId", "JOBNAME", aLERTLEVEL.JOBID);
            return View(aLERTLEVEL);
        }

        // POST: ALERTLEVELs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,Version,STATUS,Notes,JOBID,MINTHRESHOLD,regex,MAXTHRESHOLD,ALERTLEVEL1,entityStatus_systemId")] ALERTLEVEL aLERTLEVEL)
        {
            if (ModelState.IsValid)
            {
				aLERTLEVEL = (ALERTLEVEL)updateEntity( aLERTLEVEL);
                db.Entry(aLERTLEVEL).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", aLERTLEVEL.entityStatus_systemId);
            ViewBag.JOBID = new SelectList(db.MONITORINGJOB, "systemId", "JOBNAME", aLERTLEVEL.JOBID);
            return View(aLERTLEVEL);
        }

        // GET: ALERTLEVELs/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ALERTLEVEL aLERTLEVEL = await db.ALERTLEVEL.FindAsync(id);
            if (aLERTLEVEL == null)
            {
                return HttpNotFound();
            }
            return View(aLERTLEVEL);
        }

        // POST: ALERTLEVELs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            ALERTLEVEL aLERTLEVEL = await db.ALERTLEVEL.FindAsync(id);
            aLERTLEVEL.entityStatus_systemId = Utils.STATUS_DELETED;

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
