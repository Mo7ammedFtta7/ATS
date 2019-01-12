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
    public class DepartmentsController : Base
    {
        private TicketsEntities db = new TicketsEntities();

        // GET: Departments
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.Company).Include(d => d.EntityStatu).Include(d => d.User);
            
            return View(departments.Where(im => im.entityStatus_systemId == 1).ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies.Where(im=>im.entityStatus_systemId == 1), "systemId", "CompanyName");
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.DepartmentHead = new SelectList(db.Users, "systemId", "UserName");
         

            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "systemId,DepartmentName,DepartmentArName,Descrtiption,CompanyId,DepartmentHead,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,entityStatus_systemId,version,createdBy")] Department department)
        {
          
            var dep_name = db.Departments.Where(i => i.CompanyId == department.CompanyId).FirstOrDefault();
            if (dep_name!=null) {
                if (department.DepartmentName == dep_name.DepartmentName)
                {
                    ModelState.AddModelError("DepartmentName", "Department Name already exists in this Company");

                } }

            if (dep_name != null)
            {
                if (department.DepartmentArName == dep_name.DepartmentArName)
                {
                    ModelState.AddModelError("DepartmentArName", "Department Arabic Name already exists in this Company");

                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                department = (Department)createEntity(department);
                db.Entry(department).State = EntityState.Added;

                db.Departments.Add(department);
                db.SaveChanges();
                log(department);
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "systemId", "CompanyName", department.CompanyId);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", department.entityStatus_systemId);
            ViewBag.DepartmentHead = new SelectList(db.Users, "systemId", "UserName", department.DepartmentHead);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies.Where(im => im.entityStatus_systemId == 1), "systemId", "CompanyName");
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", department.entityStatus_systemId);
            ViewBag.DepartmentHead = new SelectList(db.Users, "systemId", "UserName", department.DepartmentHead);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "systemId,DepartmentName,DepartmentArName,Descrtiption,CompanyId,DepartmentHead,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,entityStatus_systemId,version,createdBy")] Department department)
        {

            var dep_name = db.Departments.Where(i => i.CompanyId == department.CompanyId).FirstOrDefault();
            if (dep_name != null)
            {
                if (department.DepartmentName == dep_name.DepartmentName)
                {
                    ModelState.AddModelError("DepartmentName", "Department Name already exists in this Company");

                }
            }
            if (ModelState.IsValid)
            {
                department = (Department)updateEntity(department);
                db.Entry(department).State = EntityState.Modified;
                
                Console.Write(department);
                var old = db.Departments.Find(department.systemId);
                db.Entry(old).CurrentValues.SetValues(department);

                
                db.SaveChanges();
                log(department);
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "systemId", "CompanyName", department.CompanyId);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", department.entityStatus_systemId);
            ViewBag.DepartmentHead = new SelectList(db.Users, "systemId", "UserName", department.DepartmentHead);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {

            Department department = db.Departments.Find(id);
            Section section = db.Sections.Where(s => s.DepartmentId == id).FirstOrDefault();

            department = (Department)deleteEntity(department);
            db.Entry(department).State = EntityState.Modified;
           
            if (section != null)
            {
                section.entityStatus_systemId = Utils.STATUS_DELETED;
                db.Entry(section).State = EntityState.Modified;

                db.SaveChanges();
                log(section);
            }
            db.SaveChanges();
            log(department);
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
