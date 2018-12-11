using SmartHup.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmartHup.Controllers.Settings
{
    [Authorized]
    public class RolesController : Base
    {
        #region User Configurations

        private SMARTEntities db = new SMARTEntities();

        //private IDUserManager _userManager;

        //public IDUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<IDUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        //public RolesController() { }

        //public RolesController(IDUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //}



        #endregion



        // GET: Roles
        public ActionResult Index()
        {
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
                return View(db.Role.Include(r => r.ServiceProviderType).Where(r => r.serviceProviderTypeId == typeId));
            else
                return View(db.Role.Include(r => r.ServiceProviderType));
        }

        // GET: Roles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            displayEntityInfo(role.createdBy, role.modifiedBy, role.deletedBy);
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType.Where(spt => spt.systemId.Equals(Utils.CSP)), "systemId", "Name");
            }
            else
            {
                ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType, "systemId", "Name");
            }
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "systemId,serviceProviderTypeId,createdBy,modifiedBy,creationDate,modificationDate,status,version,Name,EName")] Role role)
        {
            bool IsRoleNameExist = db.Role.Any
        (x => x.Name == role.Name );
            if (IsRoleNameExist == true)
            {
                ModelState.AddModelError("Name", "Role Name Or EName already exists");
                // ModelState.AddModelError("EName", "Role Ename already exists");
            }
            bool IsRoleNameExistE = db.Role.Any
    (x =>  x.EName == role.EName);
            if (IsRoleNameExistE == true)
            {
               ModelState.AddModelError("EName", "Role Ename already exists");
            }
            if (ModelState.IsValid)
            {
                role.status = 1;
                role.createdBy = user_info.user.systemId;
                role.creationDate = DateTime.Now;
                db.Role.Add(role);
                db.SaveChanges();
                log(role);
                return RedirectToAction("Index");
            }

            ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType, "systemId", "Name", role.serviceProviderTypeId);
            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var user = getUser();
            var typeId = GetUserType(user);
            if (user.ServiceProvider.serviceProviderTypeId == typeId)
            {
                ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType.Where(spt => spt.systemId.Equals(Utils.CSP)), "systemId", "Name", role.serviceProviderTypeId);
                //ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", role.status);
                //return View(user.Role);
            }
            else
            {
                ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType, "systemId", "Name", role.serviceProviderTypeId);
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", role.status);
            return View(role);

        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "systemId,Id,serviceProviderTypeId,createdBy,modifiedBy,creationDate,modificationDate,status,version,Name,EName")] Role role)
        {
            if (ModelState.IsValid)
            {
                role.version = role.version + 1;
                role.modifiedBy = user_info.user.systemId;
                role.modificationDate = DateTime.Now;
                Console.Write(role);
                var old = db.Role.Find(role.systemId);
                //Role newrole = old; 
                //db.Entry(newrole).State = EntityState.Modified;
                db.Entry(old).CurrentValues.SetValues(role);
                db.SaveChanges();
                return RedirectToAction("Index", "Roles");
            }
            ViewBag.serviceProviderTypeId = new SelectList(db.ServiceProviderType, "systemId", "Name", role.serviceProviderTypeId);
            log(role);
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Role role = db.Role.Find(id);
            // db.Roles.Remove(role);
            if (role.User.Count == 0)
            {
                role.deletedBy = user_info.user.systemId;
                role.deletedDate = DateTime.Now;
                role.version = role.version + 1;
                role.status = Utils.STATUS_DELETED;

                log(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("CustErrorMessages", "This Role Contains Users");
                return View();
            }
        }

        public ActionResult AssignPages(long id)
        {
            var data = db.Module.Include(m => m.Action).Where(m => m.parentId != null).ToList();
            ViewBag.roleId = id;
            ViewBag.authAction = db.RoleActions.Where(ra => ra.RoleId == id).Select(ra => ra.actionId).ToList();
            //ViewBag.roleId = new SelectList( db.Roles.Where( r=>r.status == "1") , "systemId", "Name");
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignPages(List<CheckBoxViewModel> list, long roleId)
        {
            foreach (var page in list)
            {
                var temp = db.RoleActions.FirstOrDefault(ra => ra.actionId == page.id && ra.RoleId == roleId);

                if (page.isChecked)
                {
                    if (temp == null)
                    {
                        db.RoleActions.Add(
                            new RoleActions
                            {
                                RoleId = roleId,
                                actionId = page.id,
                                creationDate = DateTime.Now,
                                createdBy = user_info.user.systemId,
                                status = 1

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