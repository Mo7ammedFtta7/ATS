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
    public class UserGroupsController : Base
    {
        private TicketsEntities db = new TicketsEntities();

        // GET: UserGroups
        public async Task<ActionResult> Index()
        {
            var userGroup = db.UserGroups.Include(u => u.EntityStatu).Include(u => u.GroupType);
            return View(await userGroup.ToListAsync());
        }

        // GET: UserGroups/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = await db.UserGroups.FindAsync(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        // GET: UserGroups/Create
        public ActionResult Create()
        {
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "systemId", "GroupTypeName");
            return View();
        }

        // POST: UserGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,UserGroupName,UserGroupArName,Description,GroupTypeId,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,version,entityStatus_systemId")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
				  userGroup = (UserGroup)createEntity( userGroup);
                db.UserGroups.Add(userGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", userGroup.entityStatus_systemId);
            ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "systemId", "GroupTypeName", userGroup.GroupTypeId);
            return View(userGroup);
        }

        // GET: UserGroups/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = await db.UserGroups.FindAsync(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", userGroup.entityStatus_systemId);
            ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "systemId", "GroupTypeName", userGroup.GroupTypeId);
            return View(userGroup);
        }

        // POST: UserGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,UserGroupName,UserGroupArName,Description,GroupTypeId,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,version,entityStatus_systemId")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
				userGroup = (UserGroup)updateEntity( userGroup);
                db.Entry(userGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", userGroup.entityStatus_systemId);
            ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "systemId", "GroupTypeName", userGroup.GroupTypeId);
            return View(userGroup);
        }

        // GET: UserGroups/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = await db.UserGroups.FindAsync(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            UserGroup userGroup = await db.UserGroups.FindAsync(id);
			userGroup = (UserGroup)deleteEntity( userGroup);
            
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
