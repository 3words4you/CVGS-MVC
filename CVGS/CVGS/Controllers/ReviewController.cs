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

        //user add review
        public ActionResult AddReview(int? id)
        {
            if (Session == null || Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if( game == null)
            {
                return HttpNotFound();
            }
            int userID = Convert.ToInt32(Session["userID"]);
            var res = db.OrderItems.Where(oi => oi.gameID == id && oi.Order.userID == userID);
            if (res.Count() > 0)
            {
                ViewBag.gameTitle = game.title;
                ViewBag.gameID = id;
                return View();
            }
            else
            {
                TempData["msg"] = "not owned";
                return RedirectToAction("Index");
            }

        }

        [HttpPost, ActionName("AddReview")]
        public ActionResult ReviewPost(int gameID, string comment, int rating)
        {
            if (Session == null || Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (gameID.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(comment.Trim() == "" || comment == null)
            {
                return View();
            }

            int userID = Convert.ToInt32(Session["userID"]);
            Review review = new Review();
            review.gameID = gameID;
            review.userID = userID;
            review.comment = comment;
            review.rating = rating;
            review.status = 0;
            review.displayStatus = "pending";
            review.createdDate = DateTime.Now;
            review.updatedDate = DateTime.Now;
            db.Reviews.Add(review);
            db.SaveChanges();
            TempData["msg"] = "Review is added, waiting for verify";
            return RedirectToAction("Index","Game");
        }
    }
}
