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
    public class ServiceHandlersController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: ServiceHandlers
        public async Task<ActionResult> Index()
        {

            var userid = user_info.user.systemId;
            var user = db.User.FirstOrDefault(u => u.systemId == userid);
            var typeId = db.ServiceProviderType.FirstOrDefault(spt => spt.systemId.Equals(Utils.CSP)).systemId;
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                return View(await db.ServiceHandler.Where(o => o.Service.ServiceProvider.serviceProviderTypeId == typeId
                && o.Service.ServiceProvider.systemId == user.serviceProviderId).Include(e => e.EntityStatus).OrderByDescending(t => t.systemId).ToListAsync());
            }
            else
            {
                return View(await db.ServiceHandler.Include(e => e.EntityStatus).OrderByDescending(t => t.systemId).ToListAsync());
            }


            //var serviceHandler = db.ServiceHandler.Include(s => s.EntityStatus).Include(s => s.Service);
            //return View(await serviceHandler.ToListAsync());
        }

        // GET: ServiceHandlers/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceHandler serviceHandler = await db.ServiceHandler.FindAsync(id);
            if (serviceHandler == null)
            {
                return HttpNotFound();
            }
            return View(serviceHandler);
        }
        // GET: ServiceHandlers/Details/5
        public async Task<ActionResult> ServiceFields(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceHandler serviceHandler = await db.ServiceHandler.FindAsync(id);
            //var sh = db.ServiceHandler.Find(id);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.regTypeId = new SelectList(db.RangType, "systemId", "systemId");
            //ViewBag.serviceId = sh.Service;
            ViewBag.Provider = user_info.user.serviceProviderId;
            ViewBag.names = typeof(Transaction).GetProperties().Select(property => property.Name).ToArray();

            //ViewBag.serviceHanler = new SelectList(db.ServiceHandler, "systemId", "serviceHanlerId", id);
            ViewBag.shid = id;

            ViewBag.parentId = new SelectList(db.ServiceField.Where(d => d.serviceHanlerId == serviceHandler.systemId), "systemId", "serviceFieldId");
            if (serviceHandler == null)
            {
                return HttpNotFound();
            }
            return View(serviceHandler);
        }



        public async Task<ActionResult> DataOptions(long id)
        {
            var multiValueList = db.MultiValueList.Where(m=>m.serviceFieldId==id);
            ViewBag.paymentFieldId = new SelectList(db.PaymentField, "systemId", "paymentFieldName");
            var sf = db.ServiceField.Find(id);
            ViewBag.sfn = sf.serviceFieldName ;
            ViewBag.sfid = sf.ServiceHandler.systemId;

            ViewBag.systemId = id;
            return View(await multiValueList.ToListAsync());
        }





        // GET: ServiceHandlers/Create
        public ActionResult Create()
        {
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId");

           return View();
         //   return Json(names,JsonRequestBehavior.AllowGet);

        }
        public ActionResult addnew()
        {
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId");
            return PartialView("addnew");
        }

        // POST: ServiceHandlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,status,creationDate,modificationDate,createdBy,modifiedBy,version,serviceHanlerId,serviceHanlerName,serviceHanlerStatus,serviceId,serviceHostUri,serviceHostIntegrationType,serviceHostCharSet,isSslExist,serviceHostPublicKey,serviceHostEncryptionMethod,serviceHostAcceptEncoding,serviceHostContentType,deletedDate,deletedBy,entityStatus_systemId")] ServiceHandler serviceHandler)
        {
            if (ModelState.IsValid)
            {
                serviceHandler = (ServiceHandler)createEntity(serviceHandler);
                db.ServiceHandler.Add(serviceHandler);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new {id= serviceHandler.systemId } );
            }

            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceHandler.status);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceHandler.serviceId);
            return View(serviceHandler);
        }



        public async Task<ActionResult> editoption(long? id)
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
            return PartialView("editoption", multiValueList);
        }
        //sssssssssssssssssssssssss
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> editoption([Bind(Include = "systemId,status,creationDate,modificationDate,modifiedBy,createdBy,version,multivalueId,multivalueName,multivalueEName,paymentFieldId,serviceFieldId")] MultiValueList multiValueList)
        {
            if (ModelState.IsValid)
            {
                multiValueList = (MultiValueList)updateEntity(multiValueList);
                db.Entry(multiValueList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Redirect("/ServiceHandlers/DataOptions/"+ multiValueList.serviceFieldId );
            }
            ViewBag.paymentFieldId = new SelectList(db.PaymentField, "systemId", "paymentFieldId", multiValueList.paymentFieldId);
            ViewBag.serviceFieldId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", multiValueList.serviceFieldId);
            return View(multiValueList);
        }
        //555555555555555555555
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> deleteoption(long id)
        {

            MultiValueList multiValueList = await db.MultiValueList.FindAsync(id);
            var xx = multiValueList.serviceFieldId;
            //  multiValueList = (MultiValueList)deleteEntity(multiValueList);
            db.MultiValueList.Remove(multiValueList);

            await db.SaveChangesAsync();
            return Redirect("/ServiceHandlers/DataOptions/" +xx);
        }
        //dddddddddddddddddddddd
        [HttpPost]
        public ActionResult addoption([Bind(Include = "multivalueId,multivalueName,multivalueEName,paymentFieldId,serviceFieldId")] MultiValueList multiValueList)
        {
            if (ModelState.IsValid)
            {
            //multiValueList = (MultiValueList)createEntity(multiValueList);
            multiValueList.status = 1;

                multiValueList.createdBy = user_info.user.systemId;
            multiValueList.creationDate = DateTime.Now;
            multiValueList.version = 0;
                db.MultiValueList.Add(multiValueList);
             db.SaveChanges();
            return Redirect("/ServiceHandlers/DataOptions/"+multiValueList.serviceFieldId);
            //return Json( multiValueList,JsonRequestBehavior.AllowGet);
            }
            return Redirect("/ServiceHandlers/DataOptions/" + multiValueList.serviceFieldId);

            //ViewBag.paymentFieldId = new SelectList(db.PaymentField, "systemId", "paymentFieldId", multiValueList.paymentFieldId);
            //ViewBag.serviceFieldId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", multiValueList.serviceFieldId);
            //return View(multiValueList);
            //return RedirectToAction("Index");

        }


        // GET: ServiceFields/Edit/5
        public async Task<ActionResult> EditField(long? id)
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
            ViewBag.names = typeof(Transaction).GetProperties().Select(property => property.Name).ToArray();

            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.entityStatus_systemId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.status);
            ViewBag.regTypeId = new SelectList(db.RangType, "systemId", "systemId", serviceField.regTypeId);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceField.serviceId);
            ViewBag.serviceHanlerId = new SelectList(db.ServiceHandler, "systemId", "serviceHanlerId", serviceField.serviceHanlerId);
            ViewBag.parentId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", serviceField.parentId);
            return PartialView("EditField", serviceField);
        }

        // POST: ServiceFields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditField([Bind(Include = "systemId,status,creationDate,modificationDate,createdBy,modifiedBy,version,serviceFieldId,serviceFieldName,serviceFieldStatus,channelFieldEName,channelFieldName,serviceHandlerFieldName,regularExpression,isRequired,dateFormat,isRequestField,isSavable,isResponseField,additionalFieldName,serviceId,serviceHanlerId,isPaymentInfo,paymentInfoOrder,Note,payeeId,idx,serviceProviderId,isParent,parentId,entityStatus_systemId,deletedBy,deletedDate,regTypeId,isFavorable,dbColumnName,isCheckable")] ServiceField serviceField)
        {
            if (ModelState.IsValid)
            {
                serviceField = (ServiceField)updateEntity(serviceField);
                var iid = serviceField.serviceHanlerId;
                db.Entry(serviceField).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("ServiceFields", "ServiceHandlers", new { id = iid });

            }
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.entityStatus_systemId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.status);
            ViewBag.regTypeId = new SelectList(db.RangType, "systemId", "systemId", serviceField.regTypeId);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceField.serviceId);
            ViewBag.serviceHanlerId = new SelectList(db.ServiceHandler, "systemId", "serviceHanlerId", serviceField.serviceHanlerId);
            ViewBag.parentId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", serviceField.parentId);
            return PartialView("_EditFields",serviceField);
        }

      
        public async Task<ActionResult> DeleteField(int id)
        {
               var  serviceField = db.ServiceField.Find(id);
            var iid = serviceField.serviceHanlerId;

            db.ServiceField.Remove(serviceField);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("ServiceFields", "ServiceHandlers", new { id = iid });

        }


        //fields
        // GET: ServiceFields/Create
        public ActionResult AddField(int id)
        {

            var sh = db.ServiceHandler.Find(id);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.regTypeId = new SelectList(db.RangType, "systemId", "systemId");
            ViewBag.serviceId = sh.Service;
            ViewBag.sid = sh.serviceId;

            ViewBag.Provider = user_info.user.serviceProviderId ;

            ViewBag.serviceHanler = new SelectList(db.ServiceHandler, "systemId", "serviceHanlerId",id);
            ViewBag.shid = id;

            ViewBag.parentId = new SelectList(db.ServiceField.Where(d=>d.serviceHanlerId==id) , "systemId", "serviceFieldId");
            return View();
        }

        // POST: ServiceFields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddField([Bind(Include = "systemId,status,creationDate,modificationDate,createdBy,modifiedBy,version,serviceFieldId,serviceFieldName,serviceFieldStatus,channelFieldEName,channelFieldName,serviceHandlerFieldName,regularExpression,isRequired,dateFormat,isRequestField,isSavable,isResponseField,additionalFieldName,serviceId,serviceHanlerId,isPaymentInfo,paymentInfoOrder,Note,payeeId,idx,serviceProviderId,isParent,parentId,entityStatus_systemId,deletedBy,deletedDate,regTypeId,isFavorable,dbColumnName,isCheckable")] ServiceField serviceField)
        {
            try
            {
                serviceField = (ServiceField)createEntity(serviceField);
                db.ServiceField.Add(serviceField);
                await db.SaveChangesAsync();
                return RedirectToAction("ServiceFields", "ServiceHandlers", new { id = serviceField.serviceHanlerId });

            }
            catch (Exception)
            {

                ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.entityStatus_systemId);
                ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceField.status);
                ViewBag.regTypeId = new SelectList(db.RangType, "systemId", "systemId", serviceField.regTypeId);
                ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceField.serviceId);
                ViewBag.serviceHanlerId = new SelectList(db.ServiceHandler, "systemId", "serviceHanlerId", serviceField.serviceHanlerId);
                ViewBag.parentId = new SelectList(db.ServiceField, "systemId", "serviceFieldId", serviceField.parentId);
                ViewBag.serviceHanler = new SelectList(db.ServiceHandler, "systemId", "serviceHanlerId", serviceField.serviceHanlerId);

                return View(serviceField); throw;
            }
   

          
        }


        // GET: ServiceHandlers/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceHandler serviceHandler = await db.ServiceHandler.FindAsync(id);
            if (serviceHandler == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceHandler.status);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceHandler.serviceId);
            return View(serviceHandler);
        }

        // POST: ServiceHandlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,status,serviceHanlerId,serviceHanlerName,serviceHanlerStatus,serviceId,serviceHostUri,serviceHostIntegrationType,serviceHostCharSet,isSslExist,serviceHostPublicKey,serviceHostEncryptionMethod,serviceHostAcceptEncoding,serviceHostContentType,entityStatus_systemId")] ServiceHandler serviceHandler)
        {
            if (ModelState.IsValid)
            {
                //serviceHandler = (ServiceHandler)updateEntity( serviceHandler);
                var sh = db.ServiceHandler.Find(serviceHandler.systemId);
                serviceHandler.modifiedBy = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
                serviceHandler.creationDate = sh.creationDate;
                serviceHandler.modificationDate = DateTime.Now;
                serviceHandler.version = serviceHandler.version + 1;
                db.Entry(sh).CurrentValues.SetValues(serviceHandler);

             //   db.Entry(serviceHandler).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceHandler.status);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceHandler.serviceId);
            return View(serviceHandler);
        }

        // GET: ServiceHandlers/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceHandler serviceHandler = await db.ServiceHandler.FindAsync(id);
            if (serviceHandler == null)
            {
                return HttpNotFound();
            }
            return View(serviceHandler);
        }

        // POST: ServiceHandlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            //         ServiceHandler serviceHandler = await db.ServiceHandler.FindAsync(id);
            //serviceHandler = (ServiceHandler)deleteEntity( serviceHandler);

            //         await db.SaveChangesAsync();
            //         return RedirectToAction("Index");
            ServiceHandler serviceHandler = await db.ServiceHandler.FindAsync(id);
            serviceHandler = (ServiceHandler)deleteEntity(serviceHandler);
            //List<ServiceField> outhFields = new List<ServiceField>();
            //foreach (var Sfield in serviceHandler.ServiceField)
            //{
            //    var newSField = (ServiceField)deleteEntity(Sfield);
            //    outhFields.Add(newSField);
            //}
            //serviceHandler.ServiceField = outhFields;
            await db.SaveChangesAsync();
            log(serviceHandler);
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
