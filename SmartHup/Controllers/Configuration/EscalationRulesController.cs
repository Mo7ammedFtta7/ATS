using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartHup.Models;

namespace SmartHup.Controllers.Configuration
{[Authorized]
    public class EscalationRulesController : Base
    {
        private TicketsEntities db = new TicketsEntities();

        // GET: EscalationRules
        public ActionResult Index()
        {
            var escalationRules = db.EscalationRules.Include(e => e.Category).Include(e => e.Category1).Include(e => e.EntityStatu).Include(e => e.Priority).Include(e => e.Priority1).Include(e => e.SLA).Include(e => e.Status).Include(e => e.Status1).Include(e => e.Timer).Include(e => e.UserGroup).Include(e => e.User).Include(e => e.User1).Include(e => e.User2);
            return View(escalationRules.Where(im => im.entityStatus_systemId == 1).ToList());
        }
        public List<Category> getCategoryList()
        {
           
            List<Category> CategoryList = db.Categories.Where(x => x.isChild == false).ToList();
            return CategoryList;
        }
        public ActionResult getSubCategoryList(int systemId)
        {

           
            List<Category> SubCategoryList = db.Categories.Where(x => x.ParentCategory == systemId).ToList();
            ViewBag.GetSubCategoryOptions = new SelectList(SubCategoryList, "systemId", "CategoryName");
            return PartialView("GetSubCategoryOptions");
        }

        // GET: EscalationRules/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationRule escalationRule = db.EscalationRules.Find(id);
            if (escalationRule == null)
            {
                return HttpNotFound();
            }
            return View(escalationRule);
        }

        // GET: EscalationRules/Create
        public ActionResult Create()
        {
             
            var userAssigned = db.UserGroups.Where(us => us.GroupTypeId == 1 || us.GroupTypeId==3) ;
            var userSubmitter = db.UserGroups.Where(us => us.GroupTypeId == 2 || us.GroupTypeId == 3);

            ViewBag.CotegoryList = new SelectList(getCategoryList(), "systemId", "CategoryName");
           // ViewBag.SubCategoryList = new SelectList(getCategoryList(), "systemId", "CategoryName");
            ViewBag.SubCategoryList = new SelectList(db.Categories.Where(im => im.isChild == true   && im.entityStatus_systemId == 1), "systemId", "CategoryName");
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            ViewBag.Priority_ID = new SelectList(db.Priorities.Where(im => im.entityStatus_systemId == 1), "systemId", "PriorityName");
            ViewBag.ChangePriority = new SelectList(db.Priorities.Where(im => im.entityStatus_systemId == 1), "systemId", "PriorityName");
            ViewBag.SLA_ID = new SelectList(db.SLAs.Where(im => im.entityStatus_systemId == 1), "systemId", "SLA_Name");
            ViewBag.Status_ID = new SelectList(db.Status.Where(im => im.entityStatus_systemId == 1), "systemId", "StatusName");
            ViewBag.ChangeStatus = new SelectList(db.Status.Where(im => im.entityStatus_systemId == 1), "systemId", "StatusName");
            ViewBag.TimerID = new SelectList(db.Timers.Where( im => im.entityStatus_systemId == 1),"systemId", "TimerName" );
            ViewBag.AssignedGroup = new SelectList(db.UserGroups.Where(im => im.GroupTypeId == 1 || im.GroupTypeId == 3 &&  im.entityStatus_systemId == 1), "systemId", "UserGroupName");
            ViewBag.AssignedUser_ID = new SelectList(db.Users.Where(i=>i.GroupID == userAssigned.FirstOrDefault().systemId && i.entityStatus_systemId == 1), "systemId", "UserName");
            ViewBag.SubmitterUSerID = new SelectList(db.Users.Where(i => i.GroupID == userSubmitter.FirstOrDefault().systemId && i.entityStatus_systemId == 1), "systemId", "UserName");
            ViewBag.ReAssignUser = new SelectList(db.Users.Where(i => i.GroupID == userAssigned.FirstOrDefault().systemId && i.entityStatus_systemId == 1), "systemId", "UserName");
            return View();
        }

        // POST: EscalationRules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "systemId,EscalationName,EscalationArName,Category_ID,Sub_Category_ID,SLA_ID,Status_ID,Priority_ID,ChangePriority,ChangeStatus,ReAssignUser,AssignedUser_ID,SubmitterUSerID,EscalationLevel,AssignedGroup,creationDate,modificationDate,createdBy,modifiedBy,version,entityStatus_systemId,deletedDate,deletedBy,EscalateAfter,EscalateBefore,FromTimer,FromDueDate,Hours,Minutes,TimerID")] EscalationRule escalationRule)
        {
            bool IsEscalaNameExist = db.EscalationRules.Any(x => x.EscalationName == escalationRule.EscalationName);
            if (IsEscalaNameExist == true)
            {
                ModelState.AddModelError("EscalationName", "Escalation Name already exists");

            }
            bool IsEscalARNameExist = db.EscalationRules.Any(x => x.EscalationArName == escalationRule.EscalationArName);
            if (IsEscalARNameExist == true)
            {
                ModelState.AddModelError("EscalationArName", "Escalation Arabic Name already exists");

            }
            if (ModelState.IsValid)
            {
				  escalationRule = (EscalationRule)createEntity( escalationRule);
                db.EscalationRules.Add(escalationRule);
                db.SaveChanges();
				log(escalationRule);
                return RedirectToAction("Index");
            }
          
            ViewBag.CotegoryList = new SelectList(getCategoryList(), "systemId", "CategoryName", escalationRule.Category_ID);
            //ViewBag.Sub_Category_ID = new SelectList(db.Categories, "systemId", "CategoryName", escalationRule.Sub_Category_ID);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", escalationRule.entityStatus_systemId);
            ViewBag.Priority_ID = new SelectList(db.Priorities, "systemId", "PriorityName", escalationRule.Priority_ID);
            ViewBag.ChangePriority = new SelectList(db.Priorities, "systemId", "PriorityName", escalationRule.ChangePriority);
            ViewBag.SLA_ID = new SelectList(db.SLAs, "systemId", "SLA_Name", escalationRule.SLA_ID);
            ViewBag.Status_ID = new SelectList(db.Status, "systemId", "StatusName", escalationRule.Status_ID);
            ViewBag.ChangeStatus = new SelectList(db.Status, "systemId", "StatusName", escalationRule.ChangeStatus);
            ViewBag.TimerID = new SelectList(db.Timers, "systemId", "TimerName", escalationRule.TimerID);
            ViewBag.AssignedGroup = new SelectList(db.UserGroups, "systemId", "UserGroupName", escalationRule.AssignedGroup);
            ViewBag.AssignedUser_ID = new SelectList(db.Users, "systemId", "UserName", escalationRule.AssignedUser_ID);
            ViewBag.SubmitterUSerID = new SelectList(db.Users, "systemId", "UserName", escalationRule.SubmitterUSerID);
            ViewBag.ReAssignUser = new SelectList(db.Users, "systemId", "UserName", escalationRule.ReAssignUser);
            return View(escalationRule);
        }

        // GET: EscalationRules/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationRule escalationRule = db.EscalationRules.Find(id);
            if (escalationRule == null)
            {
                return HttpNotFound();
            }
            var userAssigned = db.UserGroups.Where(us => us.GroupTypeId == 1 || us.GroupTypeId == 3);
            var userSubmitter = db.UserGroups.Where(us => us.GroupTypeId == 2 || us.GroupTypeId == 3);

            ViewBag.Category_ID = new SelectList(db.Categories.Where(im => im.isChild == false), "systemId", "CategoryName");
            //ViewBag.Category_ID = new SelectList(db.Categories.Where(im => im.entityStatus_systemId == 1), "systemId", "CategoryName", escalationRule.Category_ID);
            ViewBag.Sub_Category_ID = new SelectList(db.Categories.Where(im => im.isChild == true), "systemId", "CategoryName");
            //ViewBag.Sub_Category_ID = new SelectList(db.Categories.Where(im => im.entityStatus_systemId == 1), "systemId", "CategoryName", escalationRule.Sub_Category_ID);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", escalationRule.entityStatus_systemId);
            ViewBag.Priority_ID = new SelectList(db.Priorities.Where(im => im.entityStatus_systemId == 1), "systemId", "PriorityName", escalationRule.Priority_ID);
            ViewBag.ChangePriority = new SelectList(db.Priorities.Where(im => im.entityStatus_systemId == 1), "systemId", "PriorityName", escalationRule.ChangePriority);
            ViewBag.SLA_ID = new SelectList(db.SLAs.Where(im => im.entityStatus_systemId == 1), "systemId", "SLA_Name", escalationRule.SLA_ID);
            ViewBag.Status_ID = new SelectList(db.Status.Where(im => im.entityStatus_systemId == 1), "systemId", "StatusName", escalationRule.Status_ID);
            ViewBag.ChangeStatus = new SelectList(db.Status.Where(im => im.entityStatus_systemId == 1), "systemId", "StatusName", escalationRule.ChangeStatus);
            ViewBag.TimerID = new SelectList(db.Timers.Where(im => im.entityStatus_systemId == 1), "systemId", "TimerName", escalationRule.TimerID);
            ViewBag.AssignedGroup = new SelectList(db.UserGroups.Where(im => im.GroupTypeId == 1 || im.GroupTypeId == 3 && im.entityStatus_systemId == 1), "systemId", "UserGroupName" , escalationRule.UserGroup);
            ViewBag.AssignedUser_ID = new SelectList(db.Users.Where(i => i.GroupID == userAssigned.FirstOrDefault().systemId && i.entityStatus_systemId == 1), "systemId", "UserName", escalationRule.AssignedUser_ID);
            ViewBag.SubmitterUSerID = new SelectList(db.Users.Where(i => i.GroupID == userSubmitter.FirstOrDefault().systemId && i.entityStatus_systemId == 1), "systemId", "UserName", escalationRule.SubmitterUSerID);
            ViewBag.ReAssignUser = new SelectList(db.Users.Where(i => i.GroupID == userAssigned.FirstOrDefault().systemId && i.entityStatus_systemId == 1), "systemId", "UserName", escalationRule.ReAssignUser);
            return View(escalationRule);
        }

        // POST: EscalationRules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "systemId,EscalationName,EscalationArName,Category_ID,Sub_Category_ID,SLA_ID,Status_ID,Priority_ID,ChangePriority,ChangeStatus,ReAssignUser,AssignedUser_ID,SubmitterUSerID,EscalationLevel,AssignedGroup,creationDate,modificationDate,createdBy,modifiedBy,version,entityStatus_systemId,deletedDate,deletedBy,EscalateAfter,EscalateBefore,FromTimer,FromDueDate,Hours,Minutes,TimerID")] EscalationRule escalationRule)
        {
            var IsExist = db.EscalationRules.Any(x => x.systemId == escalationRule.systemId && x.EscalationName == escalationRule.EscalationName || x.EscalationName != escalationRule.EscalationName);
            var IsEscalaNameExist = db.EscalationRules.Any(x => x.systemId != escalationRule.systemId && x.EscalationName == escalationRule.EscalationName);
            var IsEscalaARNameExist = db.EscalationRules.Any(x => x.systemId != escalationRule.systemId && x.EscalationArName == escalationRule.EscalationArName);
            if (IsEscalaNameExist == true)
            {
                ModelState.AddModelError("EscalationName", "Escalation Name  already exists");
            }
            if (IsEscalaARNameExist == true)
            {
                ModelState.AddModelError("EscalationArName", "Escalation Arabic Name  already exists");
            }
            if (IsExist == true)
            {
                if (ModelState.IsValid)
                {
                    escalationRule = (EscalationRule)updateEntity(escalationRule);
                    db.Entry(escalationRule).State = EntityState.Modified;
                    db.SaveChanges();
                    log(escalationRule);
                    return RedirectToAction("Index");
                }
            }
               // return View(escalationRule);
            
            ViewBag.Category_ID = new SelectList(db.Categories, "systemId", "CategoryName", escalationRule.Category_ID);
            ViewBag.Sub_Category_ID = new SelectList(db.Categories, "systemId", "CategoryName", escalationRule.Sub_Category_ID);
            ViewBag.entityStatus_systemId = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", escalationRule.entityStatus_systemId);
            ViewBag.Priority_ID = new SelectList(db.Priorities, "systemId", "PriorityName", escalationRule.Priority_ID);
            ViewBag.ChangePriority = new SelectList(db.Priorities, "systemId", "PriorityName", escalationRule.ChangePriority);
            ViewBag.SLA_ID = new SelectList(db.SLAs, "systemId", "SLA_Name", escalationRule.SLA_ID);
            ViewBag.Status_ID = new SelectList(db.Status, "systemId", "StatusName", escalationRule.Status_ID);
            ViewBag.ChangeStatus = new SelectList(db.Status, "systemId", "StatusName", escalationRule.ChangeStatus);
            ViewBag.TimerID = new SelectList(db.Timers, "systemId", "TimerName", escalationRule.TimerID);
            ViewBag.AssignedGroup = new SelectList(db.UserGroups, "systemId", "UserGroupName", escalationRule.AssignedGroup);
            ViewBag.AssignedUser_ID = new SelectList(db.Users, "systemId", "UserName", escalationRule.AssignedUser_ID);
            ViewBag.SubmitterUSerID = new SelectList(db.Users, "systemId", "UserName", escalationRule.SubmitterUSerID);
            ViewBag.ReAssignUser = new SelectList(db.Users, "systemId", "UserName", escalationRule.ReAssignUser);

            return View(escalationRule);
        }

        // GET: EscalationRules/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EscalationRule escalationRule = db.EscalationRules.Find(id);
            if (escalationRule == null)
            {
                return HttpNotFound();
            }
            return View(escalationRule);
        }

        // POST: EscalationRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EscalationRule escalationRule = db.EscalationRules.Find(id);
            escalationRule = (EscalationRule)deleteEntity( escalationRule);
            db.Entry(escalationRule).State = EntityState.Modified;
            db.SaveChanges();
			log(escalationRule);
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
