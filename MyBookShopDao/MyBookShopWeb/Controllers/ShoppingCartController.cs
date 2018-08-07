using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookShopDao.Model;
using MyBookShopDao.BLL;

namespace MyBookShopWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        //专门负责显示商品
        public ActionResult Index()
        {
            ShoppingCar cart = Session["cart"] as ShoppingCar;
            return View(cart);
        }
        //专门负责添加商品
        [HttpPost]
        public ActionResult AddCart(int id)
        {
            
            ShoppingCar cart = Session["cart"] as ShoppingCar;
            if (cart == null)//第一次选商品
            {
                cart = new ShoppingCar();
                Session["cart"] = cart;
            }
            cart.AddToCart(id);//添加商品至购物车
            return RedirectToAction("Index");
        }
        //删除
        public ActionResult Delete(int id)
        {
            ShoppingCar cart = Session["cart"] as ShoppingCar;
            if (cart != null)
            {
                cart.Delete(id);
            }
            return RedirectToAction("Index");
        }
        //修改
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCar cart = Session["cart"] as ShoppingCar;
            if (cart != null)
            {
                cart.Update(id, quantity);
            }
            return RedirectToAction("Index");
        }
        [Authorize]//授权，不允许匿名用户访问
        public ActionResult Submit()
        {
            ShoppingCar cart = Session["cart"] as ShoppingCar;
            return View(cart);
        }
        [HttpPost]
        public ActionResult Submit(Order order)
        {
            User user = (User)Session["user"];
            order.UserId = user.Id;
            order.OrderDate = DateTime.Now;
            order.IsDelivered = false;

            ShoppingCar cart = Session["cart"] as ShoppingCar;
            List<OrderDetail> list = new List<OrderDetail>();

            foreach (var c in cart.Items)
            {
                OrderDetail detail = new OrderDetail();
                detail.BookID = c.Id;
                detail.OrderID = c.Id;
                detail.UnitPrice = c.Price;
                detail.Quantity = c.Quantity;
                list.Add(detail);
            }
            new OrderManager().AddOrderDetail(order,list);
            Session["cart"] = null;
            string str = "<script>alert('生成订单成功!');window.location.href='/Home/Index'</script>";
            return Content(str);
        }
    }
}
