using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartHup.Models;
using System.Text.RegularExpressions;


namespace SmartHup.Controllers.Settings
{
    [Authorized]
    public class CompaniesController  : Base
    {
        private TicketsEntities db = new TicketsEntities();

        // GET: Companies
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.EntityStatu).Include(c => c.SLA);
            return View(companies.Where(im => im.entityStatus_systemId == 1).ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            displayEntityInfo(company.createdBy, company.modifiedBy, company.deletedBy);
           // company.createdBy = user_info.user.createdBy;
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.SLAId = new SelectList(db.SLAs, "systemId", "SLA_Name");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "systemId,CompanyName,CompanyArName,Description,Address,Phone,Email,Note,SLAId,createdBy,modifiedBy,creationDate,modificationDate,entityStatus_systemId,version")] Company company)
        {
           
           bool IsCompanyNameExist = db.Companies.Any(x => x.CompanyName == company.CompanyName);
            if (IsCompanyNameExist == true)
            {
                ModelState.AddModelError("CompanyName", "Company Name already exists");
                
            }
            bool IsCompanyARNameExist = db.Companies.Any(x => x.CompanyArName == company.CompanyArName);
            if (IsCompanyARNameExist == true)
            {
                ModelState.AddModelError("CompanyArName", "Company Arabic Name already exists");

            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                company = (Company)createEntity(company);
                db.Entry(company).State = EntityState.Added;
               
                db.Companies.Add(company);
                db.SaveChanges();
                log(company);
                return RedirectToAction("Index");
            }

            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", company.entityStatus_systemId);
            ViewBag.SLAId = new SelectList(db.SLAs, "systemId", "SLA_Name", company.SLAId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", company.entityStatus_systemId);
            ViewBag.SLAId = new SelectList(db.SLAs, "systemId", "SLA_Name", company.SLAId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "systemId,CompanyName,CompanyArName,Description,Address,Phone,Email,Note,SLAId,createdBy,modifiedBy,creationDate,modificationDate,entityStatus_systemId,version")] Company company)
        {
            var IsExist = db.Companies.Any(x => x.systemId == company.systemId && x.CompanyName == company.CompanyName || x.CompanyName != company.CompanyName);
            var IsCompanyNameExist = db.Companies.Any(x => x.systemId != company.systemId && x.CompanyName == company.CompanyName);
            var IsCompanyARNameExist = db.Companies.Any(x => x.systemId != company.systemId && x.CompanyArName == company.CompanyArName);
            if (IsCompanyNameExist == true)
            {
                ModelState.AddModelError("CompanyName", "Company Name  already exists");
            }
            if (IsCompanyARNameExist == true)
            {
                ModelState.AddModelError("CompanyArName", "Company Arabic Name  already exists");
            }
            if (IsExist == true)
            {
                if (ModelState.IsValid)
                {
                    company = (Company)createEntity(company);
                    db.Entry(company).State = EntityState.Modified;

                    Console.Write(company);
                    //var old = db.Companies.Find(company.systemId);
                    //db.Entry(old).CurrentValues.SetValues(company);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
                log(company);
                ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", company.entityStatus_systemId);
                ViewBag.SLAId = new SelectList(db.SLAs, "systemId", "SLA_Name", company.SLAId);
                return View(company);
            
        }
        // GET: Companies/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Company company = db.Companies.Find(id);
            Department dep = db.Departments.Where(i => i.CompanyId == id).FirstOrDefault();
            Section section = db.Sections.Where(s => s.companyId == id).FirstOrDefault();

                db.Entry(company).State = EntityState.Modified;
              
            //Delete Department
            if (dep!=null)
            {
                dep.entityStatus_systemId = Utils.STATUS_DELETED;
                db.Entry(dep).State = EntityState.Modified;
                db.SaveChanges();
                log(dep);
            }
            //Delete Section
            if (section != null)
            {
                dep.entityStatus_systemId = Utils.STATUS_DELETED;
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                log(section);
            }

            db.SaveChanges();
            log(company);
            
            
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
