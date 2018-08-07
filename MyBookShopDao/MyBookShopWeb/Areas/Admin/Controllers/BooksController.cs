using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookShopDao.BLL;
using MyBookShopDao.Model;
using System.IO;

namespace MyBookShopWeb.Areas.Admin.Controllers
{
    public class BooksController : Controller
    {
        //
        // GET: /Admin/Books/
        int pageSize = 10;
        public ActionResult List(string term="",string title="",int pageNo=1)
        {
            BookManager manager = new BookManager();
            int rowCount;
            List<Book> list = manager.GetBooks(term, title, pageSize, pageNo, out rowCount);
            //总页数
            ViewBag.PageCount = (int)Math.Ceiling((double)rowCount / pageSize);
            //当前页
            ViewBag.PageNo = pageNo;

            Session["Term"] = term;
            ViewBag.Btitle = title;
            ViewBag.RowCount = rowCount;

            return View(list);
        }
        //删除
        public ActionResult Delete(int id)
        {
           if (id > 0)
            {
                new BookManager().Delete(id);
                string str = "<script>alert('删除成功');window.location.href='/Admin/Books/List'</script>";
                return Content(str);
            }
            else
            {
                string str = "<script>alert('删除失败');window.location.href='/Admin/Books/List'</script>";
                return Content(str);
            }
        }
        //修改
        public ActionResult EditBook(int id)
        {
            Book b = null;
            List<Publisher> list = new PublisherManager().GetAll();
            List<Category> list1 = new CategoryManager().GetAll();
            SelectList sel = new SelectList(list, "Id", "Name");
            SelectList sel1 = new SelectList(list1, "Id", "Name" );
            //下拉列表框的数据源
            if (id > 0)
            {
                b = new BookManager().GetById(id);
                sel = new SelectList(list, "Id", "Name", b.PublisherId);
            
                sel1 = new SelectList(list1, "Id", "Name", b.CategoryId);
            }
            ViewBag.PublisherList = sel;
            ViewBag.CateList = sel1;
            return View(b);
        }
        //修改
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBook(Book b,HttpPostedFileBase file)
        {
            if (file != null)
            {
                string name = Path.GetFileName(file.FileName);
                b.ISBN = name.Substring(0, name.IndexOf("."));
                string path = Server.MapPath("~/Images/BookCovers/" + name);
                file.SaveAs(path);
            }

            new BookManager().UpdateBook(b);
            return Content("<script>alert('修改成功!'),window.location.href='/Admin/Books/List'</script>");
           
        } 
    }
}
