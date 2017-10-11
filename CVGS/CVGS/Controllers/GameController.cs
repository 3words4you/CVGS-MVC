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
    public class GameController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Game
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Category).Include(g => g.Category1);
            return View(games.ToList());
        }

        // GET: Game/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName");

            var subCategoryList = new SelectList(db.Categories, "categoryID", "categoryName").ToList();
            subCategoryList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            ViewBag.subCategoryID = subCategoryList;

            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "gameID,title,description,develpoer,publisher,releasedDate,price,categoryID,subCategoryID,createdDate,updatedDate")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.createdDate = DateTime.Now;
                game.updatedDate = DateTime.Now;
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", game.categoryID);

            var subCategoryList = new SelectList(db.Categories, "categoryID", "categoryName").ToList();
            subCategoryList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            ViewBag.subCategoryID = subCategoryList;
            return View(game);
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", game.categoryID);

            var subCategoryList = new SelectList(db.Categories, "categoryID", "categoryName", game.subCategoryID).ToList();
            subCategoryList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            ViewBag.subCategoryID = subCategoryList;
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gameID,title,description,develpoer,publisher,releasedDate,price,categoryID,subCategoryID,createdDate,updatedDate")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.updatedDate = DateTime.Now;
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", game.categoryID);

            var subCategoryList = new SelectList(db.Categories, "categoryID", "categoryName",game.subCategoryID).ToList();
            subCategoryList.Insert(0, (new SelectListItem { Text = "None", Value = "0" }));
            
            return View(game);
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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
