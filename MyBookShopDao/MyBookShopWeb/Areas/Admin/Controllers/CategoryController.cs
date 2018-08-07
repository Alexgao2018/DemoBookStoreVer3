using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookShopDao.BLL;
using MyBookShopDao.Model;

namespace MyBookShopWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/
        //查询全部
        public ActionResult List()
        {
            CategoryManager manager = new CategoryManager();
            List<Category> list = manager.GetAll();
            return View(list);
        }
        //插入
        public ActionResult AddCategory(Category cate)
        {
            new CategoryManager().AddCategory(cate);
            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            BookManager manager = new BookManager();
            List<Book> list=manager.GetByCid(id);
            if (list.Capacity > 0)
            {
                string str = "<script>alert('该类型下面有书籍,不能删除');window.location.href='/Admin/Category/List'</script>";
                return Content(str);
            }
            else
            {
                new CategoryManager().Delete(id);
                string str = "<script>alert('删除成功');window.location.href='/Admin/Category/List'</script>";
                return Content(str);
            }
        }
    }
}
