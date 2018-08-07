using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookShopDao.Model;
using MyBookShopDao.BLL;
namespace MyBookShopWeb.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Admin/Order/

        public ActionResult List(string start, string end, int IsDelivered = 0, int orderid = 0)
        {
            List<Order> list = new OrderManager().GetOrders(start, end, IsDelivered, orderid);
            ViewBag.IsDelivered = IsDelivered;
            ViewBag.Orderid = orderid;
            ViewBag.Start = start;
            ViewBag.End = end;
            return View(list);
        }
        public ActionResult Details(int id)
        {
            Order order = new OrderManager().GetById(id);
            return View(order);
        }
        public ActionResult Update(int id)
        {
            new OrderManager().Deliver(id);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}
