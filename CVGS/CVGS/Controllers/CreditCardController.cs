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
    public class CreditCardController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: CreditCard
        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userID = Convert.ToInt32(Session["userID"]);
            var creditCards = db.CreditCards.Where(a => a.userID == userID);
            return View(creditCards.ToList());
        }

        // GET: CreditCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // GET: CreditCard/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.Users, "userID", "username");
            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "creditCardID,userID,owner,cardNumber,code,expiredDate,isDefault,createdDate,updatedDate")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                if (Session["userID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                int userID = Convert.ToInt32(Session["userID"]);
                creditCard.userID = userID;
                creditCard.createdDate = DateTime.Now;
                creditCard.updatedDate = DateTime.Now;

                //handle reset default
                DbContextTransaction transaction = db.Database.BeginTransaction();

                if (creditCard.isDefault == 1 && db.CreditCards.Count() > 0)
                {
                    var creditCardList = db.CreditCards.Where(a => a.userID == userID);
                    foreach (var item in creditCardList)
                    {

                        item.isDefault = 0;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                transaction.Commit();
                //end
                db.CreditCards.Add(creditCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creditCard);
        }

        // GET: CreditCard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "creditCardID,userID,owner,cardNumber,code,expiredDate,isDefault,createdDate,updatedDate")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                if (Session["userID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                int userID = Convert.ToInt32(Session["userID"]);
                creditCard.userID = userID;
                creditCard.updatedDate = DateTime.Now;

                //handle reset default 
                DbContextTransaction transaction = db.Database.BeginTransaction();

                if (creditCard.isDefault == 1 && db.CreditCards.Count() > 0)
                {
                    var creditCardList = db.CreditCards.Where(a => a.userID == userID && a.creditCardID != creditCard.creditCardID);
                    foreach (var item in creditCardList)
                    {

                        item.isDefault = 0;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();

                    }

                }
                transaction.Commit();

                db.Entry(creditCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creditCard);
        }

        // GET: CreditCard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = db.CreditCards.Find(id);
            db.CreditCards.Remove(creditCard);
            db.SaveChanges();
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
