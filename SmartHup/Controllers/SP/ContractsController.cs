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

    public class ContractsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: Contracts
        public async Task<ActionResult> Index()
        {
            var contracts = db.Contracts.Include(c => c.ContractTypes).Include(c => c.EntityStatus).Include(c => c.ServiceProvider1).Include(c => c.User).OrderByDescending(t => t.systemId);
            return View(await contracts.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contracts contracts = await db.Contracts.FindAsync(id);
            if (contracts == null)
            {
                return HttpNotFound();
            }
            return View(contracts);
        }
        public async Task<ActionResult> actions(long? id ,string status)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contracts contracts = await db.Contracts.FindAsync(id);

            if (contracts == null)
            {
                return HttpNotFound();
            }
            string st= "";
            switch (status)
            {
               
                case "Renew": { st = "Renewed"; break; }
                case "Cancel": { st = "Cancelled"; break; }
                //case "renew": { st = 1002; break; }
                //case "Cancel": { st = 7; break; }



            }

            //int status = contracts.ContractStatus;
            contracts.ContractStatus =GetStatusByName(st);
            contracts = (Contracts)updateEntity(contracts);
            db.Entry(contracts).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Requests(long? id, string status)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contracts contracts = await db.Contracts.FindAsync(id);

            if (contracts == null)
            {
                return HttpNotFound();
            }
            string st = "";
            switch (status)
            {

                case "renewr": { st = "Renewal request"; break; }
                case "Cancelr": { st = "Cancellation request"; break; }
                    //case "renew": { st = 1002; break; }
                    //case "Cancel": { st = 7; break; }



            }

            //int status = contracts.ContractStatus;
            contracts.ContractStatus = GetStatusByName(st);
            contracts = (Contracts)updateEntity(contracts);
            db.Entry(contracts).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Contracts/Create
        public ActionResult Create()
        {
            ViewBag.ContractType = new SelectList(db.ContractTypes, "systemId", "Type");
            ViewBag.ContractStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.ServiceProvider = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId");
            ViewBag.Account = new SelectList(db.User.Where(u=>u.serviceProviderId==user_info.user.serviceProviderId), "systemId", "Email");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,ContractType,ServiceProvider,StartDate,EndDate,Terms,Fees,Account")] Contracts contracts)
        {





            if (contracts.StartDate >= contracts.EndDate)
            {
                ModelState.AddModelError("EndDate", "end date cannot be before the start date");
            }
            if (ModelState.IsValid)
            {
              
                contracts.createdBy = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
               contracts.creationDate = DateTime.Now;
                contracts.version = 0;
                contracts.ContractStatus = db.EntityStatus.Where(e => e.EntityStatusName == "New").FirstOrDefault().systemId;
                //contracts = (Contracts)createEntity( contracts);
                db.Contracts.Add(contracts);
                await db.SaveChangesAsync();

                log(contracts);
                return RedirectToAction("Index");
            }

            ViewBag.ContractType = new SelectList(db.ContractTypes, "systemId", "Type", contracts.ContractType);
            ViewBag.ContractStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", contracts.ContractStatus);
            ViewBag.ServiceProvider = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", contracts.ServiceProvider);
            ViewBag.Account = new SelectList(db.User.Where(u => u.serviceProviderId == user_info.user.serviceProviderId), "systemId", "Email");
            return View(contracts);
        }

        // GET: Contracts/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contracts contracts = await db.Contracts.FindAsync(id);
            if (contracts == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractType = new SelectList(db.ContractTypes, "systemId", "Type", contracts.ContractType);
            ViewBag.ContractStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", contracts.ContractStatus);
            ViewBag.ServiceProvider = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", contracts.ServiceProvider);
            ViewBag.Account = new SelectList(db.User, "systemId", "Email", contracts.Account);
            return View(contracts);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,ContractType,ServiceProvider,StartDate,EndDate,Terms,Fees,SlaEnabled,Account,ContractStatus,creationDate,modificationDate,deletedDate,version,createdBy,modifiedBy,deletedBy")] Contracts contracts)
        {
            if (ModelState.IsValid)
            {
				contracts = (Contracts)updateEntity(contracts);
                db.Entry(db.Contracts.Find(contracts.systemId)).CurrentValues.SetValues(contracts);

                //db.Entry(contracts).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ContractType = new SelectList(db.ContractTypes, "systemId", "Type", contracts.ContractType);
            ViewBag.ContractStatus = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", contracts.ContractStatus);
            ViewBag.ServiceProvider = new SelectList(db.ServiceProvider, "systemId", "serviceProviderId", contracts.ServiceProvider);
            ViewBag.Account = new SelectList(db.User, "systemId", "Email", contracts.Account);
            return View(contracts);
        }

        // GET: Contracts/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contracts contracts = await db.Contracts.FindAsync(id);
            if (contracts == null)
            {
                return HttpNotFound();
            }
            return View(contracts);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Contracts contracts = await db.Contracts.FindAsync(id);
            //contracts = (Contracts)deleteEntity( contracts);
            contracts.deletedBy = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
            contracts.deletedDate = DateTime.Now;
            contracts.version = contracts.version + 1;
            contracts.ContractStatus = Utils.STATUS_DELETED;
            db.Entry(contracts).State = EntityState.Modified;
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
