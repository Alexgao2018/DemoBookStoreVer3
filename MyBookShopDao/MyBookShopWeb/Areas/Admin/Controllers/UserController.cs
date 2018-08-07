using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookShopDao.Model;
using MyBookShopDao.BLL;

namespace MyBookShopWeb.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Admin/User/

        public ActionResult Index()
        {
            List<User> list = new UserManager().GetAllUser();
            return View(list); 
        }
       
        public ActionResult Delete(int id)
        {
            OrderManager manager = new OrderManager();
            List<Order> list = manager.GetByUid(id);
            if (list.Capacity>0)
            {
                string str = "<script>alert('该用户下面有订单,不能删除');window.location.href='/Admin/User/Index'</script>";
                return Content(str);
            }
            else
            {
                new UserManager().DeleteUser(id);
                string str = "<script>alert('删除成功');window.location.href='/Admin/User/Index'</script>";
                return Content(str);
            }
        }

        public ActionResult Update(int id)
        {
            User u = new UserManager().GetById(id);
            return View(u);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(User u)
        {
            new UserManager().UpdateUser(u);
            string str = "<script>alert('修改成功!'),window.location.href='/Admin/User/Index'</script>";
            return Content(str);
        }

        public ActionResult UpdateState(bool isUser, List<int> ids)
        {
            new UserManager().GetUser(isUser,ids);
            return RedirectToAction("Index");
        }
    }
}
