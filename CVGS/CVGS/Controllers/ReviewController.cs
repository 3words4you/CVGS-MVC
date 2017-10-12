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
    public class ReviewController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Review
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Game).Include(r => r.User);
            return View(reviews.ToList());
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve([Bind(Include = "reviewID")] Review thisReview)
        {
            var id = thisReview.reviewID;
            Review review = db.Reviews.Find(id);
            review.status = 1;
            review.displayStatus = "Approved";
            review.updatedDate = DateTime.Now;
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Review/Deny/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deny([Bind(Include = "reviewID")] Review thisReview)
        {
            var id = thisReview.reviewID;
            Review review = db.Reviews.Find(id);
            review.status = 2;
            review.displayStatus = "Denied";
            review.updatedDate = DateTime.Now;
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
