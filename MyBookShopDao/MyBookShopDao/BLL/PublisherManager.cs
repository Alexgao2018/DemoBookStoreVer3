using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;
using MyBookShopDao.DAL;

namespace MyBookShopDao.BLL
{
    public class PublisherManager
    {
        public List<Publisher> GetAll()
        {
            PublisherDAL pDal=new PublisherDAL();
            return pDal.dbSet.ToList<Publisher>();
        }
    }
}
