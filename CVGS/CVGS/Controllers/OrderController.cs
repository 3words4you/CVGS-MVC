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
    public class OrderController : Controller
    {
        private CVGSEntities db = new CVGSEntities();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders;
            return View(orders.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var orderItems = db.OrderItems.Where(oi => oi.orderID == id).ToList();
            ViewBag.orderItems = orderItems;
            return View(order);
        }
        public ActionResult Delivering(int orderID)
        {
            if (Session == null || Session["userID"] == null || Session["role"].ToString() != "employee")
            {
                return RedirectToAction("Index", "Home");
            }
            if (orderID.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(orderID);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.status = 2;
            order.displayStatus = "delivering";
            order.updatedDate = DateTime.Now;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delivered(int orderID)
        {
            if (Session == null || Session["userID"] == null || Session["role"].ToString() != "employee")
            {
                return RedirectToAction("Index", "Home");
            }
            if (orderID.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(orderID);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.status = 3;
            order.displayStatus = "delivered";
            order.updatedDate = DateTime.Now;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}