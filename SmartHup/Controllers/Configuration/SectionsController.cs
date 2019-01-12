using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartHup.Models;

namespace SmartHup.Controllers.Settings
{
    [Authorized]
    public class SectionsController : Base
    {
        private TicketsEntities db = new TicketsEntities();

        // GET: Sections
        public ActionResult Index()
        {
            var sections = db.Sections.Include(s => s.Company).Include(s => s.Department).Include(s => s.EntityStatu);
            return View(sections.Where(se => se.entityStatus_systemId==1).ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Sections/Create
        public ActionResult Create()
        {
            ViewBag.companyId = new SelectList(db.Companies.Where(im => im.entityStatus_systemId == 1), "systemId", "CompanyName");
            ViewBag.DepartmentId = new SelectList(db.Departments.Where(im => im.entityStatus_systemId == 1), "systemId", "DepartmentName");
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "systemId,SectionName,SectionArName,DepartmentId,companyId,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,entityStatus_systemId,version")] Section section)
        {
            var section_name = db.Sections.Where(i => i.DepartmentId == section.DepartmentId && i.companyId== section.companyId).FirstOrDefault();
            if (section_name != null)
            {
                if (section.SectionName == section_name.SectionName )
                {
                    ModelState.AddModelError("SectionName", "Section Name already exists in this Department");

                }
            }
            if (section_name != null)
            {
                if (section.SectionArName == section_name.SectionArName)
                {
                    ModelState.AddModelError("SectionArName", "Section Arabic Name already exists in this Department");
                }   
            }
            
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                section = (Section)createEntity(section);
                db.Entry(section).State = EntityState.Modified;

                db.Sections.Add(section);
                db.SaveChanges();
				log(section);
                return RedirectToAction("Index");
            }

            ViewBag.companyId = new SelectList(db.Companies, "systemId", "CompanyName", section.companyId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "systemId", "DepartmentName", section.DepartmentId);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", section.entityStatus_systemId);
            return View(section);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyId = new SelectList(db.Companies.Where(im => im.entityStatus_systemId == 1), "systemId", "CompanyName", section.companyId);
            ViewBag.DepartmentId = new SelectList(db.Departments.Where(im => im.entityStatus_systemId == 1), "systemId", "DepartmentName", section.DepartmentId);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", section.entityStatus_systemId);
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "systemId,SectionName,SectionArName,DepartmentId,companyId,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,entityStatus_systemId,version")] Section section)
        {
            var section_name = db.Sections.Where(i => i.DepartmentId == section.DepartmentId && i.companyId == section.companyId).FirstOrDefault();
            if (section_name != null)
            {
                if (section.SectionName == section_name.SectionName)
                {
                    ModelState.AddModelError("SectionName", "Section Name already exists in this Department");

                }
                 else if (section.SectionArName == section_name.SectionArName)
                    {
                        ModelState.AddModelError("SectionArName", "Section Arabic Name already exists in this Department");
                    }
            }
              else
                {
                    if (ModelState.IsValid)
                    {
                        section = (Section)updateEntity(section);
                        db.Entry(section).State = EntityState.Modified;

                        Console.Write(section);
                        var old = db.Sections.Find(section.systemId);
                        db.Entry(old).CurrentValues.SetValues(section);
                        db.SaveChanges();
                        log(section);
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.companyId = new SelectList(db.Companies, "systemId", "CompanyName", section.companyId);
                ViewBag.DepartmentId = new SelectList(db.Departments, "systemId", "DepartmentName", section.DepartmentId);
                ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", section.entityStatus_systemId);

                return View(section);
            }
        // GET: Sections/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Section section = db.Sections.Find(id);
            section = (Section)deleteEntity(section);
            
            section.entityStatus_systemId = Utils.STATUS_DELETED;
            db.Entry(section).State = EntityState.Modified;
            db.SaveChanges();
			log(section);
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
