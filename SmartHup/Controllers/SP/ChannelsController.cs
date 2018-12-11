using SmartHup.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmartHup.Controllers.SP
{
    [Authorized]

    public class ChannelsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: Channels
        public ActionResult Index()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var entities = db.EntityStatus;
            foreach (var state in entities)
                dic.Add(state.systemId, state.EntityStatusName);

            ViewBag.statusDictionary = dic;
            return View(db.Channel.OrderByDescending(t => t.systemId).ToList());
        }

        // GET: Channels/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Channel channel = db.Channel.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            displayEntityInfo(channel.createdBy, channel.modifiedBy, channel.deletedBy, channel.status);
            var SStauts = Convert.ToInt32(channel.channelStatus);
            TempData["display_Sstatus"] = db.EntityStatus.FirstOrDefault(u => u.systemId == SStauts).EntityStatusName;
            return View(channel);
        }

        // GET: Channels/Create
        public ActionResult Create()
        {
            ViewBag.channelStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            return View();
        }

        // POST: Channels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "systemId,status,creationDate,modificationDate,createdBy,modifiedBy,version,channelId,channelName,channelStatus,publicKey")] Channel channel)
        {
            if (ModelState.IsValid)
            {
                channel = (Channel)createEntity(channel);
                db.Channel.Add(channel);
                db.SaveChanges();
                log(channel);
                return RedirectToAction("Index");
            }
            ViewBag.channelStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            return View(channel);
        }

        // GET: Channels/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Channel channel = db.Channel.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", channel.status);
            ViewBag.channelStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", channel.channelStatus);
            return View(channel);
        }

        // POST: Channels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "systemId,status,creationDate,modificationDate,createdBy,modifiedBy,version,channelId,channelName,channelStatus,publicKey")] Channel channel)
        {
            if (ModelState.IsValid)
            {
                channel = (Channel)updateEntity(channel);
                db.Entry(channel).State = EntityState.Modified;
                db.SaveChanges();
                log(channel);
                return RedirectToAction("Index");
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", channel.status);
            ViewBag.channelStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", channel.channelStatus);
            return View(channel);
        }

        // GET: Channels/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Channel channel = db.Channel.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            displayEntityInfo(channel.createdBy, channel.modifiedBy, channel.deletedBy, channel.status);
            return View(channel);
        }

        // POST: Channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Channel channel = db.Channel.Find(id);
            channel = (Channel)deleteEntity(channel);

            db.SaveChanges();
            log(channel);
            return RedirectToAction("Index");
        }


        public ActionResult AssignServices(long id)
        {
            var data = db.Service.Where(s=>s.status==1)
                //.Include(i=>i.Service)
                .ToList();
            ViewBag.channelId = id;


            //ViewBag.authServices = db.Channel.Where(ra => ra.systemId == id) != null ? db.Channel.Where(ra => ra.systemId == id).FirstOrDefault(). Service.Select(d => d.systemId).ToList() : new List<long>();
            ViewBag.authServices = db.LT_Channel_Service.Where(ra => ra.channelId == id) != null ? db.LT_Channel_Service.Where(ra => ra.channelId == id).Select(d => d.serviceIdId).ToList() : new List<long>();

            ViewBag.serProv = db.ServiceProvider.Include(i=>i.Service).Where(s=>s.status==1).ToList();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignServices(List<CheckBoxViewModel> list, long ChannelId)
        {
            foreach (var Services in list)
            {
                var temp = db.LT_Channel_Service.FirstOrDefault(ra => ra.serviceIdId == Services.id && ra.channelId == ChannelId);

                if (Services.isChecked)
                {
                    if (temp == null)
                    {
                        db.LT_Channel_Service.Add(
                            new LT_Channel_Service
                            {
                                serviceIdId = Services.id,
                                channelId = ChannelId
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
            var data = db.PaymentProvider.Include(d => d.PaymentMethod).ToList();
            ViewBag.channelId = id;

            ViewBag.authPayMethod = db.LT_PaymentMethod_Channel.Where(ra => ra.channelId == id).Select(d => d.paymentMethodId).ToList();
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignPaymentMethod(List<CheckBoxViewModel> list, long ChannelId)
        {
            foreach (var PayMeth in list)
            {
                var temp = db.LT_PaymentMethod_Channel.FirstOrDefault(ra => ra.paymentMethodId == PayMeth.id && ra.channelId == ChannelId);

                if (PayMeth.isChecked)
                {
                    if (temp == null)
                    {
                        db.LT_PaymentMethod_Channel.Add(
                            new LT_PaymentMethod_Channel
                            {
                                channelId = ChannelId,
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