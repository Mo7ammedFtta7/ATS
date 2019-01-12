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

    public class ActionsController : Base
    {
        private TicketsEntities db = new TicketsEntities();

        // GET: Actions
        public async Task<ActionResult> Index()
        {
            var action = db.Actions.Include(a => a.Module);
            return View(await action.ToListAsync());
        }

        // GET: Actions/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Action action = await db.Actions.FindAsync(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // GET: Actions/Create
        public ActionResult Create()
        {
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.moduleId = new SelectList(db.Modules, "systemId", "name");
            return View();
        }

        // POST: Actions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int moduleId)
        {
            Models.Action action = new Models.Action();
                for (int i = 0; i < 4; i++)
                {
                    string name = i == 0 ? "Index" :( i == 1 ? "Detsils" :( i == 2 ? "Create" :( i == 3 ? "Delete" : "Edit")));
                    action.Elabel = name;
                    action.name = name;
                    action.label = name;
                    action.pageType = name == "Index" ? "NormalPage" : "FunctionPage";
                    action.createdBy = 1;
                    action.modifiedBy = 1;
                    action.creationDate = DateTime.Now;
                    action.entityStatus_systemId = 1;
                    //action.status = 1;
                    action.version = 0;
                action.moduleId = moduleId;
                if (db.Actions.Where(a=>a.name==name&&a.moduleId==moduleId).Count()==0)
                {
                    db.Actions.Add(action);

                }

            }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
         
        }

        // GET: Actions/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Action action = await db.Actions.FindAsync(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", action.entityStatus_systemId);
            ViewBag.moduleId = new SelectList(db.Modules, "systemId", "name", action.moduleId);
            return View(action);
        }

        // POST: Actions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,name,Ename,label,Elabel,pageType,moduleId,creationDate,modificationDate,deletedDate,status,version,entityStatus_systemId,createdBy,modifiedBy,deletedBy")] Models.Action action)
        {
            if (ModelState.IsValid)
            {
				action = (Models.Action)updateEntity( action);
                db.Entry(action).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", action.entityStatus_systemId);
            ViewBag.moduleId = new SelectList(db.Modules, "systemId", "name", action.moduleId);
            return View(action);
        }

        // GET: Actions/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Action action = await db.Actions.FindAsync(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // POST: Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Models.Action action = await db.Actions.FindAsync(id);
			action = (Models.Action)deleteEntity( action);
            
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
