using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;

namespace CVGS.Controllers
{
    public class AccountController : Controller
    {
        private CVGSEntities db = new CVGSEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(u => u.username == account.inputUsername && u.password == account.inputPassword);
                if (user == null)
                {
                    ModelState.AddModelError("inputPassword", "username or password is wrong or not existed");
                    return View(account);
                }
                Session["user"] = user.username;
                return RedirectToAction("Index", "Home");
            }
            return View(account);


        }

 
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.FirstOrDefault(a => a.username == account.inputUsername);
                if(u != null)
                {
                    ModelState.AddModelError("inputUsername", "Username is existed");
                    return View(account);
                }
                User user = new User();
                user.username = account.inputUsername;
                user.password = account.inputPassword;
                user.createdDate = DateTime.Now;
                user.updatedDate = DateTime.Now;
                user.role = "member";
                db.Users.Add(user);
                db.SaveChanges();
                Session["user"] = user.username;
                return RedirectToAction("Index", "Home");
            }
            return View(account);

        }
        
        public ActionResult Register()
        {
            return View();
            
        }

    }
}