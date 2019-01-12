using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartHup.Models;

namespace SmartHup.Controllers.Configuration
{[Authorized]
    public class OperatingTimesController : Base
    {
        private TicketsEntities db = new TicketsEntities();

        // GET: OperatingTimes
        public ActionResult Index()
        {
           
            return View(db.OperatingTimes.ToList());
        }

       // GET: OperatingTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperatingTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SystemId,OperatingDay,FromTime,ToTime,createdBy,creationDate,version,entityStatus_systemId,IsSelected")] OperatingTime operatingTime)
        {
            bool IsDayExist = db.OperatingTimes.Any(x => x.OperatingDay == operatingTime.OperatingDay);
            if (IsDayExist == true)
            {
                ModelState.AddModelError("OperatingDay", "The Day already exists");

            }
           if (ModelState.IsValid)
            {
				  operatingTime = (OperatingTime)createEntity( operatingTime);
                //operatingTime.OperatingDay = DateTime.Now.ToString("dddd"); 
                db.OperatingTimes.Add(operatingTime);
                db.SaveChanges();
				log(operatingTime);
                return RedirectToAction("Index");
            }
           return View(operatingTime);
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperatingTime operatingTime = db.OperatingTimes.Find(id);
            if (operatingTime == null)
            {
                return HttpNotFound();
            }
           return View(operatingTime);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SystemId,OperatingDay,FromTime,ToTime,createdBy,creationDate,version,entityStatus_systemId,IsSelected")] OperatingTime operatingTime)
        {
           var isExist = db.OperatingTimes.Any(x => x.SystemId == operatingTime.SystemId && x.OperatingDay == operatingTime.OperatingDay || x.OperatingDay != operatingTime.OperatingDay);
            var isNameEexist = db.OperatingTimes.Any(x => x.SystemId != operatingTime.SystemId && x.OperatingDay == operatingTime.OperatingDay);
           if (isNameEexist == true)
            {
                ModelState.AddModelError("OperatingDay", "OperatingDay  already exists");

            }

            if (isExist == true)
            {
                if (ModelState.IsValid)
                {
                    operatingTime = (OperatingTime)createEntity(operatingTime);
                    db.Entry(operatingTime).State = EntityState.Modified;
                    db.SaveChanges();
                    log(operatingTime);
                    return RedirectToAction("Index");
                }
            }
            return View(operatingTime);
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
