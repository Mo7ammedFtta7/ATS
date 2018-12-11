using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartHup.Models;
 

namespace SmartHup.Controllers
{
    [Authorized]

    public class ServiceProviderController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: ServiceProvider
        public ActionResult Index()
        {
            var entities = db.EntityStatus;
            ViewBag.status = new SelectList(entities, "systemId", "EntityStatusName");
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (var state in entities)
                dic.Add(state.systemId, state.EntityStatusName);
            ViewBag.statusDictionary = dic;
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                return RedirectToAction("Details", new { id = user.serviceProviderId });
            }
            else
            {
                return View(db.ServiceProvider.Include(s => s.ServiceProviderType).OrderByDescending(t => t.systemId).ToList());
            }
            //return View(serviceProvider.ToList());
        }

        // GET: ServiceProvider/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProvider.Find(id);
                if (serviceProvider == null)
            {
                return HttpNotFound();
            }
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                displayEntityInfo(serviceProvider.createdBy, serviceProvider.modifiedBy, serviceProvider.deletedBy, serviceProvider.status);
                var SStauts = Convert.ToInt32(serviceProvider.serviceProviderStatus);
                TempData["display_Sstatus"] = db.EntityStatus.FirstOrDefault(u => u.systemId == SStauts).EntityStatusName;
                return View(user.ServiceProvider);
            }
            else
            {
                displayEntityInfo(serviceProvider.createdBy, serviceProvider.modifiedBy, serviceProvider.deletedBy, serviceProvider.status);
                var SStauts = Convert.ToInt32(serviceProvider.serviceProviderStatus);
                TempData["display_Sstatus"] = db.EntityStatus.FirstOrDefault(u => u.systemId == SStauts).EntityStatusName;
                return View(serviceProvider);
            }

           
        }

        // GET: ServiceProvider/Create
        public ActionResult Create()
        {
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType.Where(spt => spt.systemId.Equals(Utils.CSP)), "systemId", "Name");

            }
            else
                ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType, "systemId", "Name");
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            return View();
        }

        // POST: ServiceProvider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "systemId,serviceProviderId,serviceProviderName,serviceProviderEName,serviceProviderLabel,serviceProviderELabel,serviceProviderStatus,serviceProviderTypeId,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,status,version")] ServiceProvider serviceProvider)
        {
            ServiceProvider SP = db.ServiceProvider.FirstOrDefault(f=>f.serviceProviderId == serviceProvider.serviceProviderId);
            if(SP==null)
            if (ModelState.IsValid)
            {
                serviceProvider = (ServiceProvider)createEntity(serviceProvider);
                db.ServiceProvider.Add(serviceProvider);
                db.SaveChanges();
                log(serviceProvider);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("serviceProviderId", "This field is Unrepeated");
            ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType, "systemId", "Name", serviceProvider.serviceProviderTypeId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            return View(serviceProvider);
        }

        // GET: ServiceProvider/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProvider.Find(id);
            if (serviceProvider == null)
            {
                return HttpNotFound();
            }
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType.Where(spt => spt.systemId.Equals(Utils.CSP)), "systemId", "Name", serviceProvider.serviceProviderTypeId);
                ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceProvider.status);
                return View(user.ServiceProvider);
            }
            else
            {
                ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType, "systemId", "Name", serviceProvider.serviceProviderTypeId);
                ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceProvider.status);
                return View(serviceProvider);
            }
        }

        // POST: ServiceProvider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "systemId,serviceProviderId,serviceProviderName,serviceProviderEName,serviceProviderLabel,serviceProviderELabel,serviceProviderStatus,serviceProviderTypeId,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,status,version")] ServiceProvider serviceProvider)
        {
            if (ModelState.IsValid)
            {
                serviceProvider = (ServiceProvider)updateEntity(serviceProvider);

                db.Entry(serviceProvider).State = EntityState.Modified;
                db.SaveChanges();
                log(serviceProvider);
                return RedirectToAction("Index");
            }
            ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType, "systemId", "Name", serviceProvider.serviceProviderTypeId);
            return View(serviceProvider);
        }

        // GET: ServiceProvider/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProvider.Find(id);
            if (serviceProvider == null)
            {
                return HttpNotFound();
            }
            return View(serviceProvider);
        }

        // POST: ServiceProvider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            ServiceProvider serviceProvider = db.ServiceProvider.Find(id);
            serviceProvider = (ServiceProvider)deleteEntity(serviceProvider);
            db.SaveChanges();
            log(serviceProvider);
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
