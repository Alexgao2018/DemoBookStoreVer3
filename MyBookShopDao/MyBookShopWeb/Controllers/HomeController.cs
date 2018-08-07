using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookShopDao.Model;
using MyBookShopDao.BLL;

namespace MyBookShopWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            BookManager bookManager = new BookManager();
            List<Book> listBook = bookManager.GetBookOrderByDate();
            List<Book> listBook1 = bookManager.GetBookClick();
            List<Book> Recom = bookManager.GetTopByRecom();
            ViewData["listBook"] = listBook;
            ViewData["listBook1"] = listBook1;
            ViewData["Recom"] = Recom;
            return View();
        }

        public ActionResult CategoryTree()
        {
            CategoryManager cateManager = new CategoryManager();
            List<Category> list = cateManager.GetAll();
            return View(list);
        }
    }
}
