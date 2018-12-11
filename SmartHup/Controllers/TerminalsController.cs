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

    public class TerminalsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: Terminals Termianl_Log
        public async Task<ActionResult> Index()
        {
            var terminal = db.Terminal.Include(t => t.Bank).Include(t => t.City).Include(t => t.State).Include(t => t.TerminalFamily).Include(t => t.Vendor);
            return View(await terminal.OrderByDescending(t => t.systemId).ToListAsync());
        }
        public async Task<ActionResult> TermianlLog()
        {
            var Termianl_Log = db.Termianl_Log;
            return View(await Termianl_Log.OrderByDescending(t => t.systemId).ToListAsync());
        }

        // GET: Terminals/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terminal terminal = await db.Terminal.FindAsync(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            return View(terminal);
        }

        // GET: Terminals/Create
        public ActionResult Create()
        {
            ViewBag.bankId = new SelectList(db.Bank, "systemId", "bankFullName");
            ViewBag.cityId = new SelectList(db.City, "systemId", "cityName");
            ViewBag.stateId = new SelectList(db.State, "systemId", "stateName");
            ViewBag.terminalFamilyId = new SelectList(db.TerminalFamily, "systemId", "TerminalFamilyName");
            ViewBag.vendorId = new SelectList(db.Vendor, "systemId", "vendorName");
            return View();
        }

        // POST: Terminals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,terminalId,terminalName,terminalStatus,terminalIP,serialNumber,terminalModel,address,street,terminalCode,workingKey,attitude,latitude,terminalFamilyId,stateId,cityId,vendorId,bankId,terminalPassword,terminalUserName")] Terminal terminal)
        {
            if (ModelState.IsValid)
            {

                var tid = db.Terminal.Where(t => t.terminalId == terminal.terminalId).FirstOrDefault() != null;
                var sn = db.Terminal.Where(t => t.serialNumber == terminal.serialNumber).FirstOrDefault() != null;

                if (tid || sn)
                {
                    if (tid)
                    {
                        ModelState.AddModelError("terminalId", "terminalId exsist");
                     
                    }
                    if (sn)
                    {
                        ModelState.AddModelError("serialNumber", "serialNumber exsist");
                    
                    }
                    ViewBag.bankId = new SelectList(db.Bank, "systemId", "bankFullName", terminal.bankId);
                    ViewBag.cityId = new SelectList(db.City, "systemId", "cityName", terminal.cityId);
                    ViewBag.stateId = new SelectList(db.State, "systemId", "stateName", terminal.stateId);
                    ViewBag.terminalFamilyId = new SelectList(db.TerminalFamily, "systemId", "TerminalFamilyName", terminal.terminalFamilyId);
                    ViewBag.vendorId = new SelectList(db.Vendor, "systemId", "vendorName", terminal.vendorId);
                    return View(terminal);

                }



                terminal = (Terminal)createEntity(terminal);
                db.Terminal.Add(terminal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.bankId = new SelectList(db.Bank, "systemId", "bankFullName", terminal.bankId);
            ViewBag.cityId = new SelectList(db.City, "systemId", "cityName", terminal.cityId);
            ViewBag.stateId = new SelectList(db.State, "systemId", "stateName", terminal.stateId);
            ViewBag.terminalFamilyId = new SelectList(db.TerminalFamily, "systemId", "TerminalFamilyName", terminal.terminalFamilyId);
            ViewBag.vendorId = new SelectList(db.Vendor, "systemId", "vendorName", terminal.vendorId);
            return View(terminal);
        }

        // GET: Terminals/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terminal terminal = await db.Terminal.FindAsync(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            ViewBag.bankId = new SelectList(db.Bank, "systemId", "bankFullName", terminal.bankId);
            ViewBag.cityId = new SelectList(db.City, "systemId", "cityName", terminal.cityId);
            ViewBag.stateId = new SelectList(db.State, "systemId", "stateName", terminal.stateId);
            ViewBag.terminalFamilyId = new SelectList(db.TerminalFamily, "systemId", "TerminalFamilyName", terminal.terminalFamilyId);
            ViewBag.vendorId = new SelectList(db.Vendor, "systemId", "vendorName", terminal.vendorId);
            return View(terminal);
        }

        // POST: Terminals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Terminal terminal)
        {
            try
            {
                var realterminal = db.Terminal.Find(terminal.systemId);
                var tid = db.Terminal.Where(t => t.terminalId == terminal.terminalId).FirstOrDefault() != null && terminal.terminalId != realterminal.terminalId;
                var sn = db.Terminal.Where(t => t.serialNumber == terminal.serialNumber).FirstOrDefault() != null && terminal.serialNumber != realterminal.serialNumber;

                if (tid || sn)
                {
                    if (tid)
                    {
                        ModelState.AddModelError("terminalId", "terminalId exsist");
                    }
                    if (sn)
                    {
                        ModelState.AddModelError("serialNumber", "serialNumber exsist");
                    }
                    ViewBag.bankId = new SelectList(db.Bank, "systemId", "bankFullName", terminal.bankId);
                    ViewBag.cityId = new SelectList(db.City, "systemId", "cityName", terminal.cityId);
                    ViewBag.stateId = new SelectList(db.State, "systemId", "stateName", terminal.stateId);
                    ViewBag.terminalFamilyId = new SelectList(db.TerminalFamily, "systemId", "TerminalFamilyName", terminal.terminalFamilyId);
                    ViewBag.vendorId = new SelectList(db.Vendor, "systemId", "vendorName", terminal.vendorId);
                    return View(terminal);

                }

               // terminal = (Terminal)updateEntity(terminal);
                //db.Entry(terminal).State = EntityState.Modified;
                db.Entry(db.Terminal.Find(terminal.systemId)).CurrentValues.SetValues(terminal);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch {


                ViewBag.bankId = new SelectList(db.Bank, "systemId", "bankFullName", terminal.bankId);
                ViewBag.cityId = new SelectList(db.City, "systemId", "cityName", terminal.cityId);
                ViewBag.stateId = new SelectList(db.State, "systemId", "stateName", terminal.stateId);
                ViewBag.terminalFamilyId = new SelectList(db.TerminalFamily, "systemId", "TerminalFamilyName", terminal.terminalFamilyId);
                ViewBag.vendorId = new SelectList(db.Vendor, "systemId", "vendorName", terminal.vendorId);
                return View(terminal);
            }       


        }

        // GET: Terminals/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terminal terminal = await db.Terminal.FindAsync(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            return View(terminal);
        }

        // POST: Terminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Terminal terminal = await db.Terminal.FindAsync(id);
            //terminal = (Terminal)deleteEntity( terminal);
            db.Terminal.Remove(terminal);
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
