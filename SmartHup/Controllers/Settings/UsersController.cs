using SmartHup.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SmartHup.Controllers.Settings
{
    [Authorized]

    public class UsersController : Base
    {

        private TicketsEntities db = new TicketsEntities();
        //private ApplicationSignInManager _signInManager;
        //private IDUserManager _userManager;
        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

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

        //public UsersController() { }

        //public UsersController(IDUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //private void AddErrors(IdentityResu- result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        //#endregion



        // GET: Users
        public ActionResult Index()
        {
            var userid = user_info.user.systemId;
            var user = db.Users.FirstOrDefault(u => u.systemId == userid);
            //var typeId = db.GroupType.FirstOrDefault(spt => spt.systemId.Equals(Utils.CSP)).systemId;
          //  if (user.UserGroup.GroupTypeId == typeId)
          //  {
            //    return View(db.Users.Where(o => o.UserGroup.serviceProviderTypeId == typeId && o.serviceProviderId == user.serviceProviderId).ToList());
            //}
           // else
                return View(db.Users.ToList());
        }

        public ActionResult AddUser()
        {
            var user = getUser();
            var typeId = GetUserType(user);
            //if (user.UserGroup.serviceProviderTypeId == typeId)
            //{

            //    ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.status == 1 && sp.systemId == user.serviceProviderId), "systemId", "serviceProviderName");
            //    ViewBag.RoleID = new SelectList(db.Role.Where(r => r.status == 1 && r.serviceProviderTypeId == typeId), "systemId", "Name");
            //}
            //else
            //{
            //    ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.status == 1), "systemId", "serviceProviderName");
            //    ViewBag.RoleID = new SelectList(db.Role.Where(r => r.status == 1/* && r.serviceProviderTypeId == typeId*/), "systemId", "Name");
            //}
            //var typeId = db.GroupType.FirstOrDefault(spt => spt.systemId.Equals(Utils.Utils.Utils.CSP)).systemId;
            //TwoFactorEnabled, PhoneNumberConfirmed, SecurityStamp, EmailConfirmed, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount
             ViewBag.RoleID = new SelectList(db.Roles.ToList(), "systemId", "Name");

            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", user.status);

            return View();
        }
        public ActionResult UpdateUser(long id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //if (user.LockoutEndDateUtc < DateTime.UtcNow)
            //    TempData["condition"] = "1";
            //else
            //    TempData["condition"] = "0";
            var data = new RegisterViewModel
            {
                Email = user.Email,
             //   RoleID = user.RoleID ,
             //   serviceProviderId = user.serviceProviderId,
                systemId = user.systemId,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName

            };
            var typeId = GetUserType(user);
            //if (user.UserGroup.serviceProviderTypeId == typeId)
            //{

            //    ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.status == 1 && sp.systemId == user.serviceProviderId), "systemId", "serviceProviderName", user.serviceProviderId);
            //    ViewBag.RoleID = new SelectList(db.Role.Where(r => r.status == 1 && r.serviceProviderTypeId == typeId), "systemId", "Name", user.RoleID);
            //}
            //else
            //{
            //    ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.status == 1), "systemId", "serviceProviderName", user.serviceProviderId);
            //    ViewBag.RoleID = new SelectList(db.Role.Where(r => r.status == 1), "systemId", "Name", user.RoleID);
            //}
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", user.status);


            return View(data);
        }

        public ActionResult UserProfile(long Id)
        {

            User user = db.Users.Find(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName", user.status);
            displayEntityInfo(user.createdBy, user.modifiedBy, user.deletedBy);
            return View(user);
        }

        public ActionResult DeleteUser(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles.Where(r => r.systemId == user.RoleID), "systemId", "Name", user.RoleID);
            //ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.systemId == user.serviceProviderId), "systemId", "serviceProviderName", user.serviceProviderId);
            //var data = new RegisterViewModel{
            //    Email = user.Email, RoleID = user.RoleID, serviceProviderId = user.serviceProviderId,
            //    systemId = user.systemId, PhoneNumber = user.PhoneNumber, Username = user.UserName
            //};
            log(user);
            return View(user);
        }

        public ActionResult ResetPassword(long id)
        {
            var data = new ResetPasswordViewModel { systemId = id };
            log(data);
            return View(data);
        }

        public async Task<ActionResult> LockUser(long id)
        {
            var lockoutEndDate = new DateTimeOffset(new DateTime(9998, 01, 01));
             User user = db.Users.Find(id);
            user.LockoutEnabled = true;
            //user.LockoutEndDateUtc = Convert.ToDateTime ( lockoutEndDate.ToString()) ;
            db.Entry(user).State = EntityState.Modified;

            //db.Entry(user).CurrentValues.SetValues(user);
            db.SaveChanges();
            //UserManager.SetLockoutEnabled(id, true);
            //IdentityResult result = await UserManager.SetLockoutEndDateAsync(id, lockoutEndDate);
            //AddErrors(result);

            log(new { systemId = id, page = "Lock User", status = "Locked" });
            return RedirectToAction("Index", "Users");
        }

        public async Task<ActionResult> UnLockUser(long id)
        {
             User user = db.Users.Find(id);
            user.LockoutEnabled = false;
            //user.LockoutEndDateUtc = Convert.ToDateTime(DateTimeOffset.UtcNow.ToString());
            db.Entry(user).State = EntityState.Modified;

            //db.Entry(user).CurrentValues.SetValues(user);
            db.SaveChanges();
            //UserManager.SetLockoutEnabled(id, true);
            //IdentityResult result = await UserManager.SetLockoutEndDateAsync(id, DateTimeOffset.UtcNow);
            //await UserManager.ResetAccessFailedCountAsync(id);

            log(new { systemId = id, page = "Unlock User", status = "Unlocked" });
            return RedirectToAction("Index", "Users");
        }


       
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                 User user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    //RoleID = model.RoleID,
                    //serviceProviderId = model.serviceProviderId,
                    PasswordHash  = Crypto.HashPassword(model.Password),

                    //status = model.status,
                    createdBy = user_info.user.systemId,
                    creationDate = DateTime.Now,
                    version = 0
                };
                //IdentityResult result = new IdentityResult();
                try
                {
                    //result = await UserManager.CreateAsync(user, model.Password);
                    db.Users.Add(user);
                    db.SaveChanges();
                   log(new { systemId = model.Username, page = "Add User", status = "true" });
                    return RedirectToAction("Index", "Users");

                }
                catch (Exception e)
                {
                    //For Debug And Testing
                    ModelState.AddModelError(e.ToString(), e.InnerException.Message);
                    log(new { systemId = model.Username, page = "Add User", status = "false" });
                    return View(model);

                }
                //if (result.Succeeded)
                //{
                //    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //    if (model.status == 1)
                //        await UnLockUser(user.systemId);
                //    else
                //        await LockUser(user.systemId);
                //    log(new { systemId = model.Username, page = "Add User", status = "true" });
                //    return RedirectToAction("Index", "Users");
                //}
                //     AddErrors(result);
            }
            //ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.status == 1), "systemId", "serviceProviderName");
            //ViewBag.RoleID = new SelectList(db.Role.Where(r => r.status == 1), "systemId", "Name");
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUser(RegisterViewModel model, string condition)
        {
            if (model.status == 1)
                await UnLockUser(model.systemId);
            else
                await LockUser(model.systemId);
             User user = db.Users.Find(model.systemId);

            user.UserName = model.Username;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            //user.RoleID = model.RoleID;
            //user.serviceProviderId = model.serviceProviderId;
            //user.status = model.status;
            user.modifiedBy = user_info.user.systemId;
            user.modificationDate = DateTime.Now;
            try
            {
                db.Entry(user).State = EntityState.Modified;

                //db.Entry(user).CurrentValues.SetValues(user);
                db.SaveChanges();
                log(new {data= user, page = "Update User", status = "Success" });
                return RedirectToAction("Index", "Users");
            }
            catch (Exception e)
            {
                  log(new { data = user, page = "Update User", status = "failed" });

                ViewBag.msg = e.ToString();
                //ViewBag.RoleID = new SelectList(db.Role.Where(r => r.status == 1), "systemId", "Name", user.RoleID);
                //ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.status == 1), "systemId", "serviceProviderName", user.serviceProviderId);

                ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
                return View();
            }
            //var result = await UserManager.UpdateAsync(user);
            //if (result.Succeeded)
            //{
            //    log(new { user, page = "Update User", status = "Success" });
            //    return RedirectToAction("Index", "Users");
            //}
             //AddErrors(result);
            //ViewBag.RoleID = new SelectList(db.Role.Where(r => r.status == 1), "systemId", "Name", user.RoleID);
            //ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.status == 1), "systemId", "serviceProviderName", user.serviceProviderId);
            ViewBag.status = new SelectList(db.EntityStatus, "systemId", "EntityStatusName");
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(long Id)
        {
            var user = db.Users.Find(Id);

            if (Id == user_info.user.systemId)
            {
                ModelState.AddModelError("", "Cannot delete your current active user that you logged with");
                log(new { user = Id, page = "Delete User", status = "false", msg = "Cannot delete your current active user that you logged with" });
                ViewBag.RoleID = new SelectList(db.Roles.Where(r => r.systemId == user.RoleID), "systemId", "Name", user.RoleID);
                //ViewBag.serviceProviderId = new SelectList(db.UserGroup.Where(sp => sp.systemId == user.serviceProviderId), "systemId", "serviceProviderName", user.serviceProviderId);
                return View(user);
            }

            user.deletedBy = user_info.user.systemId /*User.Identity.GetUserId<long>()*/;
            user.deletedDate = DateTime.Now;
            user.version = user.version + 1;
            user.status = Utils.STATUS_DELETED;
            //user = (SmartHup.Models.User)deleteEntity(User);
                user.LockoutEnabled = true;
                //db.Users.Remove(user);
                db.Entry(user).State = EntityState.Modified;
                //db.Entry(user).CurrentValues.SetValues(user);
                db.SaveChanges();
                log(new { user = Id, page = "Delete User", status = "true" });
                   return RedirectToAction("Index", "Users");

            //var result = await UserManager.DeleteAsync(user);
            //if (result.Succeeded)
            //{
            //    log(new { user = Id, page = "Delete User", status = "true" });
            //    return RedirectToAction("Index", "Users");
            //}
       //     AddErrors(result);
           
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = user_info.user.systemId;
            if (user == null)
            {
                log(new { page = "Reset Password", status = "Not Found" });
                // Don't reveal that the user does not exist
                return RedirectToAction("Index", "Home");
            }

            //string token = await UserManager.GeneratePasswordResetTokenAsync(model.systemId);
            //var result = await UserManager.ResetPasswordAsync(user.systemId, token, model.Password);
            //if (result.Succeeded)
            //{
            //    log(new { page = "Reset Password", status = "true" });
            //    return RedirectToAction("Index", "Users");
            //}
            log(new { page = "Reset Password", status = "false" });
       //     AddErrors(result);
            return View();
        }

        
    }

}