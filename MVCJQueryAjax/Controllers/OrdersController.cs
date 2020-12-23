﻿using MVCJQueryAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCJQueryAjax.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        OnlineShopEntities db = new OnlineShopEntities();
        public ActionResult Index()
        {
            List<Customer> OrderAndCustomerList = db.Customers.ToList();
            return View(OrderAndCustomerList);
        }

        public ActionResult SaveOrder(string name, string address, Order[] order)
        {
            string result = "Error!, Order is not complete.";
            if(name != null || address != null || order != null)
            {
                var cutomerId = Guid.NewGuid();
                Customer model = new Customer();
                model.CustomerId = cutomerId;
                model.Name = name;
                model.Address = address;
                model.OrderDate = DateTime.Now;
                db.Customers.Add(model);

                foreach(var item in order)
                {
                    var orderId = Guid.NewGuid();
                    Order O = new Order();
                    O.OrderId = orderId;
                    O.ProductName = item.ProductName;
                    O.Quantity = item.Quantity;
                    O.Price = item.Price;
                    O.Amount = item.Amount;
                    O.CustomerId = cutomerId;
                    db.Orders.Add(O);
                }
                db.SaveChanges();
                result = "Success! Order is complete";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}