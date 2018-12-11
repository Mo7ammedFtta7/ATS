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

namespace SmartHup.Controllers.SP
{
    public class MultiValueListsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: MultiValueLists
        public async Task<ActionResult> Index()
        {
            var multiValueList = db.MultiValueList.Include(m => m.PaymentField).Include(m => m.ServiceField);
            return View(await multiValueList.ToListAsync());
        }

        // GET: MultiValueLists/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultiValueList multiValueList = await db.MultiValueList.FindAsync(id);
            if (multiValueList == null)
            {
                return HttpNotFound();
            }
            return View(multiValueList);
        }

        // GET: MultiValueLists/Create
        public ActionResult Create()
        {
            ViewBag.paymentFieldId = new SelectList(db.PaymentField, "systemId", "paymentFieldId");
            ViewBag.serviceFieldId = new SelectList(db.ServiceField, "systemId", "serviceFieldId");
            return View();
        }

        // POST: MultiValueLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,status,creationDate,modificationDate,modifiedBy,createdBy,version,multivalueId,multivalueName,multivalueEName,paymentFieldId,serviceFieldId")] MultiValueList multiValueList)
        {
            if (ModelState.IsValid)
            {
				  multiValueList = (MultiValueList)createEntity( multiValueList);
                db.MultiValueList.Add(multiValueList);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.paymentFieldId = new SelectList(db.PaymentField, "systemId", "paymentFieldId", multiValueList.paymentFieldId);
            ViewBag.serviceFieldId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", multiValueList.serviceFieldId);
            return View(multiValueList);
        }

        // GET: MultiValueLists/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultiValueList multiValueList = await db.MultiValueList.FindAsync(id);
            if (multiValueList == null)
            {
                return HttpNotFound();
            }
            ViewBag.paymentFieldId = new SelectList(db.PaymentField, "systemId", "paymentFieldId", multiValueList.paymentFieldId);
            ViewBag.serviceFieldId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", multiValueList.serviceFieldId);
            return View(multiValueList);
        }

        // POST: MultiValueLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,status,creationDate,modificationDate,modifiedBy,createdBy,version,multivalueId,multivalueName,multivalueEName,paymentFieldId,serviceFieldId")] MultiValueList multiValueList)
        {
            if (ModelState.IsValid)
            {
				multiValueList = (MultiValueList)updateEntity( multiValueList);
                db.Entry(multiValueList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.paymentFieldId = new SelectList(db.PaymentField, "systemId", "paymentFieldId", multiValueList.paymentFieldId);
            ViewBag.serviceFieldId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", multiValueList.serviceFieldId);
            return View(multiValueList);
        }

        // GET: MultiValueLists/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultiValueList multiValueList = await db.MultiValueList.FindAsync(id);
            if (multiValueList == null)
            {
                return HttpNotFound();
            }
            return View(multiValueList);
        }

        // POST: MultiValueLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            MultiValueList multiValueList = await db.MultiValueList.FindAsync(id);
			multiValueList = (MultiValueList)deleteEntity( multiValueList);
            
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
