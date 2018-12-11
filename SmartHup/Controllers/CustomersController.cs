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


namespace SmartHup.Controllers
{
    [Authorized]

    public class CustomersController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            var customer = db.customer/*.Include(c => c.e EntityStatus)*/;
            return View(await customer.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = await db.customer.FindAsync(id);
            ViewBag.fav = db.Favourite.Where(f => f.systemId == customer.SystemId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.isActive = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SystemId,Version,StartDate,EndDate,LastUpdated,phoneNo,email,fullName,dateOfBirth,job,customerIdNumber,customerIdType,idx,userName,password,IMEI,address,otpPassword,otpLastDate,otpStartDate,activationCode,isActive")] customer customer)
        {
            if (ModelState.IsValid)
            {
			 customer = (customer)createEntity(customer);
                db.customer.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.isActive = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", customer.isActive);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = await db.customer.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.isActive = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", customer.isActive);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SystemId,Version,StartDate,EndDate,LastUpdated,phoneNo,email,fullName,dateOfBirth,job,customerIdNumber,customerIdType,idx,userName,password,IMEI,address,otpPassword,otpLastDate,otpStartDate,activationCode,isActive")] customer customer)
        {
            if (ModelState.IsValid)
            {
				//customer = (customer)updateEntity( customer);
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.isActive = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", customer.isActive);
            return View(customer);
        }



     
        public async Task<ActionResult> func(int id,int func)
        {
            customer customer = await db.customer.FindAsync(id);

            //customer = (customer)updateEntity(customer);
            customer.isActive = func;
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            //var customer = db.customer;
            //return View(await customer.ToListAsync());
        }


        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = await db.customer.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            customer customer = await db.customer.FindAsync(id);
			customer = (customer)deleteEntity( customer);
            
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
