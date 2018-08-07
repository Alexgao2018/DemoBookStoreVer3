using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.BLL;
using MyBookShopDao.Model;

namespace MyBookShopDao.DAL
{
    public class BookDAL : DBHelper<Book>
    {
        //分页
        public List<Book> GetBookByCid(int cid, int pageSize, int pageIndex, string PublishDate, string title, out int rowCount)
        {
            var result = from p in dbSet
                         where p.Title.Contains(title)
                         select p;
           
            if (cid != 0)
            {
                result = from p in result
                         where p.CategoryId == cid
                         select p;
            }
            if (PublishDate == "PublishDate")
            {
                result = from p in result
                         orderby p.PublishDate descending
                         select p;
            }
            else
            {
                result = from p in result
                         orderby p.UnitPrice descending
                         select p;
            }

            List<Book> list = result.Skip((pageIndex - 1) * pageSize).Take(6).ToList<Book>();
                    
            rowCount = result.Count();

            return list;
        }
        //时间
        public List<Book> GetBookOrderByDate()
        {
            var result = from p in dbSet
                         orderby p.PublishDate descending
                         select p;
            List<Book> list = result.Take(9).ToList<Book>();
            return list;
        }
        //点击率最高
        public List<Book> GetBookClick()
        {
            var result = from p in dbSet
                         orderby p.Clicks descending
                         select p;
            List<Book> list = result.Take(12).ToList<Book>();
            return list;
        }
        //读者推荐
        public List<Book> GetTopByRecom()
        {
            var result = dbSet.SqlQuery("select * from Books where Id in (select top 11 BookId from RecomBooks group by BookId order by COUNT(*) desc)");
            List<Book> list = result.ToList<Book>();
            return list;
        }
        //后台分页
        public List<Book> GetBooks(string term, string title, int pageSize, int pageIndex, out int rowCount)
        {

            //查询全部
            var result = from p in dbSet
                         orderby p.Id descending
                         select p;

            if (title == "书名" && !title.Equals("") && !term.Equals(""))
            {
                result = from p in result
                         where p.Title.Contains(term)
                         orderby p.Id descending
                         select p;
            }

            if (title == "出版社" && !title.Equals("") && !term.Equals(""))
            {
                result = from p in result
                         where p.Publisher.Name.Contains(term)
                         orderby p.Id descending
                         select p;
            }
            if (title == "作者" && !title.Equals("") && !term.Equals(""))
            {
                result = from p in result
                         where p.Author.Contains(term)
                         orderby p.Id descending
                         select p;
            }
            if (title == "内容简介" && !title.Equals("") && !term.Equals(""))
            {
                result = from p in result
                         where p.ContentDescription.Contains(term)
                         orderby p.Id descending
                         select p;
            }

            //后台分页
            List<Book> list = result.Skip((pageIndex - 1) * pageSize)
                              .Take(pageSize)
                              .ToList<Book>();

            rowCount = result.Count();  //记录数
            return list;
        }
        public List<Book> GetByCid(int cid)
        {
            var result = from p in dbSet
                         where p.CategoryId == cid
                         select p;
            List<Book> list = result.ToList<Book>();
            return list;
        }
    }
}
