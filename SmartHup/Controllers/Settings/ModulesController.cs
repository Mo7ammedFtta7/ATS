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

namespace SmartHup.Controllers.Settings
{
    [Authorized]

    public class ModulesController : Base
    {

        private SMARTEntities db = new SMARTEntities();

        // GET: Modules
        public async Task<ActionResult> Index()
        {
            var module = db.Module.Include(m => m.EntityStatus).Include(m => m.Module2);
            return View(await module.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Module.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.parentId = new SelectList(db.Module, "systemId", "name");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,name,Ename,parentId,creationDate,modificationDate,deletedDate,status,version,entityStatus_systemId,createdBy,modifiedBy,deletedBy")] Module module)
        {
            if (ModelState.IsValid)
            {
				  module = (Module)createEntity( module);
                db.Module.Add(module);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", module.entityStatus_systemId);
            ViewBag.parentId = new SelectList(db.Module, "systemId", "name", module.parentId);
            return View(module);
        }

        // GET: Modules/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Module.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", module.entityStatus_systemId);
            ViewBag.parentId = new SelectList(db.Module, "systemId", "name", module.parentId);
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,name,Ename,parentId,creationDate,modificationDate,deletedDate,status,version,entityStatus_systemId,createdBy,modifiedBy,deletedBy")] Module module)
        {
            if (ModelState.IsValid)
            {
				module = (Module)updateEntity( module);
                db.Entry(module).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", module.entityStatus_systemId);
            ViewBag.parentId = new SelectList(db.Module, "systemId", "name", module.parentId);
            return View(module);
        }

        // GET: Modules/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Module.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Module module = await db.Module.FindAsync(id);
			module = (Module)deleteEntity( module);
            
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
