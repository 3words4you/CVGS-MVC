using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;
using System.Threading.Tasks;

namespace CVGS.Controllers
{
    public class AddressController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Address
        public ActionResult Index()
        {
            if(Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userID = Convert.ToInt32(Session["userID"]);
            var addresses = db.Addresses.Where(a => a.userID == userID);
            return View(addresses.ToList());
        }

        // GET: Address/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Address/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "addressID,userID,province,city,address1,isDefault,createdDate,updatedDate")] Address address)
        {
            if (ModelState.IsValid)
            {
                if (Session == null || Session["userID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                int userID = Convert.ToInt32(Session["userID"]);
                address.userID = userID;
                address.createdDate = DateTime.Now;
                address.updatedDate = DateTime.Now;

                //handle reset default address
                DbContextTransaction transaction = db.Database.BeginTransaction();
               
                if (address.isDefault == 1 && db.Addresses.Count() > 0)
                {
                    var addressList = db.Addresses.Where(a => a.userID == userID);
                    foreach (var item in addressList)
                    {
                        
                        item.isDefault = 0;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                transaction.Commit();
                //end

                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(address);
        }

        // GET: Address/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "addressID,userID,province,city,address1,isDefault,createdDate,updatedDate")] Address address)
        {
            if (ModelState.IsValid)
            {
                if (Session["userID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                int userID = Convert.ToInt32(Session["userID"]);
                address.userID = userID;
                address.updatedDate = DateTime.Now;

                //handle reset default address
                DbContextTransaction transaction = db.Database.BeginTransaction();

                if (address.isDefault == 1 && db.Addresses.Count() > 0)
                {
                    var addressList = db.Addresses.Where(a => a.userID == userID && a.addressID != address.addressID);
                    foreach (var item in addressList)
                    {
                        
                        item.isDefault = 0;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    }
                    
                }
                transaction.Commit();

                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                
    
               
                return RedirectToAction("Index");
            }
           
            return View(address);
        }

        // GET: Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
