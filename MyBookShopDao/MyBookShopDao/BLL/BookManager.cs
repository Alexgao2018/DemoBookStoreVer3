using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;
using MyBookShopDao.DAL;

namespace MyBookShopDao.BLL
{
    public class BookManager
    {
        BookDAL bookDal = new BookDAL();
        //查询所有类别
        public List<Book> GetAll()
        {
            return bookDal.dbSet.ToList<Book>();
        }
        //分页
        public List<Book> GetBookByCid(int cid, int pageSize, int pageIndex, string PublishDate, string title, out int rowCount)
        {
            return bookDal.GetBookByCid(cid, pageSize,pageIndex, PublishDate, title, out rowCount);
        }
        //显示最新出版的（按PublishDate降序）的9本图书
        public List<Book> GetBookOrderByDate()
        {
            return bookDal.GetBookOrderByDate();
        }
        //显示点击次数最多的12本图书
        public List<Book> GetBookClick()
        {
            return bookDal.GetBookClick();
        }
        //获得一条数据
        public Book GetById(int id)
        {
            return bookDal.GetByID(id);
        }
        //用户推荐的11本书
        public List<Book> GetTopByRecom()
        {
            return bookDal.GetTopByRecom();
        }
        //联合查询
        public List<Book> GetBooks(string term, string title, int pageSize, int pageIndex, out int rowCount)
        {
            return bookDal.GetBooks(term, title, pageSize, pageIndex, out rowCount);
        }
        //删除
        public void Delete(int id)
        {
            bookDal.Delete(id);
            bookDal.context.SaveChanges();
        }
        public List<Book> GetByCid(int cid)
        {
            return bookDal.GetByCid(cid);
        }
        //修改
        public void UpdateBook(Book b)
        {
            bookDal.Update(b);
            bookDal.context.SaveChanges();
        }
    }
}
