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
    public class StatesController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: States
        public async Task<ActionResult> Index()
        {
            return View(await db.State.ToListAsync());
        }

        // GET: States/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = await db.State.FindAsync(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // GET: States/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,stateName,stateArName")] State state)
        {
            if (ModelState.IsValid)
            {
				  state = (State)createEntity( state);
                db.State.Add(state);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(state);
        }

        // GET: States/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = await db.State.FindAsync(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,stateName,stateArName")] State state)
        {
            if (ModelState.IsValid)
            {
				state = (State)updateEntity( state);
                db.Entry(state).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = await db.State.FindAsync(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            State state = await db.State.FindAsync(id);
			state = (State)deleteEntity( state);
            
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
