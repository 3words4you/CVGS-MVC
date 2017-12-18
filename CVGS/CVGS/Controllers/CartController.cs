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
    public class CartController : Controller
    {
        private CVGSEntities db = new CVGSEntities();
        // GET: Cart
        public ActionResult Index()
        {
            if (Session == null || Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userID = Convert.ToInt32(Session["userID"]);
            var cart = db.CartItems.Include(c => c.Game).Where(c=>c.userID==userID);

            //calculate total cost
            decimal total = 0;
            foreach(var item in cart)
            {
                total += Convert.ToDecimal(item.Game.price);
            }

            var addressList =
              db.Addresses
                .Where(a => a.userID == userID)
                .Select(a => new
                {
                    addressID = a.addressID,
                    fullAddress = a.province + " " + a.city + " " + a.address1,
                    isDefault = a.isDefault
                })
                .OrderByDescending(a => a.isDefault)
                .ToList();

            var creditCardList = db.CreditCards.Where(c => c.userID == userID).OrderByDescending(c => c.isDefault).ToList();

            ViewBag.total = total;
            ViewBag.addressID = new SelectList(addressList, "addressID", "fullAddress");
            ViewBag.addressCount = addressList.Count();
            ViewBag.creditCardCount = creditCardList.Count();
            ViewBag.creditCardID = new SelectList(creditCardList, "creditCardID", "cardNumber");
            return View(cart);
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            if (Session == null || Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            CartItem item = db.CartItems.Find(id);
            db.CartItems.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Checkout(int addressID, int creditCardID, string cost)
        {
            if (Session == null || Session["userID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (addressID.ToString() == null || creditCardID.ToString() == null || Convert.ToDecimal(cost) <= 0)
            {
                return HttpNotFound();
            }

            int userID = Convert.ToInt32(Session["userID"]);
            var address = db.Addresses.Find(addressID);
            var fullAddress = address.province + " " + address.city + " " + address.address1;
            var cardNummber = db.CreditCards.Find(creditCardID).cardNumber;
            decimal totalCost = Convert.ToDecimal(cost);

            //add order
            Order order = new Order();
            order.userID = userID;
            order.creditCard = cardNummber;
            order.fullAddress = fullAddress;
            order.cost = totalCost;
            order.status = 1;
            order.displayStatus = "Paid";
            order.createdDate = DateTime.Now;
            order.updatedDate = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();
            TempData["msg"] = "Order is Paid";

            //add orderItem
            DbContextTransaction transaction = db.Database.BeginTransaction();
            var orderID = order.orderID;
            var currentCart = db.CartItems.Where(c => c.userID == userID);
            foreach (var item in currentCart)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.orderID = orderID;
                orderItem.gameID = item.gameID;
                orderItem.createdDate = DateTime.Now;
                orderItem.updatedDate = DateTime.Now;
                db.OrderItems.Add(orderItem);
                db.SaveChanges();
            }
            transaction.Commit();

            //clear cart
            db.CartItems.RemoveRange(currentCart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}