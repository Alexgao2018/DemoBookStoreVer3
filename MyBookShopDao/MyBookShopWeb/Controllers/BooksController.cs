using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookShopDao.BLL;
using MyBookShopDao.Model;

namespace MyBookShopWeb.Controllers
{
    public class BooksController : Controller
    {
        //
        // GET: /Books/

        int pageSize = 6;
        public ActionResult List(int cid = 0, int pageIndex = 1, string PublishDate = "PublishDate", string title = "")
        {
            BookManager manager = new BookManager();
            int rowCount;
            List<Book> list = manager.GetBookByCid(cid, pageSize, pageIndex, PublishDate, title, out rowCount);
            //总页数
            ViewBag.PageCount = (int)Math.Ceiling((double)rowCount / pageSize);
            //当前页
            ViewBag.PageIndex = pageIndex;
            //当前类别

           
            Session["CId"] = cid;
            Session["Btitle"] = title;
            Session["PublishDate"] = PublishDate;
            Session["rowCount"] = rowCount;

            return View(list);
        }

        public ActionResult Details(int id)
        {
            BookManager manager = new BookManager();
            Book book = manager.GetById(id);
            return View(book);
        }
    }
}
