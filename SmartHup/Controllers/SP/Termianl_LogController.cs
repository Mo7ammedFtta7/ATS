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
    public class Termianl_LogController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: Termianl_Log
        public async Task<ActionResult> Index()
        {
            var termianl_Log = db.Termianl_Log.Include(t => t.EntityStatus).Include(t => t.REASON).Include(t => t.Terminal).Include(t => t.TerminalComponent).Include(t => t.TerminalState).Include(t => t.TerminalStatusDictionary);
            return View(await termianl_Log.ToListAsync());
        }

        // GET: Termianl_Log/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Termianl_Log termianl_Log = await db.Termianl_Log.FindAsync(id);
            if (termianl_Log == null)
            {
                return HttpNotFound();
            }
            return View(termianl_Log);
        }

        // GET: Termianl_Log/Create
        public ActionResult Create()
        {
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.REASON_CODE = new SelectList(db.REASON, "systemId", "reasonCode");
            ViewBag.terId = new SelectList(db.Terminal, "systemId", "terminalName");
            ViewBag.componentId = new SelectList(db.TerminalComponent, "systemId", "tcCode");
            ViewBag.termianlState = new SelectList(db.TerminalState, "systemId", "terminalStateCode");
            ViewBag.ter_STATUS = new SelectList(db.TerminalStatusDictionary, "systemId", "tsdCode");
            return View();
        }

        // POST: Termianl_Log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,TL_Name,status,creationDate,modificationDate,createdBy,modifiedBy,version,terId,PACKNO,EX_ter_ID,ter_NAME,STOP_TIME,START_TIME,REASON_CODE,ter_STATUS,NOTIFIED,VALID_CASE,NOTIFICATION_PERIOD,DOWN_TIME,serialNo,componentId,messageCode,termianlStatus,termianlState,additionalF1,additionalF2,additionalF3")] Termianl_Log termianl_Log)
        {
            if (ModelState.IsValid)
            {
				  termianl_Log = (Termianl_Log)createEntity( termianl_Log);
                db.Termianl_Log.Add(termianl_Log);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", termianl_Log.status);
            ViewBag.REASON_CODE = new SelectList(db.REASON, "systemId", "reasonCode", termianl_Log.REASON_CODE);
            ViewBag.terId = new SelectList(db.Terminal, "systemId", "terminalName", termianl_Log.terId);
            ViewBag.componentId = new SelectList(db.TerminalComponent, "systemId", "tcCode", termianl_Log.componentId);
            ViewBag.termianlState = new SelectList(db.TerminalState, "systemId", "terminalStateCode", termianl_Log.termianlState);
            ViewBag.ter_STATUS = new SelectList(db.TerminalStatusDictionary, "systemId", "tsdCode", termianl_Log.ter_STATUS);
            return View(termianl_Log);
        }

        // GET: Termianl_Log/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Termianl_Log termianl_Log = await db.Termianl_Log.FindAsync(id);
            if (termianl_Log == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", termianl_Log.status);
            ViewBag.REASON_CODE = new SelectList(db.REASON, "systemId", "reasonCode", termianl_Log.REASON_CODE);
            ViewBag.terId = new SelectList(db.Terminal, "systemId", "terminalName", termianl_Log.terId);
            ViewBag.componentId = new SelectList(db.TerminalComponent, "systemId", "tcCode", termianl_Log.componentId);
            ViewBag.termianlState = new SelectList(db.TerminalState, "systemId", "terminalStateCode", termianl_Log.termianlState);
            ViewBag.ter_STATUS = new SelectList(db.TerminalStatusDictionary, "systemId", "tsdCode", termianl_Log.ter_STATUS);
            return View(termianl_Log);
        }

        // POST: Termianl_Log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,TL_Name,status,creationDate,modificationDate,createdBy,modifiedBy,version,terId,PACKNO,EX_ter_ID,ter_NAME,STOP_TIME,START_TIME,REASON_CODE,ter_STATUS,NOTIFIED,VALID_CASE,NOTIFICATION_PERIOD,DOWN_TIME,serialNo,componentId,messageCode,termianlStatus,termianlState,additionalF1,additionalF2,additionalF3")] Termianl_Log termianl_Log)
        {
            if (ModelState.IsValid)
            {
				termianl_Log = (Termianl_Log)updateEntity( termianl_Log);
                db.Entry(termianl_Log).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", termianl_Log.status);
            ViewBag.REASON_CODE = new SelectList(db.REASON, "systemId", "reasonCode", termianl_Log.REASON_CODE);
            ViewBag.terId = new SelectList(db.Terminal, "systemId", "terminalName", termianl_Log.terId);
            ViewBag.componentId = new SelectList(db.TerminalComponent, "systemId", "tcCode", termianl_Log.componentId);
            ViewBag.termianlState = new SelectList(db.TerminalState, "systemId", "terminalStateCode", termianl_Log.termianlState);
            ViewBag.ter_STATUS = new SelectList(db.TerminalStatusDictionary, "systemId", "tsdCode", termianl_Log.ter_STATUS);
            return View(termianl_Log);
        }

        // GET: Termianl_Log/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Termianl_Log termianl_Log = await db.Termianl_Log.FindAsync(id);
            if (termianl_Log == null)
            {
                return HttpNotFound();
            }
            return View(termianl_Log);
        }

        // POST: Termianl_Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Termianl_Log termianl_Log = await db.Termianl_Log.FindAsync(id);
			termianl_Log = (Termianl_Log)deleteEntity( termianl_Log);
            
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
