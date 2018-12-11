//using SmartHup.Models;

//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;

//namespace SmartHup.Controllers.SP
//{
//    [Authorized]

//    public class ServiceHandlersController : Base
//    {
//        private SMARTEntities db = new SMARTEntities();

//        // GET: ServiceHandlers
//        public ActionResult Index()
//        {
//            var entities = db.EntityStatus;
//            var deleted = db.EntityStatus.FirstOrDefault(e => e.EntityStatusName == "Deleted").systemId;
//            var serviceHandler = db.ServiceHandler.Include(s => s.Service);
//            ViewBag.status = new SelectList(entities, "systemId", "EntityStatusName");
//            Dictionary<int, string> dic = new Dictionary<int, string>();
//            foreach (var state in entities)
//                dic.Add(state.systemId, state.EntityStatusName);

//            ViewBag.statusDictionary = dic;

//            var userid = user_info.user.systemId;
//            var user = db.User.FirstOrDefault(u => u.systemId == userid);
//            var typeId = db.ServiceProviderType.FirstOrDefault(spt => spt.systemId.Equals(Utils.CSP)).systemId;
//            if (user.ServiceProvider.serviceProviderTypeId == typeId)
//            {
//                return View(db.ServiceHandler.Where(o => o.Service.ServiceProvider.serviceProviderTypeId == typeId
//                && o.Service.ServiceProvider.systemId == user.serviceProviderId).Include(e => e.EntityStatus).ToList());
//            }
//            else
//            {
//                return View(db.ServiceHandler.Include(e => e.EntityStatus).ToList());
//            }

//            //return View(serviceHandler.Include(e => e.entityStatus).ToList());
//        }

//        // GET: ServiceHandlers/Details/5
//        public ActionResult Details(long? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ServiceHandler serviceHandler = db.ServiceHandler.Find(id);
//            if (serviceHandler == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceHandler.serviceId);
//            ViewBag.serviceHandlerId = new SelectList(db.ServiceHandler, "serviceHandlerId", "serviceHandlerName");
//            ViewBag.Status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceHandler.status);
//            displayEntityInfo(serviceHandler.createdBy, serviceHandler.modifiedBy, serviceHandler.deletedBy, serviceHandler.status);
//            return View(serviceHandler);
//        }

//        // GET: ServiceHandlers/Create
//        public ActionResult Create()
//        {
//            var user = getUser();
//            var typeId = GetUserType(user);
//            if (user.ServiceProvider.serviceProviderTypeId == typeId)
//            {
//                ViewBag.serviceId = new SelectList(db.Service.Where(o => o.ServiceProvider.serviceProviderTypeId == typeId
//                && o.ServiceProvider.systemId == user.serviceProviderId && o.isParent != true), "systemId", "serviceId");
//            }
//            else
//                ViewBag.serviceId = new SelectList(db.Service.Where(o => o.isParent != true), "systemId", "serviceId");
//            ViewBag.serviceHandlerId = new SelectList(db.ServiceHandler, "serviceHandlerId", "serviceHandlerName");
//            ViewBag.Status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
//            ViewBag.page = "C";
//            return View();
//        }

//        // POST: ServiceHandlers/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(ServiceHandler serviceHandler, List<ServiceField> ServiceField)
//        {
//            if (ModelState.IsValid)
//            {
//                serviceHandler = (ServiceHandler)createEntity(serviceHandler);
//                List<ServiceField> outhFields = new List<ServiceField>();
//                foreach (var Sfield in serviceHandler.ServiceField)
//                {
//                    Sfield.serviceId = serviceHandler.serviceId;
//                    var newSField = (ServiceField)createEntity(Sfield);
//                    outhFields.Add(newSField);
//                }
//                serviceHandler.ServiceField = outhFields;
//                db.ServiceHandler.Add(serviceHandler);
//                db.SaveChanges();
//                log(serviceHandler);
//                return RedirectToAction("Index");
//            }
//            ViewBag.Status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
//            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceHandler.serviceId);
//            ViewBag.page = "C";
//            return View(serviceHandler);
//        }

//        // GET: ServiceHandlers/Edit/5
//        public ActionResult Edit(long? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ServiceHandler serviceHandler = db.ServiceHandler.Find(id);
//            if (serviceHandler == null)
//            {
//                return HttpNotFound();
//            }
//            var deleted = db.EntityStatus.FirstOrDefault(e => e.EntityStatusName == "Deleted").systemId;
//            List<ServiceField> Sfields = db.ServiceField.Where(s => s.serviceHanlerId == id && s.status != deleted).ToList();
//            var user = getUser();
//            var typeId = GetUserType(user);
//            if (user.ServiceProvider.serviceProviderTypeId == typeId)
//            {
//                ViewBag.serviceId = new SelectList(db.Service.Where(o => o.ServiceProvider.serviceProviderTypeId == typeId
//                && o.ServiceProvider.systemId == user.serviceProviderId && o.isParent != true), "systemId", "serviceId", serviceHandler.serviceId);
//            }
//            else
//                ViewBag.serviceId = new SelectList(db.Service.Where(o => o.isParent != true), "systemId", "serviceId", serviceHandler.serviceId);
//            ViewBag.Status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceHandler.status);
//            ViewBag.serviceHandlerId = new SelectList(db.ServiceHandler, "serviceHandlerId", "serviceHandlerName", serviceHandler.serviceHanlerId);
//            serviceHandler.ServiceField = Sfields;
//            ViewBag.page = "E";
//            return View(serviceHandler);
//        }

//        // POST: ServiceHandlers/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "systemId,serviceHanlerId,serviceHanlerName,serviceHanlerStatus,serviceId,serviceHostUri,serviceHostIntegrationType,serviceHostCharSet,isSslExist,serviceHostPublicKey,serviceHostEncryptionMethod,serviceHostAcceptEncoding,serviceHostContentType,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,status,version,ServiceField,deletedID")] ServiceHandler serviceHandler, string deletedID)
//        {
//            var deleted = db.EntityStatus.FirstOrDefault(e => e.EntityStatusName == "Deleted").systemId;
//            var errors = ModelState.Values.SelectMany(v => v.Errors);
//            if (ModelState.IsValid)
//            {

//                serviceHandler = (ServiceHandler)updateEntity(serviceHandler);

//                var did = deletedID.Split(',');
//                foreach (var sid in did)
//                {
//                    if (string.IsNullOrEmpty(sid) || sid.Equals("undefined"))
//                        continue;
//                    var id = Convert.ToInt64(sid);
//                    var temp = db.ServiceField.Find(id);
//                    if (temp != null)
//                    {
//                        db.ServiceField.Remove(temp);
//                        db.SaveChanges();
//                    }
//                }
//                if (serviceHandler.ServiceField != null)
//                {
//                    foreach (var sf in serviceHandler.ServiceField)
//                    {
//                        sf.serviceHanlerId = serviceHandler.systemId;
//                        sf.serviceId = serviceHandler.serviceId;
//                        var tempSf = db.ServiceField.FirstOrDefault(df => df.systemId == sf.systemId);
//                        if (tempSf == null)
//                            db.ServiceField.Add((ServiceField)createEntity(sf));
//                        else
//                            db.Entry(tempSf).CurrentValues.SetValues(((ServiceField)createEntity(sf)));
//                        db.SaveChanges();
//                    }
//                }
//                serviceHandler.ServiceField = new List<ServiceField>();
//                //serviceHandler.ServiceField = sfList;
//                db.Entry(serviceHandler).State = EntityState.Modified;

//                db.SaveChanges();
//                log(serviceHandler);
//                return RedirectToAction("Index");
//            }
//            ViewBag.serviceHandlerId = new SelectList(db.ServiceHandler, "serviceHandlerId", "serviceHandlerName");
//            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceHandler.serviceId);
//            ViewBag.Status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceHandler.status);
//            ViewBag.page = "E";
//            return View(serviceHandler);
//        }

//        // GET: ServiceHandlers/Delete/5
//        public ActionResult Delete(long? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ServiceHandler serviceHandler = db.ServiceHandler.Find(id);
//            if (serviceHandler == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", serviceHandler.serviceId);
//            ViewBag.serviceHandlerId = new SelectList(db.ServiceHandler, "serviceHandlerId", "serviceHandlerName");
//            ViewBag.Status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", serviceHandler.status);
//            return View(serviceHandler);
//        }

//        // POST: ServiceHandlers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(long id)
//        {
//            ServiceHandler serviceHandler = db.ServiceHandler.Find(id);
//            serviceHandler = (ServiceHandler)deleteEntity(serviceHandler);
//            List<ServiceField> outhFields = new List<ServiceField>();
//            foreach (var Sfield in serviceHandler.ServiceField)
//            {
//                var newSField = (ServiceField)deleteEntity(Sfield);
//                outhFields.Add(newSField);
//            }
//            serviceHandler.ServiceField = outhFields;
//            db.SaveChanges();
//            log(serviceHandler);
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }

//}