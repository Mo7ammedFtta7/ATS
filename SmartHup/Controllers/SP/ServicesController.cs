using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartHup.Models.New;
using SmartHup.Models;

namespace SmartHup.Controllers
{
    [Authorized]

    public class ServicesController : Base
    {

        private SMARTEntities db = new SMARTEntities();

        // GET: Services
        public ActionResult Index()
        {
            

            var entities = db.EntityStatus;
            ViewBag.status = new SelectList(entities, "systemId", "EntityStatusName");
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (var state in entities)
                dic.Add(state.systemId, state.EntityStatusName);
            ViewBag.statusDictionary = dic;

            var userid = user_info.user.systemId;
            var user = db.User.FirstOrDefault(u => u.systemId == userid);
            var typeId = db.ServiceProviderType.FirstOrDefault(spt => spt.systemId.Equals(Utils.CSP)).systemId;
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                return View(db.Service.Where(o => o.ServiceProvider.serviceProviderTypeId == typeId
                && o.ServiceProvider.systemId == user.serviceProviderId).Include(s => s.Service1)
                .Include(s => s.ServiceProvider).Include(s => s.ServiceScenario).Include(s => s.EntityStatus).OrderByDescending(t => t.systemId).ToList());
            }
            else
            {
                return View(db.Service.Include(s => s.Service2).Include(s => s.Service3).Include(s => s.ServiceProvider).Include(s => s.ServiceScenario).Include(s => s.EntityStatus).OrderByDescending(t => t.systemId).ToList());
            }
            //var service = db.Service.Include(s => s.mainServiceForPreServices).Include(s => s.parentServices).Include(s => s.ServiceProvider).Include(s => s.ServiceScenario);
            //return View(service.Include(r=>r.entityStatus).ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Service.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            displayEntityInfo(service.createdBy, service.modifiedBy, service.deletedBy, service.status);
            var SStauts = Convert.ToInt32(service.serviceStatus);
            TempData["display_Sstatus"] = db.EntityStatus.FirstOrDefault(u => u.systemId == SStauts).EntityStatusName;
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.precheckServiceId = new SelectList(db.Service.Where(PCs=>PCs.isParent == null || PCs.isParent ==false), "systemId", "serviceId");
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                ViewBag.parentId = new SelectList(db.Service.Where(o => o.ServiceProvider.serviceProviderTypeId == typeId  && o.ServiceProvider.systemId == user.serviceProviderId && o.isParent==true ).Include(s => s.Service1).ToList(), "systemId", "serviceName");

                ViewBag.serviceProviderId = new SelectList(db.ServiceProvider.Where(SP=>SP.systemId == user.serviceProviderId), "systemId", "serviceProviderId");
            }
            else
            {
                ViewBag.parentId = new SelectList(db.Service.Where(r => r.parentId == null && r.isParent == false).ToList(), "systemId", "serviceEName");

                ViewBag.serviceProviderId = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId");
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.serviceScenarioId = new SelectList(db.ServiceScenario, "systemId", "serviceScenarioId");
            ViewBag.isadmin = user_info.user.ServiceProvider.serviceProviderTypeId == 1 ? "" : "hide";
            ViewBag.spid = user_info.user.serviceProviderId;
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "serviceId,serviceName,serviceEName,ServiceLable,ServiceELable,serviceStatus,serviceDescription,isParent,serviceConfigurationId,serviceProviderId,precheckRquired,serviceScenarioId,precheckServiceId,parentId,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,status,version,serviceDescriptionAr")] Service service)
        {
            if (ModelState.IsValid)
            {
				  service = (Service)createEntity( service);
                service.status = 5;
                //service.serviceStatus = 5;
                if (user_info.user.ServiceProvider.serviceProviderTypeId != 1)
                {
                    service.serviceProviderId = user_info.user.serviceProviderId;

                }
                db.Service.Add(service);
                db.SaveChanges();
				log(service);

                return RedirectToAction("Index");
            }

            ViewBag.precheckServiceId = new SelectList(db.Service, "systemId", "serviceId", service.precheckServiceId);
            ViewBag.parentId = new SelectList(db.Service.Where(a=>a.serviceProviderId==user_info.user.serviceProviderId), "systemId", "serviceId", service.parentId);
            ViewBag.serviceProviderId = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", service.serviceProviderId);
            ViewBag.serviceScenarioId = new SelectList(db.ServiceScenario, "systemId", "serviceScenarioId", service.serviceScenarioId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName",service.status);
            ViewBag.isadmin = user_info.user.ServiceProvider.serviceProviderTypeId == 1 ? "" : "hide";

            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error","Error", new { httpCode = Utils.HTTP_BADREQUEST });
            }
            Service service = db.Service.Find(id);
            if (service == null)
            {
                return RedirectToAction("Error", "Error", new { httpCode = Utils.HTTP_NOTFOUND });
            }
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                ViewBag.serviceProviderId = new SelectList(db.ServiceProvider.Where(SP => SP.systemId == user.serviceProviderId), "systemId", "serviceProviderId", service.serviceProviderId);
            }
            else
            {
                ViewBag.serviceProviderId = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", service.serviceProviderId);

            }
            ViewBag.precheckServiceId = new SelectList(db.Service.Where(PCs => PCs.isParent == null || PCs.isParent == false), "systemId", "serviceId", service.precheckServiceId);
            ViewBag.parentId = new SelectList(db.Service.Where(r => r.parentId == null && r.isParent == true).ToList(), "systemId", "serviceId", service.parentId);
            ViewBag.serviceScenarioId = new SelectList(db.ServiceScenario, "systemId", "serviceScenarioId", service.serviceScenarioId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", service.status);
            ViewBag.serviceStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", service.serviceStatus);

            ViewBag.isadmin = user_info.user.ServiceProvider.serviceProviderTypeId == 1 ? "" : "hide";

            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "systemId,serviceId,serviceName,serviceEName,ServiceLable,ServiceELable,serviceStatus,serviceDescription,isParent,serviceConfigurationId,serviceProviderId,precheckRquired,serviceScenarioId,precheckServiceId,parentId,createdBy,modifiedBy,deletedBy,creationDate,modificationDate,deletedDate,status,version,serviceDescriptionAr,paymentListName")] Service service)
        {
            if (ModelState.IsValid)
            {
				service = (Service)updateEntity( service);
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
				log(service);
                return RedirectToAction("Index");
            }
            ViewBag.precheckServiceId = new SelectList(db.Service, "systemId", "serviceId", service.precheckServiceId);
            ViewBag.parentId = new SelectList(db.Service.Where(a => a.serviceProviderId == user_info.user.serviceProviderId), "systemId", "serviceId", service.parentId);
            ViewBag.serviceProviderId = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", service.serviceProviderId);
            ViewBag.serviceScenarioId = new SelectList(db.ServiceScenario, "systemId", "serviceScenarioId", service.serviceScenarioId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", service.status);
            ViewBag.serviceStatus =  new SelectList(db.EntityStatus, "systemId", "EntityStatusName", service.serviceStatus);
            ViewBag.isadmin = user_info.user.ServiceProvider.serviceProviderTypeId == 1 ? "" : "hide";

            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Error", new { httpCode = Utils.HTTP_BADREQUEST });
            }
            Service service = db.Service.Find(id);
            if (service == null)
            {
                return RedirectToAction("Error", "Error", new { httpCode = Utils.HTTP_NOTFOUND });
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Service service = db.Service.Find(id);
			service = (Service)deleteEntity( service);
            db.SaveChanges();
			log(service);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignChannels(List<CheckBoxViewModel> list, long ServiceId)
        {
            foreach (var channel in list)
            {
                var temp = db.LT_Channel_Service.FirstOrDefault(ra => ra.channelId == channel.id && ra.serviceIdId == ServiceId);

                if (channel.isChecked)
                {
                    if (temp == null)
                    {
                        db.LT_Channel_Service.Add(
                            new LT_Channel_Service
                            {
                                serviceIdId =ServiceId,
                               channelId=channel.id
                             }
                            );
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (temp != null)
                    {
                        db.Entry(temp).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult AssignChannels(long id)
        {
            var data = db.Channel
                //.Include(i=>i.Service)
                .ToList();
            ViewBag.serviceId = id;
            ViewBag.authChannels = db.LT_Channel_Service.Where(ra => ra.serviceIdId == id)!=null? db.LT_Channel_Service.Where(ra => ra.serviceIdId == id).Select( d=>d.channelId  ).ToList():new List<long>();
            
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignPaymentMethod(List<CheckBoxViewModel> list, long ServiceId)
        {
            foreach (var PayMeth in list)
            {
                var temp = db.LT_PaymentMethod_Service.FirstOrDefault(ra => ra.paymentMethodId == PayMeth.id && ra.serviceId == ServiceId);

                if (PayMeth.isChecked)
                {
                    if (temp == null)
                    {
                        db.LT_PaymentMethod_Service.Add(
                            new LT_PaymentMethod_Service
                            { 
                                 serviceId = ServiceId,
                                  paymentMethodId = PayMeth.id
                            }
                            );
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (temp != null)
                    {
                        db.Entry(temp).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult AssignPaymentMethod(long id)
        {
            var data = db.PaymentProvider.Include(d=>d.PaymentMethod).ToList();
            ViewBag.serviceId = id;
          
            ViewBag.authPayMethod = db.LT_PaymentMethod_Service.Where(ra => ra.serviceId == id).Select(d => d.paymentMethodId).ToList();
            return View(data);
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
