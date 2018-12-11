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
    [Authorized]
    public class ServiceFieldsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: ServiceFields
        public async Task<ActionResult> Index()
        {
            var serviceField = db.ServiceField.Include(s => s.EntityStatus).Include(s => s.EntityStatus1).Include(s => s.RangType).Include(s => s.Service).Include(s => s.ServiceHandler).Include(s => s.ServiceField2).OrderByDescending(t => t.systemId);
            return View(await serviceField.ToListAsync());
        }

        // GET: ServiceFields/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceField serviceField = await db.ServiceField.FindAsync(id);
            if (serviceField == null)
            {
                return HttpNotFound();
            }
            return View(serviceField);
        }

     
        // GET: ServiceFields/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceField serviceField = await db.ServiceField.FindAsync(id);
            if (serviceField == null)
            {
                return HttpNotFound();
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.entityStatus_systemId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.status);
            ViewBag.regTypeId = new SelectList(db.RangType, "systemId", "systemId", serviceField.regTypeId);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceField.serviceId);
            ViewBag.serviceHanlerId = new SelectList(db.ServiceHandler, "systemId", "serviceHanlerId", serviceField.serviceHanlerId);
            ViewBag.parentId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", serviceField.parentId);
            return View(serviceField);
        }

        // POST: ServiceFields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,status,creationDate,modificationDate,createdBy,modifiedBy,version,serviceFieldId,serviceFieldName,serviceFieldStatus,channelFieldEName,channelFieldName,serviceHandlerFieldName,regularExpression,isRequired,dateFormat,isRequestField,isSavable,isResponseField,additionalFieldName,serviceId,serviceHanlerId,isPaymentInfo,paymentInfoOrder,Note,payeeId,idx,serviceProviderId,isParent,parentId,entityStatus_systemId,deletedBy,deletedDate,regTypeId,isFavorable,dbColumnName,isCheckable")] ServiceField serviceField)
        {
            if (ModelState.IsValid)
            {
				serviceField = (ServiceField)updateEntity( serviceField);
                db.Entry(serviceField).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.entityStatus_systemId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.status);
            ViewBag.regTypeId = new SelectList(db.RangType, "systemId", "systemId", serviceField.regTypeId);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceField.serviceId);
            ViewBag.serviceHanlerId = new SelectList(db.ServiceHandler, "systemId", "serviceHanlerId", serviceField.serviceHanlerId);
            ViewBag.parentId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", serviceField.parentId);
            return View(serviceField);
        }

        // GET: ServiceFields/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceField serviceField = await db.ServiceField.FindAsync(id);
            if (serviceField == null)
            {
                return HttpNotFound();
            }
            return View(serviceField);
        }

        // POST: ServiceFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ServiceField serviceField = await db.ServiceField.FindAsync(id);
			serviceField = (ServiceField)deleteEntity( serviceField);
            
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
