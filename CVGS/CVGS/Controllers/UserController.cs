using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;

namespace CVGS.Controllers
{
    public class UserController : Controller
    {
        private CVGSEntities db = new CVGSEntities();
        // GET: User
        public ActionResult ProfileDetail()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userID = Convert.ToInt32(Session["userID"]);
            var user = db.Users.Where(a => a.userID == userID).FirstOrDefault();
            return View(user);
        }

        public ActionResult EditProfile(int? id)
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
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "userID,nickname,gender,dob,phone,email")] User tempUserProfile)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Find(tempUserProfile.userID);
                user.nickname = tempUserProfile.nickname;
                user.dob = tempUserProfile.dob;
                user.gender = tempUserProfile.gender;
                user.phone = tempUserProfile.phone;
                user.email = tempUserProfile.email;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProfileDetail");
            }
            return View(tempUserProfile);
        }

        public ActionResult ChangePassword()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userID = Convert.ToInt32(Session["userID"]);
            var user = db.Users.Where(a => a.userID == userID).FirstOrDefault();
            ViewBag.userID = user.userID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword,string newPassword,int userID)
        {
            if (userID.ToString() == null || oldPassword.Trim().Equals("") || newPassword.Trim().Equals(""))
            {
                ModelState.AddModelError(string.Empty, "Missing paramters");
                ViewBag.userID = userID;
                return View();
            }
            User user = db.Users.Find(userID);
            if (user == null)
            {
                return HttpNotFound();
            }
            if(user.password != oldPassword)
            {
                ModelState.AddModelError(string.Empty, "Your old Password is not correct");
                ViewBag.userID = userID;
                return View();
            }
            user.password = newPassword;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}