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

    public class TransactionsController : Base
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: Transactions
        public async Task<ActionResult> Index()
        {
            var transaction = db.Transaction.OrderByDescending(a=>a.requestDateTime).Take(100) ;
            return View(await transaction.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.customerId = new SelectList(db.Currency, "systemId", "CurrencyName");
            ViewBag.paymentMethodId = new SelectList(db.PaymentMethod, "systemId", "paymentMethodId");
            ViewBag.paymentProviderId = new SelectList(db.PaymentProvider, "systemId", "paymentProviderId");
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId");
            ViewBag.systemResponseId = new SelectList(db.SystemResponse, "systemId", "systemResponseId");
            ViewBag.terminalId = new SelectList(db.Terminal, "systemId", "terminalName");
            ViewBag.originalTranID = new SelectList(db.Transaction, "systemId", "transactionLogStatus");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "systemId,status,transactionLogStatus,transactionId,requestDateTime,responseDateTime,exTranDateTime,packNo,transactionAmount,applicationId,discriminator,responseCode,responseMessage,responseStatus,PAN,MSISDN,bankAccount,expDate,mbr,tranCurrency,UUID,userName,entityId,entityType,toCard,fromAccountType,fromAccount,toAccountType,toAccount,accountCurrency,paymentInfo,originalTranUUID,originalTranID,phoneNo,email,registrationType,authenticationType,customerRefrence,transactionState,transactionFees,customerBalance,reciptNo,note,payeeId,systemResponseId,exSystemResponseId,paymentMethodId,channelId,paymentProviderId,currencyId,serviceId,serviceProviderId,customerId,customerCardId,terminalId,additional_8_Field_1,additional_8_Field_2,additional_8_Field_3,additional_16_Field_1,additional_16_Field_2,additional_16_Field_3,additional_32_Field_1,additional_32_Field_2,additional_32_Field_3,additional_128_Field_1,additional_128_Field_2,additional_256_Field_1,additional_256_Field_2,additional_512_Field_1,serviceNum,channelNum,paymentMethodNum,workingKey")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
				  transaction = (Transaction)createEntity( transaction);
                db.Transaction.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.customerId = new SelectList(db.Currency, "systemId", "CurrencyName", transaction.customerId);
            ViewBag.paymentMethodId = new SelectList(db.PaymentMethod, "systemId", "paymentMethodId", transaction.paymentMethodId);
            ViewBag.paymentProviderId = new SelectList(db.PaymentProvider, "systemId", "paymentProviderId", transaction.paymentProviderId);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", transaction.serviceId);
            ViewBag.systemResponseId = new SelectList(db.SystemResponse, "systemId", "systemResponseId", transaction.systemResponseId);
            ViewBag.terminalId = new SelectList(db.Terminal, "systemId", "terminalName", transaction.terminalId);
            ViewBag.originalTranID = new SelectList(db.Transaction, "systemId", "transactionLogStatus", transaction.originalTranID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerId = new SelectList(db.Currency, "systemId", "CurrencyName", transaction.customerId);
            ViewBag.paymentMethodId = new SelectList(db.PaymentMethod, "systemId", "paymentMethodId", transaction.paymentMethodId);
            ViewBag.paymentProviderId = new SelectList(db.PaymentProvider, "systemId", "paymentProviderId", transaction.paymentProviderId);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", transaction.serviceId);
            ViewBag.systemResponseId = new SelectList(db.SystemResponse, "systemId", "systemResponseId", transaction.systemResponseId);
            ViewBag.terminalId = new SelectList(db.Terminal, "systemId", "terminalName", transaction.terminalId);
            ViewBag.originalTranID = new SelectList(db.Transaction, "systemId", "transactionLogStatus", transaction.originalTranID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "systemId,status,transactionLogStatus,transactionId,requestDateTime,responseDateTime,exTranDateTime,packNo,transactionAmount,applicationId,discriminator,responseCode,responseMessage,responseStatus,PAN,MSISDN,bankAccount,expDate,mbr,tranCurrency,UUID,userName,entityId,entityType,toCard,fromAccountType,fromAccount,toAccountType,toAccount,accountCurrency,paymentInfo,originalTranUUID,originalTranID,phoneNo,email,registrationType,authenticationType,customerRefrence,transactionState,transactionFees,customerBalance,reciptNo,note,payeeId,systemResponseId,exSystemResponseId,paymentMethodId,channelId,paymentProviderId,currencyId,serviceId,serviceProviderId,customerId,customerCardId,terminalId,additional_8_Field_1,additional_8_Field_2,additional_8_Field_3,additional_16_Field_1,additional_16_Field_2,additional_16_Field_3,additional_32_Field_1,additional_32_Field_2,additional_32_Field_3,additional_128_Field_1,additional_128_Field_2,additional_256_Field_1,additional_256_Field_2,additional_512_Field_1,serviceNum,channelNum,paymentMethodNum,workingKey")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
				transaction = (Transaction)updateEntity( transaction);
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.customerId = new SelectList(db.Currency, "systemId", "CurrencyName", transaction.customerId);
            ViewBag.paymentMethodId = new SelectList(db.PaymentMethod, "systemId", "paymentMethodId", transaction.paymentMethodId);
            ViewBag.paymentProviderId = new SelectList(db.PaymentProvider, "systemId", "paymentProviderId", transaction.paymentProviderId);
            ViewBag.serviceId = new SelectList(db.Service, "systemId", "serviceId", transaction.serviceId);
            ViewBag.systemResponseId = new SelectList(db.SystemResponse, "systemId", "systemResponseId", transaction.systemResponseId);
            ViewBag.terminalId = new SelectList(db.Terminal, "systemId", "terminalName", transaction.terminalId);
            ViewBag.originalTranID = new SelectList(db.Transaction, "systemId", "transactionLogStatus", transaction.originalTranID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Transaction transaction = await db.Transaction.FindAsync(id);
			transaction = (Transaction)deleteEntity( transaction);
            
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
