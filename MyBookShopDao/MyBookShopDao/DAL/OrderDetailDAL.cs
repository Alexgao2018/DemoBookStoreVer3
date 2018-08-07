using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;
using System.Data.Entity;

namespace MyBookShopDao.DAL
{
    public class OrderDetailDAL:DBHelper<OrderDetail>
    {
        public OrderDetailDAL() { }

        public OrderDetailDAL(DbContext db) : base(db) { }

    }
}
