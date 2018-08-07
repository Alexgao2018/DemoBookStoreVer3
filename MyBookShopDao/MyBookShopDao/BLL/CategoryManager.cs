using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;
using MyBookShopDao.DAL;
namespace MyBookShopDao.BLL
{
    public class CategoryManager
    {
        CategoryDAL cateDal = new CategoryDAL();
        //查询所有类别
        public List<Category> GetAll()
        {
            return cateDal.dbSet.ToList<Category>();
        }
        //插入
        public void AddCategory(Category cate)
        {
            cateDal.Insert(cate);
            cateDal.context.SaveChanges();
        }
        //删除
        public void Delete(int id)
        {
            cateDal.Delete(id);
            cateDal.context.SaveChanges();
        }
    }
}
